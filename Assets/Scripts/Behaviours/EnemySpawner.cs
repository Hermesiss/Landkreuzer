using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Landkreuzer.Types;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Landkreuzer.Behaviours {
	public class EnemySpawner : MonoBehaviour {
		[SerializeField] private float cooldown = 1;
		[SerializeField] private int maxEnemies = 10;
		[SerializeField] private GameObject enemyPrefab;
		[SerializeField] private BeingParameters[] enemiesPool;
		[SerializeField] private Renderer perimeter;

		private readonly List<EnemyController> _enemies = new List<EnemyController>();
		private Stopwatch _stopwatch;
		private Coroutine _spawnCoroutine;
		private Vector3 _playerPosition;

		private void Awake() {
			_spawnCoroutine = StartCoroutine(SpawnInstructions());
			Overseer.OnPlayerMove.AddListener(pos => { _playerPosition = pos; });
		}

		/// <summary>
		/// Spawns an <see cref="enemyPrefab"/> with random parameters from <see cref="enemiesPool"/>
		/// at a random point on a <see cref="perimeter"/> not faster than <see cref="cooldown"/>
		/// when total <see cref="_enemies"/> count is smaller than <see cref="maxEnemies"/>.
		/// Pass this as a parameter to <see cref="StartCoroutine()"/>
		/// </summary>
		private IEnumerator SpawnInstructions() {
			_stopwatch = new Stopwatch();
			_stopwatch.Start();
			while (true) {
				var enemyCount = _enemies.Count;
				if (_stopwatch.ElapsedMilliseconds > cooldown * 1000 && enemyCount < maxEnemies) {
					var spawnPosition = GetSpawnPosition();
					var enemy = Spawn(enemiesPool[Random.Range(0, enemiesPool.Length)], spawnPosition,
						GetSpawnRotation(spawnPosition));
					_enemies.Add(enemy);
					_stopwatch.Restart();
				}

				yield return new WaitForEndOfFrame();
			}
		}
		
		private EnemyController Spawn(BeingParameters parameters, Vector3 position, Quaternion rotation,
			Transform parent = null) {
			var enemy = Instantiate(enemyPrefab, position, rotation, parent).AddComponent<EnemyController>();
			enemy.SetParameters(parameters);
			enemy.OnDead.AddListener(EnemyDeadHandler);
			return enemy;
		}

		
		private void EnemyDeadHandler(BeingControllerAbstract being) {
			try {
				_enemies.Remove((EnemyController) being);
			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}

		private Vector3 GetSpawnPosition() => GetPointOnPerimeter(perimeter.bounds);

		private Vector3 GetPointOnPerimeter(Bounds bounds) {
			var random = Random.value;
			var useX = random < bounds.size.x / (bounds.size.x + bounds.size.z);
			var x = useX ? Random.value * bounds.size.x : Mathf.RoundToInt(Random.value) * bounds.size.x;
			var z = !useX ? Random.value * bounds.size.z : Mathf.RoundToInt(Random.value) * bounds.size.z;
			Debug.Log($"Random on perimeter: {x}, {z}");
			return new Vector3(bounds.min.x + x, 0, bounds.min.z + z);
		}
		
		/// <summary>
		/// Rotation towards player position
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Quaternion GetSpawnRotation(Vector3 pos) {
			return Quaternion.LookRotation(_playerPosition - pos, Vector3.up);
		}

		private void OnGUI() {
			var main = Camera.main;
			if (main == null) return;
			var viewportRect = main.pixelRect;

			foreach (var enemyController in _enemies) {
				enemyController.DisplayHealth(main, viewportRect);
			}
		}
	}
}