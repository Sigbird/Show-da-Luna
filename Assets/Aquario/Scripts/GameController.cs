using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class GameController : MonoBehaviour {

        public enum States { WatingToStart, Playing, GameOver }

        public States State { get; private set; }

        public static GameController Instance;

		public GameObject[] UIUnlocks;
		public GameObject[] Unlocks;

		public AudioClip[] Audios;

        public static bool IsPlaying {
            get {
                if (Instance != null) {
                    return Instance.State == States.Playing;
                }
                return true;
            }
        }

		public void Awake() {
            State = States.WatingToStart;

            if (Instance == null) {
                Instance = this;
            }
        }

        public void GameOver() {
            State = States.GameOver;
        }

        public void Restart() {
            State = States.Playing;
        }

		public void Pause() {
			State = States.Playing;
		}

        public void ChangeStateToPlaying() {
            State = States.Playing;
        }

		public void PlayAudio(int x) {
			GetComponent<AudioSource> ().PlayOneShot (Audios [x]);
		}

        // Use this for initialization
        void Start() {
			GetComponent<AudioSource> ().clip = Audios [0];
			GetComponent<AudioSource> ().Play ();
			GetComponent<AudioSource> ().loop = true;
        }

        // Update is called once per frame
        void Update() {

        }

       
    }
}
