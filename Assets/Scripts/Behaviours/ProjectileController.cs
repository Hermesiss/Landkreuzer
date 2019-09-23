using System;
using System.Collections;
using Landkreuzer.Types;
using UnityEngine;
using UnityEngine.Events;

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

		public ProjectileController() {
			Overseer.RegisterProjectile(this);
		}

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
			OnFire.Invoke();
			StartCoroutine(Movement(_parameters.Speed));
		}

		public void SetParameters(ProjectileParameters parameters) {
			_parameters = parameters;
		}

		public UnityEvent OnFire { get; } = new UnityEvent();

		private IEnumerator Movement(float speed) {
			while (true) {
				transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.Self);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}