using System;
using System.Diagnostics;
using Landkreuzer.Types;
using Trismegistus.Core.Types;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Landkreuzer.Behaviours {
	public interface IPlayer {
		UnityEvent OnPlayerBorn { get; }
		UnityEvent OnPlayerDied { get; }
		
		UnityVec3Event OnPlayerMove { get; }
	}
	public class PanzerController : BeingControllerAbstract, Input.IPanzerActions, IPlayer {
		
		public UnityEvent OnPlayerBorn { get; } = new UnityEvent();
		public UnityEvent OnPlayerDied { get; } = new UnityEvent();
		public UnityVec3Event OnPlayerMove { get; } = new UnityVec3Event();

		[SerializeField] private Transform weaponPlace;
		[SerializeField] private WeaponParameters[] weapons;

		private Input _input;
		private Vector2 _currentMovement;
		private BeingParameters BeingParams => executor.BeingParameters;
		private Rigidbody _rigidbody;
		private int _selectedWeapon = 0;
		private WeaponController[] _instancedWeapons;
		private bool _controlsEnabled;
		private Stopwatch _gameStopwatch = new Stopwatch();

		#region MonoBehaviourCallbacks

		private new void Awake() {
			base.Awake();
			_input = new Input();
			_input.Panzer.SetCallbacks(this);
			_rigidbody = GetComponent<Rigidbody>();
			executor.ApplyParameters();

			InstantiateWeapons();
			SelectWeapon(0);
			_gameStopwatch.Start();
			Overseer.RegisterPlayer(this);
			
		}

		private void Start() {
			OnPlayerBorn.Invoke();
		}

		private void Update() {
			InputSystem.Update();
			LegacyMovementDetection();
			Rotate(_currentMovement.x);
			Move(_currentMovement.y);
		}

		private void OnEnable() => ControlsState(true);

		private void OnDisable() => ControlsState(false);

		private void OnGUI() {
			var main = Camera.main;
			if (main == null) return;
			var viewportRect = main.pixelRect;
			DisplayHealth(main, viewportRect);
		}

		#endregion

		#region InputSystemCallbacks

		public void OnShoot(InputAction.CallbackContext context) {
			if (context.started) {
				Shoot();
			}
		}

		public void OnMove(InputAction.CallbackContext context) {
			//var axes = context.ReadValue<Vector2>(); TODO: use this instead of LegacyMovementDetection() when bug is resolved
			//_currentMovement = axes;
		}

		public void OnChangeWeapon(InputAction.CallbackContext context) {
			if (context.started) {
				SelectWeapon(_selectedWeapon + (int) Mathf.Sign(context.ReadValue<float>()));
			}
		}

		#endregion

		/// <summary>
		/// Use this instead of <see cref="OnMove"/>
		/// reason:
		/// bug https://forum.unity.com/threads/bug-with-two-keyboard-keys-pressed-or-released-simultaneously.706955/
		/// bug https://issuetracker.unity3d.com/issues/callbackcontext-is-not-called-when-two-buttons-are-pressed-or-released
		/// </summary>
		private void LegacyMovementDetection() {
			if (!_controlsEnabled) return;
			var up = Pressed(KeyCode.UpArrow) || Pressed(KeyCode.W) ? 1 : 0;
			var down = Pressed(KeyCode.DownArrow) || Pressed(KeyCode.S) ? 1 : 0;
			var right = Pressed(KeyCode.RightArrow) || Pressed(KeyCode.D) ? 1 : 0;
			var left = Pressed(KeyCode.LeftArrow) || Pressed(KeyCode.A) ? 1 : 0;

			_currentMovement = new Vector2(right - left, up - down);

			bool Pressed(KeyCode key) {
				return UnityEngine.Input.GetKey(key);
			}
		}

		private void InstantiateWeapons() {
			if (_instancedWeapons != null) {
				foreach (var instancedWeapon in _instancedWeapons) {
					Destroy(instancedWeapon);
				}
			}

			_instancedWeapons = new WeaponController[weapons.Length];
			for (var i = 0; i < weapons.Length; i++) {
				_instancedWeapons[i] = WeaponController.Create(weapons[i], Vector3.down * 100, Quaternion.identity);
			}
		}

		private void SelectWeapon(int index) {
			var count = _instancedWeapons.Length;
			index = (index + count) % count; //So we can call this method with small offset 
			var prev = _instancedWeapons[_selectedWeapon];
			prev.transform.parent = null;
			prev.transform.position = Vector3.down * 100;
			var current = _instancedWeapons[index];
			current.transform.parent = transform;
			current.transform.SetPositionAndRotation(weaponPlace.position, weaponPlace.rotation);
			_selectedWeapon = index;
		}

		private void Move(float amount) {
			_rigidbody.velocity = BeingParams.speed * amount * Time.deltaTime * 60 * transform.forward;
			OnPlayerMove.Invoke(transform.position);
		}


		private void Rotate(float signedAngle) => _rigidbody.angularVelocity =
			BeingParams.rotationSpeed * signedAngle * Time.deltaTime * 60 * Vector3.up;

		private void Shoot() => _instancedWeapons[_selectedWeapon].Shoot();

		private void ControlsState(bool mode) {
			_controlsEnabled = mode;
			if (mode) _input.Panzer.Enable();
			else _input.Panzer.Disable();
		}
		
		
		protected override void Death() {
			ControlsState(false);
			//Statistics.StatisticsEvent(StatisticType.Time, _gameStopwatch.ElapsedMilliseconds/1000f);
			_gameStopwatch.Stop();
			OnPlayerDied.Invoke();
		}
	}
}