using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario {
    public class ChangeDirectionBehaviour : SwimmerBaseBehaviour {             
        public void InvertXScale()
        {            
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
        
        public void ChangeDirection()
        {
            InvertXScale();  
            if ((int) transform.localScale.x == 1)
            {                
                rb.AddRelativeForce(Vector2.right * SwimForce);           
            }
            else
            {                
                rb.AddRelativeForce(Vector2.left * SwimForce);           
            }
        }        

        protected override void BehaviourAction() {
            int random = Random.Range(0, 2);

            if (random > 0) {
                ChangeDirection();
            }
        }
      
    }
}
