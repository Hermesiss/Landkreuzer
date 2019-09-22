using UnityEngine;
using UnityEngine.Events;

namespace Landkreuzer.Types {
	public interface IBeing {
		BeingParameters BeingParameters { get; set; }
		
		float Health { get; }
		
		/// <summary>
		/// Decrease health
		/// </summary>
		/// <param name="value">Pure damage points before applying modificators</param>
		/// <returns>Health after hurting</returns>
		float Hurt(float value);
		
		/// <summary>
		/// Increase health 
		/// </summary>
		/// <param name="value">Pure heal points before applying modificators</param>
		/// <returns>Health after healing</returns>
		float Heal(float value);
		
		UnityEvent OnDead { get; }
	}

	public interface IProjectile<T> {
		void Fire();
		void SetParameters(T parameters);
	}

	public interface IDamaging {
		int Damage { get; }
	}
}