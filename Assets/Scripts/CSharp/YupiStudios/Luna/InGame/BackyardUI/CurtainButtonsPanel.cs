using UnityEngine;
using System.Collections;

using UnityEngine.UI;

namespace YupiStudios.Luna.InGame.BackyardUI {

	public class CurtainButtonsPanel : MonoBehaviour {
		private const float FADEIN_SPEED = 1.0f; 
		private const float FADEOUT_SPEED = 3.0f;

		private Outline []buttonOutlines;
		private Image []buttonImages;

		private Color buttonsColor;

		private bool isFadeIn;

		
		public void ButtonsFadeIn()
		{
			if (!isFadeIn) {
				isFadeIn = true;
				EnableButtons ();
				TurnButtonsOn();
			}
		}
		
		public void ButtonsFadeOut()
		{
			if (isFadeIn) {
				isFadeIn = false;
				TurnButtonsOff ();
				TurnOutlineOff();
			}
		}

		private void TurnOutlineOn()
		{			
			foreach (Outline oLine in buttonOutlines) {
				if (oLine.enabled)
				{
					Color c = oLine.effectColor;
					c.a = 1;
					oLine.effectColor = c;
					break;
				}
			}		
		}

		private void TurnOutlineOff()
		{			
			foreach (Outline oLine in buttonOutlines) {
				if (oLine.enabled)
				{
					Color c = oLine.effectColor;
					c.a = 0;
					oLine.effectColor = c;
					break;
				}
			}		
		}

		private void TurnButtonsOn() 
		{
			foreach (Outline oLine in buttonOutlines) {
				Button b = oLine.gameObject.GetComponent<Button>();
				b.interactable = true;
			}
		}

		private void TurnButtonsOff() 
		{
			foreach (Outline oLine in buttonOutlines) {
				Button b = oLine.gameObject.GetComponent<Button>();
				b.interactable = false;
			}
		}

		private void EnableButtons() 
		{
			foreach (Outline oLine in buttonOutlines) {
				oLine.gameObject.SetActive(true);
			}
		}

		private void DisableButtons() 
		{
			foreach (Outline oLine in buttonOutlines) {
				oLine.gameObject.SetActive(false);
			}
		}

		private void UpdateColor()
		{
			foreach (Image image in buttonImages) {
				image.color = buttonsColor;
			}
		}

		private void ChangeOutline(Outline obj)
		{
			foreach (Outline oLine in buttonOutlines) {
				if (oLine != obj)
					oLine.enabled = false;
				else
					oLine.enabled = true;
			}
		}

		// Use this for initialization
		void Start () {
			buttonOutlines = GetComponentsInChildren<Outline> ();
			buttonImages = GetComponentsInChildren<Image> ();
			DisableButtons ();
			buttonsColor = Color.white;
			buttonsColor.a = 0.0f;
		}
		
		// Update is called once per frame
		void Update () {

			if (isFadeIn && buttonsColor.a != 1) {
				if (buttonsColor.a < 1.0f)
					buttonsColor.a += Time.deltaTime * FADEIN_SPEED;

				if (buttonsColor.a >= 0.95)
				{
					buttonsColor.a = 1;
					EnableButtons();
					TurnButtonsOn();
					TurnOutlineOn();
				}
				UpdateColor();

			}

			if (!isFadeIn && buttonsColor.a != 0) {
				if (buttonsColor.a > 0.0f)
					buttonsColor.a -= Time.deltaTime * FADEOUT_SPEED;

				if (buttonsColor.a <= 0)
				{
					buttonsColor.a = 0;
					DisableButtons ();
				}
				UpdateColor();
			}
		
		}
	}


}