using System;
using System.Collections;
using Landkreuzer.Types;
using UnityEngine;

namespace Landkreuzer.Behaviours {
	public class ProjectileParameters {
		public readonly float Damage;
		public readonly float Speed;
		public ProjectileParameters(float damage, float speed) {
			Damage = damage;
			Speed = speed;
		}
	}
	public class ProjectileController : MonoBehaviour, IProjectile<ProjectileParameters>, IDamaging {
		public float Damage => _parameters.Damage;

		private ProjectileParameters _parameters;

		private void Awake() {
			GetComponent<Collider>().isTrigger = true;
		}

		private void OnTriggerEnter(Collider other) {
			Destroy(gameObject);
		}

		private void OnCollisionEnter(Collision other) {
			Destroy(gameObject);
		}

		public void Fire() {
			StartCoroutine(Movement(_parameters.Speed));
			Statistics.StatisticsEvent(StatisticType.Shot, 1);
		}

		public void SetParameters(ProjectileParameters parameters) {
			_parameters = parameters;
		}

		private IEnumerator Movement(float speed) {
			while (true) {
				transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.Self);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}