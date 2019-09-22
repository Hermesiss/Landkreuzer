using System;
using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Landkreuzer.Behaviours {
	[Serializable]
	public class BeingExecutor : IBeing {
		[SerializeField] private BeingParameters beingParameters;

		public BeingParameters BeingParameters {
			get => beingParameters;
			set {
				beingParameters = value;
				Health = beingParameters.health;
			}
		}

		public float Health { get; private set; }

		public float Hurt(float value) {
			value = Mathf.Clamp(value, 0, Single.PositiveInfinity);

			var usingDefence = value - beingParameters.defence;
			var prevHealth = Health;
			Health -= usingDefence;
			if (Health <= 0) {
				Health = 0;
				OnDead.Invoke();
				return Health;
			}
			
			Debug.Log($"Dmg {value}, def {beingParameters.defence}, health reduced from {prevHealth} to {Health}");
			return Health;
		}

		public float Heal(float value) {
			value = Mathf.Clamp(value, 0, Single.PositiveInfinity);
			var prevHealth = Health;
			Health = Mathf.Clamp(Health + value, 0, beingParameters.health);
			Debug.Log($"Heal {value}, health increased from {prevHealth} to {Health}");
			return Health;
		}

		public UnityEvent OnDead { get; } = new UnityEvent();
	}
}