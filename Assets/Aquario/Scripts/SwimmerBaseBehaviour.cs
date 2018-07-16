using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario 
{
	[RequireComponent(typeof(SwimmerStateController))]
	[RequireComponent(typeof(Rigidbody2D))]
	abstract public  class SwimmerBaseBehaviour : MonoBehaviour {		
		public float MinInterval = 1;
		public float MaxInterval = 4;
		public float SwimForce = 10f;
		protected Rigidbody2D rb;
		protected SwimmerStateController stateController;
		
		// Use this for initialization
		protected virtual void Start () {
			rb = GetComponent<Rigidbody2D>();
			stateController = GetComponent<SwimmerStateController>();

			StartCoroutine(BehaviourActionCoroutine());
		}

		protected IEnumerator BehaviourActionCoroutine() {
			while (true) {
				if (GameController.IsPlaying) {	
					switch (stateController.State) {
						case SwimmerStates.Roaming:
							yield return Roam();
							break;
						case SwimmerStates.Chasing:
							yield return Chase();
							break;						
					}									
				}

				yield return new WaitForEndOfFrame();
			}			
		}

		protected float GetRandomInterval() {
			return Random.Range(MinInterval, MaxInterval);
		}

		protected IEnumerator Roam() {			
			yield return new WaitForSeconds(GetRandomInterval());
			BehaviourAction();	
		}

		protected IEnumerator Chase() {
			yield return new WaitForSeconds(GetRandomInterval());
			ChaseBehaviourAction();			
		}

		virtual protected void BehaviourAction() {}

		virtual protected void ChaseBehaviourAction() {}
	}
}
