using Landkreuzer.Behaviours;
using Landkreuzer.Types;
using Trismegistus.Core.Types;
using UnityEngine.Events;

namespace Landkreuzer {
	/// <summary>
	/// Been there. Seen that. Got the scars.
	/// </summary>
	public static class Overseer {
		public static UnityEvent OnGameOver { get; } = new UnityEvent();
		public static UnityEvent OnGameStart { get; } = new UnityEvent();
		
		public static UnityVec3Event OnPlayerMove { get; } = new UnityVec3Event();

		public static UnityEvent OnEnemyHit { get; } = new UnityEvent();

		public static UnityFloatEvent OnEnemyHurt { get; } = new UnityFloatEvent();

		public static BeingEvent OnEnemySpawn { get; } = new BeingEvent();
		public static BeingEvent OnEnemyKilled { get; } = new BeingEvent();

		public static UnityEvent OnShot { get; } = new UnityEvent();

		public static void RegisterPlayer(IPlayer player) {
			player.OnPlayerBorn.AddListener(OnGameStart.Invoke);
			player.OnPlayerDied.AddListener(OnGameOver.Invoke);
			player.OnPlayerMove.AddListener(OnPlayerMove.Invoke);
		}

		public static void RegisterEnemy(IEnemy enemy) {
			enemy.OnHit.AddListener(OnEnemyHit.Invoke);
			enemy.OnDamageReceived.AddListener(OnEnemyHurt.Invoke);
			enemy.OnSpawn.AddListener(OnEnemySpawn.Invoke);
			enemy.OnKilled.AddListener(OnEnemyKilled.Invoke);
		}

		public static void RegisterProjectile<T>(IProjectile<T> projectile) {
			projectile.OnFire.AddListener(OnShot.Invoke);
		}
	}
}