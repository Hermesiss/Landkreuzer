using System;
using UnityEngine;

namespace Landkreuzer.Behaviours.UI {
	[RequireComponent(typeof(Canvas))]
	public class UiController : MonoBehaviour {
		[SerializeField] private RectTransform scoreRoot;
		[SerializeField] private ScoreView scoreViewPrefab;

		private Canvas _canvas;

		private void Awake() {
			_canvas = GetComponent<Canvas>();
			_canvas.enabled = false;
			Overseer.OnGameOver.AddListener(DisplayScores);
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
			score.Value = (valueA/valueB).ToString("P1");
		}
	}
}