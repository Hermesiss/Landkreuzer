using Landkreuzer.Types;
using UnityEngine;

namespace Landkreuzer.Behaviours {
	public class BeingExecutor : MonoBehaviour, IBeing {
		[SerializeField] private BeingParameters beingParameters;

		public BeingParameters GetBeingParameters => beingParameters;
		
		public int Health { get; private set; }

		public int Hurt(uint value) {
			throw new System.NotImplementedException();
		}

		public int Heal(uint value) {
			throw new System.NotImplementedException();
		}
	}
}