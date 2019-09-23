using System.Diagnostics;
using Landkreuzer.Types;
using UnityEngine;

namespace Landkreuzer.Behaviours {
	public class WeaponController : MonoBehaviour {
		public WeaponParameters Parameters { get; private set; }

		private readonly Stopwatch _stopwatch = new Stopwatch();

		public static WeaponController Create(WeaponParameters parameters, Vector3 position, Quaternion rotation,
			Transform parent = null) {
			var weapon = Instantiate(parameters.towerPrefab, position, rotation, parent)
				.AddComponent<WeaponController>();
			weapon.Parameters = parameters;
			return weapon;
		}

		public void Shoot() {
			if (_stopwatch.IsRunning && _stopwatch.ElapsedMilliseconds < Parameters.cooldown * 1000)
				return;
			var t = transform;
			var projectile = Instantiate(Parameters.projectilePrefab, t.position + t.forward,
				t.rotation);
			var projectileController = projectile.AddComponent<ProjectileController>();
			projectileController.SetParameters(new ProjectileParameters(Parameters.damage, Parameters.projectileSpeed));
			projectileController.Fire();
			_stopwatch.Restart();
		}
	}
}