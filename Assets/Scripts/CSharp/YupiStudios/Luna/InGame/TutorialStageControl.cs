using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame {

	public class TutorialStageControl : StageControl {

		public GameObject animTut1, animTut2, animTut3;
		public TintLogic mixTint1, mixEnd1, mixTint3, mixEnd3, mixTint5, mixEnd5;

		private void AtivateAnim (int num)
		{
			switch (num) {
			case 2:
				animTut1.SetActive(false);
				animTut2.SetActive(true);
				animTut3.SetActive(false);
				break;
			case 3:
				animTut1.SetActive(false);
				animTut2.SetActive(false);
				animTut3.SetActive(true);
				break;
			default:
				animTut1.SetActive(true);
				animTut2.SetActive(false);
				animTut3.SetActive(false);
				break;
			}
		}

		private void DeactivateAnims()
		{
			animTut1.SetActive(false);
			animTut2.SetActive(false);
			animTut3.SetActive(false);
		}

		private void ShowOffItem1()
		{
			if (slotMixPigment.tintRef != mixTint1)
			{
				AtivateAnim (1);
			}
			else
			{
				AtivateAnim (2);
			}
		}

		private void ShowOffItem3()
		{
			if (slotMixPigment.tintRef != mixTint3)
			{
				AtivateAnim (1);
			}
			else
			{
				AtivateAnim (3);
			}
		}

		private void ShowOffItem5()
		{
			if (slotMixPigment.tintRef != mixTint5)
			{
				AtivateAnim (2);
			}
			else
			{
				AtivateAnim (3);
			}
		}

		protected override void UpdateShowOff(TintLogic t)
		{
			base.UpdateShowOff (t);

			switch (currentItem) {
			case 1:
				if (slotMixPigment.tintRef != mixEnd1)
					ShowOffItem1();
				else
					DeactivateAnims();
				break;
			case 3:
				if (slotMixPigment.tintRef != mixEnd3)
					ShowOffItem3();
				else
					DeactivateAnims();
				break;
			case 5:
				if (slotMixPigment.tintRef != mixEnd5)
					ShowOffItem5();
				else
					DeactivateAnims();
				break;
			default:
				DeactivateAnims();
				break;
			}
		}

		protected override void UpdateItem(bool toLeft, int newItem, int last)
		{
			base.UpdateItem (toLeft, newItem, last);

			if (newItem % 2 == 0) {
				SetMixPigment(stageItems[newItem].exitTints[0]);
				UpdateShowOff(stageItems[newItem].exitTints[0]);
			}
		}

	}

}