using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Landkreuzer.Behaviours.UI {
	[RequireComponent(typeof(Canvas))]
	public abstract class UiControllerAbstract : MonoBehaviour{
		[SerializeField] private protected Button exitBtn;
	}


	public class UiController : UiControllerAbstract {
		[SerializeField] private RectTransform scoreRoot;
		[SerializeField] private ScoreView scoreViewPrefab;
		[SerializeField] private Button restartBtn, menuBtn;

		private Canvas _canvas;

		private void Awake() {
			_canvas = GetComponent<Canvas>();
			_canvas.enabled = false;
			Overseer.OnGameOver.AddListener(DisplayScores);
			restartBtn.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().name);});
			menuBtn.onClick.AddListener(() => { SceneManager.LoadScene(0);});
			exitBtn.onClick.AddListener(() => { SceneManager.LoadScene(0);});
		}

		private void DisplayScores() {
			_canvas.enabled = true;
			DisplayScore("Damage dealt", StatisticType.Damage);
			DisplayScore("Time survived", StatisticType.Time);
			DisplayScoreRatio("Accuracy", StatisticType.Hit, StatisticType.Shot );
			DisplayScore("Killed", StatisticType.Death);
		}

		private void DisplayScore(string caption, StatisticType type) {
			var score = Instantiate(scoreViewPrefab, Vector3.zero, Quaternion.identity, scoreRoot);
			score.Caption = caption;
			score.Value = Statistics.Stats[type].Value.ToString();
		}
		
		private void DisplayScoreRatio(string caption, StatisticType typeA, StatisticType typeB) {
			var score = Instantiate(scoreViewPrefab, Vector3.zero, Quaternion.identity, scoreRoot);
			score.Caption = caption;
			var valueA = Convert.ToSingle(Statistics.Stats[typeA].Value.ToString());
			var valueB = Convert.ToSingle(Statistics.Stats[typeB].Value.ToString());
			var result = Math.Abs(valueB) < float.Epsilon ? 0 : valueA / valueB;
			
			score.Value = result.ToString("P1");
		}
	}
}