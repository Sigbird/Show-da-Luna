using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame {

	[RequireComponent(typeof(SpriteRenderer))]
	public class StageGauge : MonoBehaviour {

		private enum EGaugeAnim 
		{
			Still,
			ChangingUP,
			ChangingDown,
			FillingUP,
			FillingDOWN
		}

		public const float GAUGE_SPEED_UPDATE = 1f;
		public const float GAUGE_SPEED_FILL = 0.25f;
		public const float INTERVAL = 0.3f;

		private float realGaugeValue;
		private float greenGaugeValue;
		private float previousGaugeValue;

		private float greenGaugeValueEnd;
		private float previousGaugeValueEnd;

		private EGaugeAnim anim;
		private float waitInterval;

		public bool IsGaugeFull()
		{
			return greenGaugeValue == 1.0f;
		}

		public void SetGaugeValue(float newGaugevalue)
		{
			if (newGaugevalue > 1)
				newGaugevalue = 1;
			else if (newGaugevalue < 0)
				newGaugevalue = 0;

			if (newGaugevalue == realGaugeValue)
				return;

			waitInterval = INTERVAL;

			if (newGaugevalue > realGaugeValue)
			{
				greenGaugeValue = realGaugeValue;
				previousGaugeValue = realGaugeValue;
				previousGaugeValueEnd = newGaugevalue;
				anim = EGaugeAnim.ChangingUP;
			} else
			{
				greenGaugeValueEnd = newGaugevalue;
				previousGaugeValue = realGaugeValue;
				anim = EGaugeAnim.ChangingDown;
			}

			realGaugeValue = newGaugevalue;
		}

		void Start()
		{
			previousGaugeValue = 0;
			previousGaugeValueEnd = 0;
			greenGaugeValue = 0;
			greenGaugeValueEnd = 0;
			realGaugeValue = 0;
		}

		private void AnimGauge()
		{
			switch (anim)
			{
			case EGaugeAnim.ChangingUP:
				if (previousGaugeValue < previousGaugeValueEnd)
				{
					previousGaugeValue += Time.deltaTime * GAUGE_SPEED_UPDATE;
					if (previousGaugeValue > previousGaugeValueEnd)
						previousGaugeValue = previousGaugeValueEnd;
				}
				else
				{
					anim = EGaugeAnim.FillingUP;
				}
				break;
			case EGaugeAnim.ChangingDown:
				if (greenGaugeValue > greenGaugeValueEnd)
				{
					greenGaugeValue -= Time.deltaTime * GAUGE_SPEED_UPDATE;
					if (greenGaugeValue < greenGaugeValueEnd)
						greenGaugeValue = greenGaugeValueEnd;
				}
				else
				{
					anim = EGaugeAnim.FillingDOWN;
				}
				break;
			case EGaugeAnim.FillingUP:

				if (waitInterval > 0)
				{
					waitInterval -= Time.deltaTime;
					break;
				}

				if (greenGaugeValue < realGaugeValue)
				{
					greenGaugeValue += Time.deltaTime * GAUGE_SPEED_FILL;
					if (greenGaugeValue > realGaugeValue)
						greenGaugeValue = realGaugeValue;
				}
				break;
			case EGaugeAnim.FillingDOWN:
				if (waitInterval > 0)
				{
					waitInterval -= Time.deltaTime;
					break;
				}

				if (previousGaugeValue > realGaugeValue)
				{
					previousGaugeValue -= Time.deltaTime * GAUGE_SPEED_FILL;
					if (previousGaugeValue < realGaugeValue)
						previousGaugeValue = realGaugeValue;
				}
				break;
			default:
				break;
			}
		}
		
		// Update is called once per frame
		void Update () {

			AnimGauge();

			Material m = GetComponent<Renderer>().materials[0];
			m.SetFloat("_CutOffPrevious", previousGaugeValue);
			m.SetFloat("_CutOffFinal", greenGaugeValue);		
		}
	}

}