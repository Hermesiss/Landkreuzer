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

		private bool _canSpawn;
		private List<EnemyController> _enemies = new List<EnemyController>();
		private Stopwatch _stopwatch;
		private Coroutine _spawnCoroutine;

		private void Awake() {
			_spawnCoroutine = StartCoroutine(SpawnInstructions());
		}

		public void SetSpawningState(bool mode) {
			_canSpawn = mode;
		}

		private EnemyController Spawn(BeingParameters parameters, Vector3 position, Quaternion rotation,
			Transform parent = null) {
			var enemy = Instantiate(enemyPrefab, position, rotation, parent).AddComponent<EnemyController>();
			enemy.SetParameters(parameters);
			enemy.OnDead.AddListener(EnemyDeadHandler);
			return enemy;
		}

		private void EnemyDeadHandler(BeingControllerAbstract arg0) {
			try {
				_enemies.Remove((EnemyController) arg0);
			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}

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

		private Quaternion GetSpawnRotation(Vector3 pos) {
			return Quaternion.LookRotation(PanzerController.Position - pos, Vector3.up);
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


		private void OnGUI() {
			var main = Camera.main;
			if (main == null) return;

			var style = new GUIStyle(GUI.skin.box) {
				normal = {
					background = new Texture2D(2, 2) {wrapMode = TextureWrapMode.Repeat}, textColor = Color.black
				}
			};
			var viewportRect = main.pixelRect;

			foreach (var enemyController in _enemies) {
				var pos = main.WorldToViewportPoint(enemyController.transform.position + Vector3.up);
				var rect = new Rect(0, 0, 100, 20);
				rect.x = pos.x * viewportRect.width - rect.width / 2;
				rect.y = viewportRect.height - pos.y * viewportRect.height - rect.height / 2;

				GUI.Box(rect, $"{enemyController.executor.Health}/{enemyController.executor.BeingParameters.health}",
					style);
				rect.y += rect.height;
				rect.height = 3;
				rect.width *= enemyController.executor.Health / enemyController.executor.BeingParameters.health;
				GUI.Box(rect,"", style);
			}
		}
	}
}