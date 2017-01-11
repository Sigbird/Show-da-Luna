using UnityEngine;
using System.Collections;


namespace YupiStudios.Luna.InGame {

	[RequireComponent(typeof(Animator))]
	public class ItemControl : MonoBehaviour {

		public TintLogic []exitTints;

		public float Correctness {
			get;
			set;
		}

		private Animator animator;

		private int animExitRightHash, animExitLeftHash, animEnterRightHash, animEnterLeftHash;

		private ColorableObject[] colorableObjects;	

		public void AppearFromLeft()
		{
			animator.enabled = true;
			animator.Play(animEnterRightHash);
		}

		public void AppearFromRight()
		{
			animator.enabled = true;
			animator.Play(animEnterLeftHash);
		}

		public float GetAnimatorTimeRef()
		{
			return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		}

		public void ExitToRight()
		{
			animator.enabled = true;
			animator.Play(animExitRightHash);
		}

		public void ExitToLeft()
		{
			animator.enabled = true;
			animator.Play(animExitLeftHash);
		}

		private void UpdateColorableObjects()
		{
			colorableObjects = GetComponentsInChildren<ColorableObject>();
			System.Array.Sort<ColorableObject> (colorableObjects);
		}

		public ColorableObject []GetColorableObjects()
		{
			return colorableObjects;
		}

		void Awake () {
			animator = GetComponent<Animator>();
			animExitRightHash = Animator.StringToHash("ItemExitToRight");
			animExitLeftHash = Animator.StringToHash("ItemExitToLeft");
			animEnterRightHash = Animator.StringToHash("ItemAppearFromLeft");
			animEnterLeftHash = Animator.StringToHash("ItemAppearFromRight");
			UpdateColorableObjects();
			Correctness = 0;
			animator.enabled = false;
			gameObject.SetActive(false);
		}

	}

}