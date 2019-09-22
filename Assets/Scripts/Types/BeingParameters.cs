using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Landkreuzer.Types {
	[CreateAssetMenu(fileName = "New BeingParameters", menuName = "Trismegistus/BeingParameters")]
	public class BeingParameters : ScriptableObject {
		public float health;
		public float defence;
		public float speed;
		public float baseDmg;

		/// <summary>
		/// Angles per second
		/// </summary>
		public float rotationSpeed = 90;
	}
}