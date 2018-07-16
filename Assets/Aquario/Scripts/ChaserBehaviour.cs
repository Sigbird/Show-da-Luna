using System;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
	[RequireComponent(typeof(DownToGravityBehaviour))]
	[RequireComponent(typeof(Collider2D))]	
	[RequireComponent(typeof(ChangeDirectionBehaviour))]
	public class ChaserBehaviour : SwimmerBaseBehaviour {
		public bool CanChase = true;	
		public float ChaseDuration = 5f;
		public float ChaseCooldown = 5f;
		public string[]	TagsToChase;

        private Vector2 chaseDirection;
		private Vector2 chasedPosition;
		private DownToGravityBehaviour myGravity;
		private ChangeDirectionBehaviour myDirection;
        private Animator animator;
        private LineRenderer lineRenderer;

		void Awake() {
			myGravity    = GetComponent<DownToGravityBehaviour>();
			myDirection  = GetComponent<ChangeDirectionBehaviour>();
            animator     = GetComponent<Animator>();            
		}

		override protected void ChaseBehaviourAction() {	
			var position   = (Vector2) transform.position;
            chaseDirection = chasedPosition - position;

            //isso é obtido através da equação linear, a determinante da matriz é uma abstração coincidente
            var determinant = VectorUtil.MatrixDeterminant(myGravity.ToGroundBelow - position,
                chaseDirection);
            
			//Debug.Log(determinant.ToString("F2"));
			//DebugValue.Show("Det", determinant.ToString("F2"));

			//if target is to the right
			if (determinant > 0 && transform.localScale.x < 0) {				
				myDirection.InvertXScale();				
			} else if (determinant < 0 && transform.localScale.x > 0) {
				myDirection.InvertXScale();
			}            
           
            rb.AddForce(chaseDirection.normalized * SwimForce);			
		}		

		bool IsChaseable(string tag) {
			if (Array.Exists(TagsToChase, tagToChase => tagToChase == tag)) {
				return true;
			}			

			return false;
		}
		
		void OnTriggerEnter2D(Collider2D other) {
			if (!enabled) return;

            if (GameController.IsPlaying) {
                if (IsChaseable(other.tag) && CanChase) {
                    stateController.State = SwimmerStates.Chasing;
                    animator.SetBool("IsChasing", true);
                }
            }			
		}
		
		void OnTriggerStay2D(Collider2D other) {
			if (!enabled) return;

            if (GameController.IsPlaying) {
                if (IsChaseable(other.tag) && CanChase) {
                    chasedPosition        = other.transform.position;
                    stateController.State = SwimmerStates.Chasing;

                    animator.SetBool("IsChasing", true);                  
                }
            }			
		}
		
		void OnTriggerExit2D(Collider2D other) { 
			if (!enabled) return;

            if (GameController.IsPlaying) {
                if (IsChaseable(other.tag)) {
                    stateController.State = SwimmerStates.Roaming;
                    
                    animator.SetBool("IsChasing", false);                    
                }
            }			
		}

	}

}
