using Trismegistus.Core.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Landkreuzer.Behaviours.UI {
	[RequireComponent(typeof(ScenePicker))]
	public class UiControllerMainMenu : UiControllerAbstract {
		[SerializeField] private Button startBtn;

		private ScenePicker _scenePicker;

		private void Awake() {
			_scenePicker = GetComponent<ScenePicker>();
			startBtn.onClick.AddListener(() => SceneManager.LoadScene(_scenePicker.scenePath));
			exitBtn.onClick.AddListener(Application.Quit);
		}
	}
}