using System;
using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.AI;

namespace Landkreuzer.Behaviours {
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyController : BeingControllerAbstract {
		private NavMeshAgent _navMeshAgent;

		public override void SetParameters(BeingParameters parameters) {
			base.SetParameters(parameters);
			_navMeshAgent.speed = 0;
			_navMeshAgent.angularSpeed = parameters.rotationSpeed;
		}

		private protected override void Awake() {
			base.Awake();
			_navMeshAgent = GetComponent<NavMeshAgent>();
			OnDead.AddListener(Death);
		}

		private void Death(BeingControllerAbstract arg0) {
			Debug.Log("Death");
			Destroy(gameObject);
		}

		private void OnTriggerEnter(Collider other) {
			var projectileController = other.GetComponent<ProjectileController>();
			if (projectileController) {
				executor.Hurt((uint) projectileController.Damage);
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