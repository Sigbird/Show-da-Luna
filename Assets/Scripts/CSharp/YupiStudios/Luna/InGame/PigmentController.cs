using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame {

	public class PigmentController : MonoBehaviour {

		public SpriteRenderer []colorSegments;

		public Animator animator;

		private int stateJump;
		private int stateIdle;
		private int statePicked;

		public void SetColor (Color c)
		{
			foreach (SpriteRenderer spRenderer in colorSegments)
			{
				spRenderer.color = c;
			}
		}

		public void Jump()
		{
			animator.Play(stateJump);
		}

		public void Idle()
		{
			animator.Play(stateIdle);
		}

		public void Picked()
		{
			animator.Play(statePicked);
		}

		void Awake()
		{
			stateJump = Animator.StringToHash("jump");
			stateIdle = Animator.StringToHash("idle");
			statePicked = Animator.StringToHash("picked");
			Idle();
		}


	}

}
