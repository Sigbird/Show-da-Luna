using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace YupiPlay.Luna.Aquario 
{
    public abstract class AbstractScoreController : MonoBehaviour {

		private int unlocked;

        void OnCollisionEnter2D(Collision2D coll) {
            GameScore scoreOwner = GetScoreOwner();

			if (coll.gameObject.tag == "Pearl" && scoreOwner == PlayerScore.Instance) {
				GameController.Instance.PlayAudio (2);
				if (ProgressBar.Instance.GetComponent<Image> ().fillAmount <= 0.9f) {
					scoreOwner.Add (1);  
					ProgressBar.Instance.GetComponent<Image> ().fillAmount += 0.1f;
				} else if(unlocked <=2){
					scoreOwner.Add (1);  
					ProgressBar.Instance.GetComponent<Image> ().fillAmount = 0;
					if (unlocked <= 1) {
						GameController.Instance.UIUnlocks [unlocked].SetActive (false);
					}
					GameController.Instance.Unlocks [unlocked].SetActive (true);
					switch (unlocked) {
					case 0:
						GameController.Instance.PlayAudio (4);
						break;
					case 1:
						GameController.Instance.PlayAudio (5);
						break;
					case 2:
						GameController.Instance.PlayAudio (6);
						break;
					default:
						break;
					}
					unlocked++;
				}
            }

			if (coll.gameObject.tag == "Octopus" && scoreOwner == PlayerScore.Instance) {
				if (ProgressBar.Instance.GetComponent<Image> ().fillAmount > 0) {
					scoreOwner.Add (-1);
					ProgressBar.Instance.GetComponent<Image> ().fillAmount -= 0.1f;
					GameController.Instance.PlayAudio (3);
				}
			}
        }

        protected abstract GameScore GetScoreOwner();
    }

}
