using UnityEngine.Events;

namespace Landkreuzer {
	public static class Overseer {
		public static UnityEvent OnGameOver { get; } = new UnityEvent();
		public static UnityEvent OnGameStart { get; } = new UnityEvent();

		public static void GameOver() {
			OnGameOver.Invoke();
		}
		
		public static void GameStart() {
			OnGameStart.Invoke();
		}
	}
}