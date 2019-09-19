using UnityEngine;

namespace Landkreuzer.Types {
	[CreateAssetMenu(fileName = "New WeaponParameters", menuName = "Trismegistus/WeaponParameters")]
	public class WeaponParameters : ScriptableObject {
		public GameObject towerPrefab;
		public GameObject projectilePrefab;
		public int damage;
		public float cooldown;
		public float projectileSpeed;
	}
}