using System;
using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Landkreuzer.Behaviours {
	public class PanzerController : BeingControllerAbstract, Input.IPanzerActions {
		[SerializeField] private WeaponParameters[] weapons;

		private Input _input;
		private Vector2 _currentMovement;
		private BeingParameters BeingParams => executor.BeingParameters;

		#region MonoBehaviourCallbacks

		private void Awake() {
			_input = new Input();
			_input.Panzer.SetCallbacks(this);
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
			var down = Pressed(KeyCode.DownArrow) || Pressed(KeyCode.S) ? 1:0;
			var right = Pressed(KeyCode.RightArrow) || Pressed(KeyCode.D) ? 1:0;
			var left = Pressed(KeyCode.LeftArrow) || Pressed(KeyCode.A) ? 1:0;

			_currentMovement = new Vector2(right - left, up-down);

			bool Pressed(KeyCode key) {
				return UnityEngine.Input.GetKey(key);
			}	
		}

		private void Move(float amount) {
			transform.Translate(BeingParams.speed * amount * Time.deltaTime * Vector3.forward);
		}

		private void Rotate(float signedAngle) {
			transform.Rotate(Vector3.up, BeingParams.rotationSpeed * signedAngle * Time.deltaTime);
		}
	}
}