using System;
using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Landkreuzer.Behaviours {
	public class BeingEvent : UnityEvent<BeingControllerAbstract> { }

	public abstract class BeingControllerAbstract : MonoBehaviour {
		public BeingExecutor executor = new BeingExecutor();
		public BeingEvent OnDead { get; } = new BeingEvent();
		private bool _isAlive = true;
		private bool _displayOverlay = true;

		private protected virtual void Awake() {
			executor.OnDead.AddListener(() => {
				if (!_isAlive) return;
				_isAlive = false;
				OnDead.Invoke(this);
				Death();
			});
			Overseer.OnGameOver.AddListener(() => _displayOverlay = false);
		}

		public virtual void SetParameters(BeingParameters parameters) => executor.BeingParameters = parameters;

		/// <summary>
		/// Call this inside OnGUI method
		/// </summary>
		/// <param name="cam">Usually <see cref="Camera.main"/></param>
		/// <param name="viewportRect">Usually <see cref="Camera.main.pixelRect"/></param>
		public void DisplayHealth(Camera cam, Rect viewportRect) {
			if (!_displayOverlay) return;
			var being = this;
			var pos = cam.WorldToViewportPoint(being.transform.position + Vector3.up);
			var rect = new Rect(0, 0, 100, 20);
			rect.x = pos.x * viewportRect.width - rect.width / 2;
			rect.y = viewportRect.height - pos.y * viewportRect.height - rect.height / 2;

			//Current health / Full health
			GUI.Label(rect, $"{being.executor.Health}/{being.executor.BeingParameters.health}");
			rect.y += rect.height;
			rect.height = 6;
			
			//Full health background rectangle
			GUI.Box(rect, "", GUI.skin.box);
			rect.width *= being.executor.Health / being.executor.BeingParameters.health;
			
			//Current health foreground rectangle
			GUI.Box(rect, "", GUI.skin.textField);
		}

		protected abstract void Death();
	}
}