using System;
using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.Events;

namespace Landkreuzer.Behaviours {
	public class BeingEvent : UnityEvent<BeingControllerAbstract> {
		
	}
	public abstract class BeingControllerAbstract : MonoBehaviour {
		public BeingExecutor executor = new BeingExecutor();
		public BeingEvent OnDead { get; } = new BeingEvent();

		private protected virtual void Awake() {
			executor.OnDead.AddListener(() => OnDead.Invoke(this));
		}

		public virtual void SetParameters(BeingParameters parameters) => executor.BeingParameters = parameters;
	}
}