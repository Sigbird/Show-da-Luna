using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame {

	public class BackyardItemAnimController : MonoBehaviour {

		private const float MIN_IDLE_WAIT = 5.0f;
		private const float MAX_IDLE_WAIT = 18.0f;

		private const string IDLE1_NAME = "Idle1";
		private const string IDLE2_NAME = "Idle2";
		private const string PAINT1_NAME = "Paint1";

		public Animator animator;

		public float idleWait;
		private int idle1Hash;
		private int idle2Hash;
		private int paint1Hash;


		private void PlayIdle1()
		{
			animator.Play (idle1Hash);
		}

		private void PlayIdle2()
		{
			animator.Play (idle2Hash);
		}

		private void PlayPaint1()
		{
			animator.Play (paint1Hash);
		}

		public void PlayIdleAnim()
		{
			if (Random.Range (0.0f, 1.0f) > 0.5f)
				PlayIdle1 ();
			else
				PlayIdle2 ();

			idleWait = Random.Range (MIN_IDLE_WAIT, MAX_IDLE_WAIT);
		}

		public void PlayPaintAnim()
		{
			PlayPaint1();
			
			idleWait = Random.Range (MIN_IDLE_WAIT, MAX_IDLE_WAIT);
		}

		void Start ()
		{
			idle1Hash = Animator.StringToHash (IDLE1_NAME);
			idle2Hash = Animator.StringToHash (IDLE2_NAME);
			paint1Hash = Animator.StringToHash (PAINT1_NAME);
			idleWait = Random.Range (MIN_IDLE_WAIT, MAX_IDLE_WAIT);
		}
			
		// Update is called once per frame
		void Update () {

			if (idleWait > 0.0f)
			{
				idleWait -= Time.deltaTime;
			}
			else 
			{
				PlayIdleAnim();
			}
		
		}
	}

}
