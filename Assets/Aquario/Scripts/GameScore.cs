using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace YupiPlay.Luna.Aquario {
    public abstract class GameScore : MonoBehaviour  {
        public Text ScoreText;
        
        protected int Score = 0;

        public void Add(int amount) {
            Score += amount;

            UpdateText();
        }

        public void Subtract(int amount) {
            int newScore = Score + amount;

            if (newScore < 0) {
                Score = 0;                
            } else {
                Score = newScore;
            }

            UpdateText();
        }

        public void Reset() {
            Score = 0;

            UpdateText();
        }

        public void Set(int amount) {
            Score = amount;

            UpdateText();
        }

        protected void UpdateText() {
            ScoreText.text = Score.ToString();
        }
    }
}
