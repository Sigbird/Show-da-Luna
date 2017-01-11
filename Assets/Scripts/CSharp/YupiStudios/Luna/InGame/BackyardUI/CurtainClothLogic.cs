using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame.BackyardUI {

	public class CurtainClothLogic : MonoBehaviour {


		private const float CURTAIN_SPEED = 5.0f;
		private const float CURTAIN_CHANGE_THRESHOLD = 0.4f;
		private const float CURTAIN_FIX = 0.02f;
		private const float WAIT_TO_FORCE = 0f;

		public float triggerPosition;
		public float closePosition;
		public float openPosition;
		public GameObject disableOnClosed;

		public CurtainLogic curtain;

		public CurtainButtonsPanel panel;

		public bool isOpened;

		public GameObject uiCloseButton;
		public GameObject uiOpenButton;
		public GameObject uiScreenShot;

		private Vector3 Position
		{
			get 
			{
				return transform.localPosition;
			}
			set
			{
				transform.localPosition = value;
			}
		}


		private float startClosingPos;
		private float startOpeningPos;
		private float posDiff;
		private bool moveToOpen;

		private bool forceOpen;
		private float forceWaitTime; 

		bool forceClose;

		private void ProcessClosed()
		{
			Vector3 pos = Position;
			float percent = Mathf.Abs ( (pos.x - openPosition) / posDiff );
			if (pos.x < startOpeningPos) {
				if (pos.x > openPosition && percent > CURTAIN_FIX) {
					pos.x += (openPosition - pos.x) * CURTAIN_SPEED * Time.deltaTime;
				} else {
					pos.x = openPosition;
					moveToOpen = true;
				}

			} else {
				if (pos.x < closePosition && percent < 1-CURTAIN_FIX) {
					pos.x += (closePosition - pos.x) * CURTAIN_SPEED * Time.deltaTime;					 
				} else {
					pos.x = closePosition;
					panel.ButtonsFadeIn();
				}
			}		

			Position = pos; 
		}

		private void ProcessForceOpen()
		{
			Vector3 pos = Position;

			if (pos.x > openPosition) {
				pos.x += (openPosition - pos.x) * CURTAIN_SPEED * Time.deltaTime;					 
			} else {
				pos.x = openPosition;
				forceOpen = false;
			}
			
			Position = pos;
		}

		private void ProcessForceClose()
		{
			Vector3 pos = Position;

			if (pos.x < closePosition) {
				pos.x += (closePosition - pos.x) * CURTAIN_SPEED * Time.deltaTime;					 
			} else {
				pos.x = closePosition;
				forceClose = false;
			}
			
			Position = pos;
		}

		private void ProcessOpened()
		{
			Vector3 pos = Position;
			float percent = Mathf.Abs ( (pos.x - openPosition) / (closePosition - openPosition) );
			if (pos.x > startClosingPos) {
				if (pos.x < closePosition && percent < 1 - CURTAIN_FIX) {
					pos.x += (closePosition - pos.x) * CURTAIN_SPEED * Time.deltaTime;	
				} else {
					pos.x = closePosition;
					moveToOpen = false;
				}
			} else {
				if (pos.x > openPosition && percent > CURTAIN_FIX) {
					pos.x += (openPosition - pos.x) * CURTAIN_SPEED * Time.deltaTime;					 
				} else {
					pos.x = openPosition;
				}
			}

			Position = pos;
		}

		public void ForceOpen()
		{
			forceClose = false;
			forceOpen = true;
			forceWaitTime = 0;
		}

		public void ForceClose()
		{
			forceOpen = false;
			forceClose = true;
			forceWaitTime = 0;
		}

		public bool HasNewContent()
		{
			for (int i = 0; i < 5; ++i)
			{
				if (MenuLogic.GetNewItem (i))
					return true;
			}

			return false;
		}

		private void ActiveUIClose()
		{
			if (!uiCloseButton.activeSelf) {
				uiScreenShot.SetActive (true);
				uiCloseButton.SetActive (true);
				uiOpenButton.SetActive (false);
			}
		}

		private void ActiveUIOpen()
		{
			if (!uiOpenButton.activeSelf) {
				uiCloseButton.SetActive (false);
				uiScreenShot.SetActive (false);
				uiOpenButton.SetActive (true);
			}
		}

		private void DeActiveUIOpenClose()
		{
			if (uiCloseButton.activeSelf || uiOpenButton.activeSelf) {
				uiCloseButton.SetActive (false);
				uiOpenButton.SetActive (false);
				uiScreenShot.SetActive (false);
			}
		}


		void Start ()
		{
			moveToOpen = true;
			
			startClosingPos = Mathf.Lerp (openPosition, closePosition, CURTAIN_CHANGE_THRESHOLD);
			startOpeningPos = Mathf.Lerp (closePosition, openPosition, CURTAIN_CHANGE_THRESHOLD);

			posDiff = (closePosition - openPosition);
		}

		void Awake(){
			if (HasNewContent ()) {
				ForceClose ();
			}
		}


		
		// Update is called once per frame
		void Update () {
			
			if (Position.x > triggerPosition)
			{
				disableOnClosed.SetActive(false);
			}
			else
			{
				disableOnClosed.SetActive(true);
			}

			bool reachClosePos = Position.x >= closePosition - posDiff * 0.05f;
			bool reachOpenPos = Position.x <= openPosition + posDiff * 0.05f;

			if ( reachClosePos ) {
				panel.ButtonsFadeIn ();
			} else {
				panel.ButtonsFadeOut ();
			}

			if (reachOpenPos) {
				ActiveUIClose ();
			} else if (reachClosePos) {
				ActiveUIOpen ();
			} else {
				DeActiveUIOpenClose();
			}

			if (reachOpenPos) {
				if (!isOpened)
				{
					isOpened = true;
					curtain.Enlarge ();
				}
			} else {
				if (isOpened) {
					isOpened = false;
					curtain.Shrink ();
				}
			}

			if (forceOpen || forceClose) {

				if (Input.GetMouseButton (0)) {
						Vector3 pos = Position;
						if (forceOpen)
								pos.x = openPosition;
						else
								pos.x = closePosition;
						Position = pos;
						forceClose = false;
						forceOpen = false;
				} else {
						if (forceOpen) {
								ProcessForceOpen ();
						} else if (forceClose) {
								if (forceWaitTime < WAIT_TO_FORCE) {
										forceWaitTime += Time.deltaTime;
								} else {
										ProcessForceClose ();
								}
						}
				}
			} else {

				if (!Input.GetMouseButton (0)) {

						if (moveToOpen) {
								ProcessOpened ();
						} else {
								ProcessClosed ();
						}

				}
			}


		
		}
	}

}
