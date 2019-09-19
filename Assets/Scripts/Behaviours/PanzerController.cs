using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Landkreuzer.Behaviours {
	public class PanzerController : BeingControllerAbstract, Input.IPanzerActions {
		[SerializeField] private WeaponParameters[] weapons;

		private Input _input;

		private void Awake() {
			_input = new Input();
			_input.Panzer.SetCallbacks(this);
			//_input.Panzer.Enable();
		}

		private void OnEnable() {
			_input.Panzer.Enable();
		}

		private void OnDisable() {
			_input.Panzer.Disable();
		}

		public void OnShoot(InputAction.CallbackContext context) {
			if (context.started)
				Debug.Log("Shoot");
		}

		public void OnMove(InputAction.CallbackContext context) {
			var axes = context.ReadValue<Vector2>();
			Debug.Log(axes);
		}

		public void OnChangeWeapon(InputAction.CallbackContext context) {
			if (context.started) {
				var value = context.ReadValue<float>();
				Debug.Log(value);
			}
		}
	}
}