using UnityEngine;
using System.Collections;



namespace YupiStudios.Luna.InGame.BackyardUI {

	public class CurtainButtonLogic : MonoBehaviour {

		public int stageToEnable;

		public GameObject unlockedButton; 
		public GameObject lockedButton;
		public GameObject gift;

		void Start(){
			MenuLogic.ClearNewItems ();
		}

		void Awake() 
		{
			if (MenuLogic.GetCurrentStage () > stageToEnable) {
				unlockedButton.SetActive (true);
				lockedButton.SetActive(false);

				if (MenuLogic.GetNewItem (stageToEnable))
					gift.SetActive (true);
				else
					gift.SetActive (false);

			} else {
				unlockedButton.SetActive (false);
				lockedButton.SetActive(true);
				gift.SetActive (false);
			}
		}

	}

}
