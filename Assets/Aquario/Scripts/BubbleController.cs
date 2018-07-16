using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class BubbleController : MonoBehaviour {
        public ParticleSystem Stream;
        public ParticleSystem Jet;
		public AudioClip Bubbling;

        public float longPressDuration = 1f;
        private float longPressTimer = 0f;

        private bool secondPress = false;
        
        // Use this for initialization
        void Start() {
			GetComponent<AudioSource> ().loop = true;
			GetComponent<AudioSource> ().clip = Bubbling;
        }

        // Update is called once per frame
        void Update() {
            if (GameController.IsPlaying) {
                if (Input.GetMouseButtonUp(0) && secondPress) {
                    if (longPressTimer < longPressDuration) {
                        SetRotation();
                        Jet.Play();
                    }
                }

                if (Input.GetMouseButton(0) && secondPress) {
                    longPressTimer += Time.deltaTime;
					if (!GetComponent<AudioSource> ().isPlaying) {
						GetComponent<AudioSource> ().Play ();
					}

                    if (longPressTimer > longPressDuration) {
                        SetRotation();
                        if (!Stream.isPlaying) {
                            Stream.Play();
							GetComponent<AudioSource> ().Play ();
                        }
                    }
                } else {
                    longPressTimer = 0f;
                    Stream.Stop();
					GetComponent<AudioSource> ().Stop ();
                }

                if (Input.GetMouseButtonUp(0) && !secondPress) {
                    secondPress = true;
                }
            }
        }

        private void SetRotation() {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;

            Vector2 v = (Physics2D.gravity - (Vector2)transform.position) + (Vector2)transform.position;
            transform.rotation = Quaternion.FromToRotation((Vector3)Vector2.down, (Vector3)v);
        }
    }
}


