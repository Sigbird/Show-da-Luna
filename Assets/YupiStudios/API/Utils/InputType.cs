
using System;
using UnityEngine;

namespace YupiStudios.API.Utils
{

	public enum EInputType 
	{
		Any,
		Click,
		Escape,
		KeyCode
	}

	public enum EMouseButton
	{
		Left,
		Right,
		Middle
	}

	[System.Serializable]
	public class InputType
	{

		public EInputType inputType;
		public KeyCode keyCode;
		public EMouseButton mouseButton;


		private bool TestAny()
		{
			return Input.anyKeyDown;
		}

		private bool TestEscape()
		{
			return Input.GetKeyDown(KeyCode.Escape);
		}

		private bool TestClick()
		{
			switch (mouseButton)
			{
			case EMouseButton.Right:
				return Input.GetMouseButtonDown(1);
			case EMouseButton.Middle:
				return Input.GetMouseButtonDown(2);
			default:
				return Input.GetMouseButtonDown(0);
			}
		}

		private bool TestKey()
		{
			return Input.GetKeyDown(keyCode);
		}

		public bool TestInput()
		{
			switch(inputType)
			{
			case EInputType.Click:
				return TestClick();
			case EInputType.KeyCode:
				return TestKey();
			case EInputType.Escape:
				return TestEscape();
			default:
				return TestAny();
			}
		}
		
	}
}

