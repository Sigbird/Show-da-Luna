using UnityEngine;
using System.Collections;

using YupiStudios.Luna.Menu;

namespace YupiStudios.Luna.InGame {

	public class NextStageGaugeLogic : MonoBehaviour {

		public StageGauge gauge;
		public GameObject enableButton;
		public GameObject disableButton;
		public GameObject stageShow;

		private float gaugeValue;

		public void SetGaugeValue (float value)
		{
			gaugeValue = value;
			gauge.SetGaugeValue (gaugeValue);
			/*if (gaugeValue >= 1 && !enableButton.activeSelf) {
				enableButton.SetActive (true);
				disableButton.SetActive (false);
				stageShow.SetActive(true);
			} else if (enableButton.activeSelf) {
				enableButton.SetActive (false);
				disableButton.SetActive (true);
				stageShow.SetActive(false);
			}*/
		}

		void Update() 
		{
			if (gauge.IsGaugeFull ()) {
				enableButton.SetActive (true);
				disableButton.SetActive (false);
				stageShow.SetActive(true);
			}
		}


	}

}