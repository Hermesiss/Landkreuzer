using UnityEngine;

namespace Landkreuzer.Behaviours {
	public abstract class BeingControllerAbstract : MonoBehaviour {
		[SerializeField] protected BeingExecutor executor;
	}
}