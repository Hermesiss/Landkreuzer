using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Landkreuzer.Types {
	[CreateAssetMenu(fileName = "New BeingParameters", menuName = "Trismegistus/BeingParameters")]
	public class BeingParameters : ScriptableObject {
		public int health;
		public int defence;
		public int speed;
		public int baseDmg;
	}
}