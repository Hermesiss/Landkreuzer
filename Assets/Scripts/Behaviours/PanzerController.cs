using System;
using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Landkreuzer.Behaviours {
	public class PanzerController : BeingControllerAbstract, Input.IPanzerActions {
		[SerializeField] private Transform weaponPlace;
		[SerializeField] private WeaponParameters[] weapons;

		private Input _input;
		private Vector2 _currentMovement;
		private BeingParameters BeingParams => executor.BeingParameters;
		private Rigidbody _rigidbody;
		private int _selectedWeapon = 0;
		private GameObject[] _instancedWeapons;

		#region MonoBehaviourCallbacks

		private void Awake() {
			_input = new Input();
			_input.Panzer.SetCallbacks(this);
			_rigidbody = GetComponent<Rigidbody>();

			InstanceWeapons();
			SelectWeapon(0);
		}

		private void Update() {
			InputSystem.Update();
			LegacyMovementDetection();
			Rotate(_currentMovement.x);
			Move(_currentMovement.y);
		}

		private void OnEnable() => _input.Panzer.Enable();

		private void OnDisable() => _input.Panzer.Disable();

		#endregion

		#region InputSystemCallbacks

		public void OnShoot(InputAction.CallbackContext context) {
			if (context.started)
				Debug.Log("Shoot");
		}

		public void OnMove(InputAction.CallbackContext context) {
			//var axes = context.ReadValue<Vector2>(); TODO: use this instead of LegacyMovementDetection() when bug is resolved
			//_currentMovement = axes;
		}

		public void OnChangeWeapon(InputAction.CallbackContext context) {
			if (context.started) {
				var value = context.ReadValue<float>();
				SelectWeapon(_selectedWeapon + (int) Mathf.Sign(value));
				Debug.Log($"Change weapon:{value}");
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
			var up = Pressed(KeyCode.UpArrow) || Pressed(KeyCode.W) ? 1 : 0;
			var down = Pressed(KeyCode.DownArrow) || Pressed(KeyCode.S) ? 1 : 0;
			var right = Pressed(KeyCode.RightArrow) || Pressed(KeyCode.D) ? 1 : 0;
			var left = Pressed(KeyCode.LeftArrow) || Pressed(KeyCode.A) ? 1 : 0;

			_currentMovement = new Vector2(right - left, up - down);

			bool Pressed(KeyCode key) {
				return UnityEngine.Input.GetKey(key);
			}
		}

		private void InstanceWeapons() {
			_instancedWeapons = new GameObject[weapons.Length];
			for (var i = 0; i < weapons.Length; i++) {
				_instancedWeapons[i] = Instantiate(weapons[i].towerPrefab, Vector3.down * 100, Quaternion.identity);
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
		}

		private void Rotate(float signedAngle) {
			_rigidbody.angularVelocity = BeingParams.rotationSpeed * signedAngle * Time.deltaTime * 60 * Vector3.up;
		}
	}
}