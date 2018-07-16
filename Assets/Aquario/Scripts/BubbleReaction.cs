using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class BubbleReaction : MonoBehaviour
    {
        private Rigidbody2D rb;

        public float JetMultiplier = 2f;
        public float StreamMultiplier = 1f;
        public float Force = 2f;
        // Use this for initialization
        void Start() {
            rb = GetComponent<Rigidbody2D>();
        }      

        //attached to the fish, the GameObject parameters are the particles
        void OnParticleCollision(GameObject particles)
        {
            //Vector2 pushDirection = (transform.position - gameObject.transform.position).normalized;
            //Vector2 againstGravity = -Physics2D.gravity.normalized;
            //Vector2 pushDirection = (-Physics2D.gravity - (Vector2) transform.position).normalized;


            //if (againstGravity.x > 0 && pushDirection.x < 0)
            //{
            //    pushDirection.x = -pushDirection.x;
            //}
            //if (againstGravity.y > 0 && pushDirection.y < 0)
            //{
            //    pushDirection.y = -pushDirection.y;
            //}           

            switch(particles.tag) {
                case "BubbleJet":
                case "BubbleStream":
                    Push(particles.tag);
                    break;
                case "BubbleRadialStream":
                case "BubbleRadialJet":
                    PushRadial(particles.tag, particles.transform.position);
                    break;
            }                        
                      
            //if (gameObject.tag == "BubbleJet")
            //{
            //    Push(pushDirection, JetMultiplier);
            //}
            //if (gameObject.tag == "BubbleStream")
            //{
            //    Push(pushDirection, StreamMultiplier);
            //}           
        }

        void Push(string tag) {
            Vector2 pushDirection = (-Physics2D.gravity - (Vector2)transform.position).normalized;

            float multiplier = StreamMultiplier;
            if (tag == "BubbleJet") multiplier = JetMultiplier;

            rb.AddForce(pushDirection * Force * multiplier);
			if (pushDirection.x < 0 && this.gameObject.tag == "Player") {
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, 1f);
			} else if(this.gameObject.tag == "Player") {
				transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, 1f);
			}
        }

        void PushRadial(string tag, Vector3 particlePos) {
            Vector2 pushDirection = (transform.position - particlePos).normalized;
            
            float multiplier = StreamMultiplier;
            if (tag == "BubbleRadialJet") multiplier = JetMultiplier;
            rb.AddForce(pushDirection * Force * multiplier);
			if (pushDirection.x < 0 && this.gameObject.tag == "Player") {
				transform.localScale = new Vector3 (-1, transform.localScale.y, 1f);
			} else if(this.gameObject.tag == "Player") {
				transform.localScale = new Vector3 (1, transform.localScale.y, 1f);
			}
        }
        
    }
}
