using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using YupiStudios.Luna.Menu;

namespace YupiStudios.Luna.InGame {

	public class StageControl : TintMixControl {

		private const float WAIT_TO_NEXT_ITEM = 2.2f;
		private const float SPEED = 6.0f;

		public ItemControl[] stageItems;

		public GameObject canvasLeftButton;

		public GameObject canvasRightButton;

		public NextStageGaugeLogic nextStageGauge;

		public AudioSource victorySound1; 

		public AudioSource victorySound2;

		public GameObject caseObject;

		public int stageNum;

		public AudioSource exitAudio;

		protected int currentItem;

		private bool isNextColorMix = false;

		private Color colorResult;

		private TintLogic []exitTints;

		private int exitTintsLeft;

		private bool end = false;

		public ColorableObject stageObject;

		private void ResetSlotMixShowOff()
		{
			(slotMixEmpty.GetComponent<Renderer>() as SpriteRenderer).color = Color.white;
			slotMixPigment.SetShowOff(false);
			isNextColorMix = false;
		}

		private void ResetShowOff()
		{
			ResetSlotMixShowOff ();
			foreach (TintLogic t in allTints)
			{
				t.SetShowOff(false);
			}
		}

		protected virtual void UpdateShowOff(TintLogic t)
		{
			if (t.tintColor != colorResult)
			{
				colorResult = t.tintColor;
				ResetTintMix();
			}

			ResetShowOff();

			t.SetShowOff(true);


			if (t.tintRef == slotMixPigment.tintRef)
			{
				slotMixPigment.SetShowOff(true);
				return;
			}
			else
			{
				slotMixPigment.SetShowOff(false);
			}


			isNextColorMix = false;
			(slotMixEmpty.GetComponent<Renderer>() as SpriteRenderer).color = Color.white;
			
			foreach (MixTint mix in mixTints)
			{
				if (mix.tintResult == t)
				{
					mix.tint1.SetShowOff(true);
					mix.tint2.SetShowOff(true);
					isNextColorMix = true;
					colorResult = t.tintColor;
					break;
				}
			}
		}

		private float GetGaugeValue()
		{
			int total = stageItems.Length;
			if (total > 0) {
				float correctness = 0.0f;
				foreach (ItemControl item in stageItems) {
					correctness += item.Correctness;
				}
				return (correctness / total);
			} else {
				return 0;
			}
		}

		private void UpdateGauge()
		{
			int total = stageItems.Length;
			if (total > 0) {
				float gaugeValue = GetGaugeValue();
				nextStageGauge.SetGaugeValue (gaugeValue);
				if (gaugeValue == 1.0f)
				{
					DisablePainting();
					canvasLeftButton.SetActive(false);
					canvasRightButton.SetActive(false);
					caseObject.SetActive(false);
				}
			}
		}

		protected List<TintLogic> RemainingTintsToPaint()
		{
			List<TintLogic> tintsToFind = new List<TintLogic> (exitTints);
			
			foreach (ColorableObject obj in colorableObjects)
			{
				if (tintsToFind.Count == 0)
					break;
				
				TintLogic objTint = obj.GetCurrentTint();
				tintsToFind.Remove(objTint);
			}

			stageItems[currentItem].Correctness = 1.0f - (tintsToFind.Count / (float)exitTints.Length);

			UpdateGauge ();

			return tintsToFind;
		}

		private bool TestItemEnd(string tint_name)
		{
			List<TintLogic> tintsToFind =  RemainingTintsToPaint();

			if (exitTintsLeft < tintsToFind.Count) {
				YupiStudios.Analytics.YupiAnalyticsEventHandler.InGameEvent ("PaintError", tint_name, currentItem);
			} else if (exitTintsLeft > tintsToFind.Count)
			{
				victorySound1.Play();
			}

			exitTintsLeft = tintsToFind.Count;

			if (exitTintsLeft > 0)
			{
				UpdateShowOff(tintsToFind[0]);
			}
			else
			{
				ResetShowOff();
			}

			if (exitTintsLeft == 0)
				return true;
			else
				return false;

		}

		public bool TryToColorStageObjects(TintLogic tint)
		{
			if (stageObject != null) {
				if (!stageObject.IsEmptyOnMousePos ()) {
					if (stageObject.GetCurrentTint () != tint.tintRef) {
						stageObject.SetTint (tint.tintRef);
						return true;
					}
				}
			}
			return false;
		}

		public override bool TryToColor(TintLogic tint)
		{
			if (tint.tintRef == null) {
				Debug.LogWarning ("no tint ref");
				return false;
			}

			bool tintChange = false;

			if (TryToColorStageObjects (tint)) {
				return true;
			}
			
			ColorableObject obj = GetObjectInMouse();
			if (obj != null)
			{
				tint.SetShowOff(false);
				tint.tintRef.SetShowOff(false);

				if (obj.GetCurrentTint() != tint.tintRef)
				{
					obj.SetTint(tint.tintRef);
					tintChange = true;
					if (TestItemEnd(tint.tintRef.gameObject.name))
						EndItem();
				}
			}


			
			return tintChange;
		}

		public override TintLogic TryToMix(TintLogic tint)
		{
			TintLogic tintMix = base.TryToMix(tint);
			slotMixPigment.SetShowOff(false);
			List<TintLogic> tintsToPaint = RemainingTintsToPaint();
			if (tintsToPaint.Count > 0)
				UpdateShowOff(tintsToPaint[0]);
			return tintMix;
		}

		private void SetExitTints(int newItem)
		{
			exitTints = stageItems[newItem].exitTints;

			List<TintLogic> tintsToFind =  RemainingTintsToPaint();
			exitTintsLeft = tintsToFind.Count;

			if (exitTintsLeft > 0)
				UpdateShowOff(tintsToFind[0]);
			else
				ResetShowOff();
		}

		protected virtual void UpdateItem(bool toLeft, int newItem, int last)
		{
			ResetTintMix();

			currentItem = newItem;

			if (newItem > 0)
				canvasLeftButton.SetActive(true);			
			else
				canvasLeftButton.SetActive(false);			

			if (newItem < stageItems.Length - 1)
				canvasRightButton.SetActive(true);
			else
				canvasRightButton.SetActive(false);

			// Previous Item
			if (newItem != last) {
				if (toLeft) {
						stageItems [last].ExitToLeft ();
				} else {
						stageItems [last].ExitToRight ();
				}
			}

			ActivateItem(stageItems[newItem]);
			SetColorableObjects(stageItems[newItem].GetColorableObjects());
			SetExitTints(newItem);

			if (toLeft)
			{
				stageItems[newItem].transform.position = new Vector3(0,0,0);
				stageItems[newItem].AppearFromRight();
			}
			else
			{ 
				stageItems[newItem].transform.position = new Vector3(0,0,0);
				stageItems[newItem].AppearFromLeft();
			}
		}

		public void EnablePaintingCallBack()
		{
			EnablePainting ();
		}

		public void UpdateNextCallback()
		{
			victorySound2.Play ();
			UpdateItem(true,currentItem+1,currentItem);
			EnablePainting ();
		}

		private void EndItem()
		{
			if (currentItem < stageItems.Length - 1) {
				DisablePainting();
				Invoke("UpdateNextCallback",WAIT_TO_NEXT_ITEM);
			}
		}

		private void CancelNextCallback()
		{
			CancelInvoke ("UpdateNextCallback");
			EnablePainting ();
		}

		public void NextItem()
		{
			if (!end && currentItem < stageItems.Length-1)
			{
				UpdateItem(true,currentItem+1,currentItem);
			}
			CancelNextCallback ();
		}

		public void PreviousItem()
		{
			if (!end && currentItem > 0)
			{
				UpdateItem(false,currentItem-1,currentItem);
			}
			CancelNextCallback ();
		}

		private void ExitCallBack()
		{
			Application.LoadLevel("Menu");
		}

		private void CompleteStage()
		{
			if (MenuLogic.GetCurrentStage () == stageNum) {
				MenuLogic.SetCurrentStage (stageNum + 1);
				MenuLogic.SetNewItem (stageNum);
			}
			
			if (stageNum < 4)
				Application.LoadLevel ("Menu");
			else
				Application.LoadLevel ("GameEnd");
		}
		
		public void ExitToMenu () {
			if (end)
				return;

			DisablePainting ();
			end = true;

			if (GetGaugeValue () == 1.0f) {
				CompleteStage ();
			} else {
				exitAudio.Play ();
			}

			Invoke ("ExitCallBack",EXIT_DELAY);
		}

		protected void Update()
		{
			if (isNextColorMix)
			{
				float pos = (1 + Mathf.Cos(Time.realtimeSinceStartup*SPEED)) / 2.0f;
				(slotMixEmpty.GetComponent<Renderer>() as SpriteRenderer).color = Color.Lerp(Color.white,colorResult,pos);
			}
		}

		// Use this for initialization
		public override void Start () 
		{
            base.Start();
			currentItem = 0;
			UpdateItem(true,0,0);
		}


	}

}