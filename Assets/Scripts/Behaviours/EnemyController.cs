using System;
using System.Diagnostics;
using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

namespace Landkreuzer.Behaviours {
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyController : BeingControllerAbstract {
		private NavMeshAgent _navMeshAgent;
		private Stopwatch _stopwatch = new Stopwatch();

		public override void SetParameters(BeingParameters parameters) {
			base.SetParameters(parameters);
			_navMeshAgent.speed = 0;
			_navMeshAgent.angularSpeed = parameters.rotationSpeed;
		}

		private protected override void Awake() {
			base.Awake();
			_navMeshAgent = GetComponent<NavMeshAgent>();
		}

		protected override void Death() {
			Statistics.StatisticsEvent(StatisticType.Death, 1);
			Debug.Log("Death");
			Destroy(gameObject);
		}

		private void OnTriggerEnter(Collider other) {
			var projectileController = other.GetComponent<ProjectileController>();
			if (projectileController) {
				var dmg = projectileController.Damage;
				Statistics.StatisticsEvent(StatisticType.Damage, dmg);
				Statistics.StatisticsEvent(StatisticType.Hit, 1);
				executor.Hurt((uint) dmg);
			}
		}

		private void OnTriggerStay(Collider other) {
			if (other.tag.Equals("Player")) {
				//Debug.Log();
				if (!_stopwatch.IsRunning || (_stopwatch.ElapsedMilliseconds > 1000))
				{
					var player = other.GetComponent<PanzerController>();
					if (player) {
						Debug.Log($"Is running {_stopwatch.IsRunning}, elapsed {_stopwatch.ElapsedMilliseconds}");
						player.executor.Hurt(executor.BeingParameters.baseDmg);
						_stopwatch.Restart();
					}
				}
			}
		}


		private void Update() {
			_navMeshAgent.SetDestination(PanzerController.Position);
			var t = transform;
			if (Mathf.Pow(_navMeshAgent.stoppingDistance, 2) < (_navMeshAgent.destination - t.position).sqrMagnitude) {
				t.Rotate(Vector3.up,
					Vector3.SignedAngle(t.forward, _navMeshAgent.steeringTarget - t.position,
						Vector3.up) *
					executor.BeingParameters.rotationSpeed * Time.deltaTime / 6);
				t.Translate(
					Time.deltaTime * executor.BeingParameters.speed * t.forward, Space.World);
			}

			Debug.DrawLine(_navMeshAgent.steeringTarget, t.position);
			Debug.DrawLine(t.position, transform.position + t.forward);
		}
	}
}