using UnityEngine;

namespace Landkreuzer.Types {
	[CreateAssetMenu(fileName = "New WeaponParameters", menuName = "Trismegistus/WeaponParameters")]
	public class WeaponParameters : ScriptableObject {
		public GameObject prefab;
		public int damage;
		public float cooldown;
	}
}