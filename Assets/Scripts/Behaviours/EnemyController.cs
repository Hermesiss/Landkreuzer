using System;
using System.Diagnostics;
using Landkreuzer.Types;
using Trismegistus.Core.Types;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace Landkreuzer.Behaviours {
	
	public interface IEnemy {
		UnityFloatEvent OnDamageReceived { get; }
		UnityEvent OnHit { get; }
		BeingEvent OnSpawn { get; }
		BeingEvent OnKilled { get; }
	}

	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyController : BeingControllerAbstract, IEnemy {
		private NavMeshAgent _navMeshAgent;
		private Stopwatch _stopwatch = new Stopwatch();
		private Vector3 _playerPosition;

		public UnityFloatEvent OnDamageReceived { get; } = new UnityFloatEvent();
		public UnityEvent OnHit { get; } = new UnityEvent();
		public BeingEvent OnSpawn { get; } = new BeingEvent();
		public BeingEvent OnKilled { get; } = new BeingEvent();

		public override void SetParameters(BeingParameters parameters) {
			base.SetParameters(parameters);
			_navMeshAgent.speed = 0;
			_navMeshAgent.angularSpeed = parameters.rotationSpeed;
		}

		private protected override void Awake() {
			base.Awake();
			
			_navMeshAgent = GetComponent<NavMeshAgent>();
			Overseer.RegisterEnemy(this);
			Overseer.OnPlayerMove.AddListener(pos => { _playerPosition = pos; });
			OnSpawn.Invoke(this);
		}

		protected override void Death() {
			OnKilled.Invoke(this);
			Debug.Log("Death");
			Destroy(gameObject);
		}

		private void OnTriggerEnter(Collider other) {
			var projectileController = other.GetComponent<ProjectileController>();
			if (projectileController) {
				var dmg = projectileController.Damage;
				OnHit.Invoke();
				OnDamageReceived.Invoke(dmg);
				executor.Hurt((uint) dmg);
			}
		}

		private void OnTriggerStay(Collider other) {
			if (other.tag.Equals("Player")) {
				//Debug.Log();
				if (!_stopwatch.IsRunning || (_stopwatch.ElapsedMilliseconds > 1000)) {
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
			_navMeshAgent.SetDestination(_playerPosition);
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