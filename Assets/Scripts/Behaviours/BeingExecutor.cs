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
			set => beingParameters = value;
		}

		public int Health { get; private set; }

		public int Hurt(uint value) {
			throw new System.NotImplementedException();
		}

		public int Heal(uint value) {
			throw new System.NotImplementedException();
		}

		public UnityEvent OnDead { get; } = new UnityEvent();
	}
}