using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame {
	
	public class ColorableMultiObject : ColorableObject {

		private SpriteRenderer []sprites;

		public override void SetColor (Color newColor)
		{
			base.SetColor(newColor);

			foreach(SpriteRenderer spRenderer in sprites)
			{
				spRenderer.color = newColor;
			}
		}

		protected override void Paint() 
		{

			foreach(SpriteRenderer sp in sprites)
			{
				
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


		}

		public override void SetTint(TintLogic t)
		{
			base.SetTint(t);

			foreach(SpriteRenderer spRenderer in sprites)
			{
				spRenderer.color = t.tintColor;
			}
		}

		private Vector2 PixelCoordFromMousePos(SpriteRenderer spRenderer)
		{
			
			Vector2 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			
			Bounds b = spRenderer.bounds;
			
			Vector2 v = new Vector2( (mouse.x - b.min.x) / ((float)b.size.x) , (mouse.y - b.min.y) / ((float)b.size.y) );
			
			return v;
		}

		public override bool IsEmptyOnMousePos()
		{

			foreach(SpriteRenderer spRenderer in sprites)
			{
				Vector2 coord = PixelCoordFromMousePos (spRenderer);
				
				if (coord.x < 0 || coord.x > 1 || coord.y < 0 || coord.y > 1)
					continue;

				Texture2D texture = (spRenderer as SpriteRenderer).sprite.texture;
				
				float rot = transform.rotation.eulerAngles.z; 
				
				float sin = Mathf.Sin( - Mathf.Deg2Rad * rot);
				float cos = Mathf.Cos( - Mathf.Deg2Rad * rot);
				float signal = transform.lossyScale.x > 0 ? 1 : -1;
				
				float x1 =  0.5f + signal * ( cos*(coord.x - 0.5f) - sin * (coord.y - 0.5f) );
				float y1 =  0.5f + ( sin*(coord.x - 0.5f) + cos * (coord.y - 0.5f) );
				
				Color pixel = texture.GetPixel((int) (x1 * texture.width), 
				                               (int) (y1 * texture.height));
				
				
				if (pixel.a < 0.1) // || (pixel.r < 0.05 && pixel.g < 0.05 && pixel.b < 0.05)
					continue;
				else
					return false;
			}

			return true;
			
		}

		void Awake()
		{
			sprites = GetComponentsInChildren<SpriteRenderer>();
		}

	}

}