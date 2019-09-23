using System;
using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Landkreuzer.Behaviours {
	[Serializable]
	public class BeingExecutor : IBeing {
		[SerializeField] private BeingParameters beingParameters;

		/// <inheritdoc />
		public BeingParameters BeingParameters {
			get => beingParameters;
			set {
				beingParameters = value;
				ApplyParameters();
			}
		}

		internal void ApplyParameters() {
			if (!beingParameters) return;
			Health = beingParameters.health;
		}

		/// <inheritdoc />
		public float Health { get; private set; }


		/// <inheritdoc />
		public float Hurt(float value) {
			value = Mathf.Clamp(value, 0, Single.PositiveInfinity);

			var usingDefence = value - beingParameters.defence;
			var prevHealth = Health;
			Health -= usingDefence;
			if (Health <= 0) {
				Health = 0;
				OnDead.Invoke();
			}
			
			Debug.Log($"Dmg {value}, def {beingParameters.defence}, health reduced from {prevHealth} to {Health}");
			return Health;
		}
		
		/// <inheritdoc />
		public float Heal(float value) {
			value = Mathf.Clamp(value, 0, Single.PositiveInfinity);
			var prevHealth = Health;
			Health = Mathf.Clamp(Health + value, 0, beingParameters.health);
			Debug.Log($"Heal {value}, health increased from {prevHealth} to {Health}");
			return Health;
		}
		
		/// <inheritdoc />
		public UnityEvent OnDead { get; } = new UnityEvent();


		public BeingExecutor() {
			ApplyParameters();
		}
	}
}