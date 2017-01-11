using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame {


	public class TintMixControl : MonoBehaviour {

		protected const float EXIT_DELAY = 0.9f;

		[System.Serializable]
		public struct MixTint
		{
			public TintLogic tint1;
			public TintLogic tint2;
			public TintLogic tintResult;
		}
		
		public MixTint []mixTints;
        protected TintLogic[] allTints;

        protected TintLogic lastTintMix = null;
		
		public GameObject slotMixEmpty;
		public GameObject slotMixColor;
		public TintLogic slotMixPigment;

		public BoxCollider2D TintCaseCollider;
		
		protected ColorableObject[] colorableObjects;

		protected void SetColorableObjects (ColorableObject[] objects)
		{
			this.colorableObjects = objects;
		}
		
		public bool TestTints(MixTint m, TintLogic tint1, TintLogic tint2)
		{
			if (m.tint1 == tint1 && m.tint2 == tint2)
				return true;
			if (m.tint2 == tint1 && m.tint1 == tint2)
				return true;
			
			return false;
		}

		public bool IsMouseInsideCase()
		{
			Bounds boundingBox = TintCaseCollider.bounds;

			Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			mousePos.z = boundingBox.center.z;

			return TintCaseCollider.bounds.Contains(mousePos);
		}

		public TintLogic MixTints(TintLogic tint1, TintLogic tint2)
		{
			TintLogic result = null;
			
			foreach (MixTint m in mixTints)
			{
				if (TestTints ( m, tint1, tint2))
				{
					result = m.tintResult;
					break;
				}
			}

			return result;
		}

		protected void SetMixPigment(TintLogic tint)
		{
			if (lastTintMix == null)
			{
				lastTintMix = tint;
			}
			else
			{
				TintLogic result = MixTints(lastTintMix,tint);
				
				if (result == null)
				{
					lastTintMix = tint;
				}
				else
				{
					tint = result;
					lastTintMix = null;
				}
			}
			
			if (!slotMixColor.activeSelf)
			{
				SetSlotMixActive(true);
			}
			
			slotMixPigment.SetTintRef ( tint );
			(slotMixColor.GetComponent<Renderer>() as SpriteRenderer).color = tint.colorSlot;
		}
		
		public virtual TintLogic TryToMix(TintLogic tint)
		{
			if (!slotMixPigment.IsEmptyOnMousePos())
			{

				if (tint == slotMixPigment)
				{
					//ResetTintMix();
					return tint;
				}

				SetMixPigment(tint);
			}

			return tint;
		}
		
		protected ColorableObject GetObjectInMouse()
		{
			if (colorableObjects == null)
				return null;

			foreach (ColorableObject obj in colorableObjects)
			{
				if (obj.gameObject.activeInHierarchy && !obj.IsEmptyOnMousePos())
				{
					return obj;
				}
			}
			
			return null;
		}
		
		public virtual bool TryToColor(TintLogic tint)
		{			
			ColorableObject obj = GetObjectInMouse();

			if (obj != null)
				obj.SetTint (tint.tintRef);

			return (obj != null);
		}
		
		protected void ActivateItem(ItemControl item) 
		{
			item.gameObject.SetActive(true);
		}

		private void SetSlotMixActive(bool active)
		{
			slotMixEmpty.SetActive(!active);
			slotMixColor.SetActive(active);
			slotMixPigment.gameObject.SetActive(active);
		}
		
		protected void ResetTintMix()
		{
			SetSlotMixActive(false);
			lastTintMix = null;
			slotMixPigment.tintRef = null;
		}

        public void DisablePainting()
        {
            foreach (TintLogic t in allTints)
            {
                t.CanPaint = false;
            }
        }

        public void EnablePainting()
        {
            foreach (TintLogic t in allTints)
            {
                t.CanPaint = true;
            }
        }

        public virtual void Start()
        {
            allTints = GetComponentsInChildren<TintLogic>();
        }
		           

	}

}