using UnityEngine;
using System.Collections;
using System;

namespace YupiStudios.Luna.InGame {

	//[RequireComponent(typeof(Collider2D))]
	public class ColorableObject : MonoBehaviour, IComparable {

		protected const float CHANGE_TIME = 1.3f;
		protected const float CHANGE_DELAY = 0.1f; // 0.8

		protected TintLogic currentTint;
		protected Color currentColor = Color.white;
		protected Color lastColor = Color.white;
		protected float colorChangeTime = 0.0f;

		public TintLogic GetCurrentTint()
		{
			return currentTint;
		}

		public Color GetColor()
		{
			return currentColor;
		}

		public virtual void SetColor (Color newColor)
		{
			currentColor = newColor;
			SpriteRenderer sp = (GetComponent<Renderer>() as SpriteRenderer);
			if (sp != null)
				sp.color = currentColor;
		}

		public virtual void SetTint(TintLogic t)
		{
			currentTint = t;
			lastColor = currentColor;
			currentColor = t.tintColor;
			colorChangeTime = 0.0f;
		}

		protected virtual void Paint()
		{
			SpriteRenderer sp = (GetComponent<Renderer>() as SpriteRenderer);
			
			if (sp.color != currentColor) {
				colorChangeTime += Time.deltaTime;
				if (colorChangeTime < CHANGE_TIME + CHANGE_DELAY)
				{
					float currentPos = (colorChangeTime - CHANGE_DELAY)/CHANGE_TIME;
					if (currentPos > 0)
					{
						sp.color = Color.Lerp(lastColor,currentColor,currentPos);
					}
				}
				else
				{
					sp.color = currentColor; 
				}
			}
		}

		public int CompareTo (object obj)
		{
			return this.transform.position.z.CompareTo (((ColorableObject)obj).transform.position.z);
		}
		
		public virtual bool IsEmptyOnMousePos()
		{
			Vector2 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			if (GetComponent<Collider2D>() == null) {

					
					Bounds b = (GetComponent<Renderer>() as SpriteRenderer).bounds;
					
					Vector2 v = new Vector2( (mouse.x - b.min.x) / ((float)b.size.x) , (mouse.y - b.min.y) / ((float)b.size.y) );

					if (v.x < 0 || v.x > 1 || v.y < 0 || v.y > 1)
						return true;
					else
						return false;
			}
			else
				return !GetComponent<Collider2D>().OverlapPoint (mouse);	
		}



		void Update()
		{
			Paint ();
		}

	}

}