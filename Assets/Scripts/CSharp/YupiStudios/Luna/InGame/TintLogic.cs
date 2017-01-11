using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame {
	
	public class TintLogic : MonoBehaviour {

		public enum TintState
		{
			Idle,
			Jumping,
			Dragging
		}

		public TintLogic tintRef;

		private bool hasSettedColor = false; 

		public Color tintColor = Color.white;
		public Color colorPigment = Color.white;
		public Color colorSlot = Color.white;

		public TintMixControl stageControl;
		public PigmentController pigmentController;

		public SpriteRenderer slotSprite;

		public BoxCollider2D listenerRegion;

		public TintSplashLogic splash;

		public bool CanPaint {
			get;
			set;
		}

		private bool showOff;
		private bool changedState = true;

		private TintState _currentState;
		private TintState CurrentState
		{
			get
			{
				return _currentState;
			}
			set
			{
				TintState lastState = _currentState;
				_currentState = value;
				changedState = true;

				if (lastState == TintState.Dragging && value != TintState.Dragging)
				{
					transform.localScale /= 2.0f;
				}

				if (lastState != TintState.Dragging && value == TintState.Dragging)
				{
					transform.localScale *= 2.0f;
				}

				switch (value)
				{
				case TintState.Jumping:
					if (!showOff)
						CurrentState = TintState.Idle;
					break;
				case TintState.Idle:
					if (showOff)
						CurrentState = TintState.Jumping;
					break;
				case TintState.Dragging:
					break;
				}

			}
		}


		private Vector3 originalPos;
		private bool storedOriginalPos;

		private Vector2 PixelCoordFromMousePos()
		{

			if (listenerRegion != null)
			{			
				Vector2 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);

				Bounds b = listenerRegion.bounds;
				
				Vector2 v = new Vector2( (mouse.x - b.min.x) / ((float)b.size.x) , (mouse.y - b.min.y) / ((float)b.size.y) );
				
				return v;
			} else
			{
				return new Vector2(-1,-1);
			}

		}

		public bool IsEmptyOnMousePos()
		{
			if (listenerRegion == null)
				return true;

			Vector2 coord = PixelCoordFromMousePos ();
			
			if (coord.x < 0 || coord.x > 1 || coord.y < 0 || coord.y > 1)
				return true;
			
			/*Texture2D texture = (renderer as SpriteRenderer).sprite.texture;
			
			Color pixel = texture.GetPixel((int) (coord.x * texture.width), 
			                               (int) (coord.y * texture.height));
			
			
			if (pixel.a < 0.1) // || (pixel.r < 0.05 && pixel.g < 0.05 && pixel.b < 0.05)
				return true;
			else		*/

			return false;
			
		}

		public void SetShowOff (bool b)
		{
			showOff = b;

			if (b && CurrentState == TintState.Idle)
				CurrentState = TintState.Jumping;

			if (!b && CurrentState == TintState.Jumping)
				CurrentState = TintState.Idle;
		}


		public void SetTintRef(TintLogic tint)
		{
			if (hasSettedColor)
			{
				// nao executado na inicializacao
				SetShowOff(false);
				CurrentState = TintState.Idle;
			}

			hasSettedColor = true;
			if (pigmentController != null)
			{	
				pigmentController.SetColor(tint.colorPigment);
				slotSprite.color = tint.colorSlot;
			}

			tintColor = tint.tintColor;
			tintRef = tint;
		}

		void Awake() {
			CurrentState = TintState.Idle;
			SetShowOff(false);
			changedState = true;
		}

		void Start () {
			CanPaint = true;
			storedOriginalPos = false;
			if (!hasSettedColor)
				SetTintRef(tintRef);
		}

		bool GetShowOff ()
		{
			return showOff;
		}

		private void UpdateAnimation()
		{
			if (pigmentController != null)
			{
				switch (CurrentState)
				{
				case TintState.Jumping:
					pigmentController.Jump();
					break;
				case TintState.Dragging:
					pigmentController.Picked();
					break;
				default:
					pigmentController.Idle();
					break;
				}
			}
		}

		private void UpdatePigmentOnDrag()
		{
			if (!Input.GetMouseButton(0))
			{
				CurrentState = TintState.Idle;
				
				TintLogic tint = stageControl.TryToMix(this);
				
				if (!stageControl.IsMouseInsideCase())
				{
					if (stageControl.TryToColor(tint))
					{
						splash.SetColor(tint.tintColor);
						splash.transform.position = transform.position;
						splash.PlayAnim();
					}
				}

				transform.localPosition = originalPos;
			}
			else
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mousePos.z = transform.position.z;
				transform.position = mousePos;
			}
		}

		// Update is called once per frame
		void Update () {

			if (changedState)
			{
				UpdateAnimation();
				changedState = false;
			}

			if (!CanPaint)
				return;

			if (CurrentState == TintState.Dragging)
			{
				UpdatePigmentOnDrag();
			}
			else 
			{
				if (Input.GetMouseButtonDown(0) && !IsEmptyOnMousePos())
				{
					// Start Pigment Drag
					if (!storedOriginalPos)
					{
						storedOriginalPos = true;
						originalPos = transform.localPosition;
					}
					CurrentState = TintState.Dragging;
				}
			}
			
		}


	}

}