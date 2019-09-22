using UnityEngine;
using UnityEngine.UI;

namespace Landkreuzer.Behaviours.UI {
	public class ScoreView : MonoBehaviour {
		public Text CaptionTxt {
			get => captionTxt;
			set => captionTxt = value;
		}

		public Text ValueTxt {
			get => valueTxt;
			set => valueTxt = value;
		}

		public string Caption {
			get => caption;
			set {
				caption = value;
				captionTxt.text = caption;
			}
		}

		public string Value {
			get => value;
			set {
				this.value = value;
				valueTxt.text = this.value;
			}
		}

		[SerializeField] private Text captionTxt;

		[SerializeField] private Text valueTxt;

		[SerializeField] private string caption;

		[SerializeField] private string value;

		private void Awake() {
			captionTxt.text = caption;
			valueTxt.text = value;
		}
	}
}