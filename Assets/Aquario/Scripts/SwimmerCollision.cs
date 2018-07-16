using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay.Luna.Aquario 
{
    [RequireComponent(typeof(ChangeDirectionBehaviour))]
    public class SwimmerCollision : MonoBehaviour {
        public int HitsToChangeDirection = 3;
        public float HitsCooldown = 5f;
        
        private int hitCounter;        
        private bool timerRunning = false;

        private DownToGravityBehaviour myGravity;
        private ChangeDirectionBehaviour myDirection;
        private Rigidbody2D rb;        

        // Use this for initialization
        void Start () {
            hitCounter = HitsToChangeDirection;

            myDirection = GetComponent<ChangeDirectionBehaviour>();
            myGravity = GetComponent<DownToGravityBehaviour>();
            rb = GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag != "Pearl") {
                --hitCounter;
                
                if (!timerRunning) StartCoroutine(HitTimeWindow());                                               

                if (hitCounter <= 0 && timerRunning) {
                    Vector2 invertedDirection = -(coll.transform.position - transform.position);
                    rb.AddRelativeForce(invertedDirection.normalized * 10f);

                    var determinant = VectorUtil
                        .MatrixDeterminant(myGravity.ToGroundBelow - (Vector2)transform.position,
                            coll.transform.position - transform.position);

                    if (determinant < 0 && transform.localScale.x < 0) {
                        myDirection.InvertXScale();
                    } else if (determinant > 0 && transform.localScale.x > 0) {
                        myDirection.InvertXScale();
                    }

                    hitCounter = HitsToChangeDirection;                                                                               
                }                                
            }
        }

        private IEnumerator HitTimeWindow() {
            timerRunning = true;

            yield return new WaitForSeconds(HitsCooldown);
            hitCounter = HitsToChangeDirection;
            timerRunning = false;
        }
        
        
    }    
}

