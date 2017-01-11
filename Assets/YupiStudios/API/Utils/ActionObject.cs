using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Reflection;


namespace YupiStudios.API.Utils {

	public enum EActionType
	{
		None,
		Quit,
		SetTimeScale,
		CallEvent,
		LoadScene
	}

	[System.Serializable]
	public class ActionObject {

		public EActionType actionType;
		public float timeScaleValue;
		public string scene;

		public UnityEvent gameEvent;

		private void CallEvent()
		{
			if (gameEvent != null)
				gameEvent.Invoke ();
		}

		public void DoAction()
		{
			switch (actionType)
			{
			case EActionType.Quit:
				Application.Quit();
				break;
			case EActionType.SetTimeScale:
				Time.timeScale = timeScaleValue;
				break;
			case EActionType.LoadScene:
				Application.LoadLevel(scene);
				break;
			case EActionType.CallEvent:
				CallEvent();
				break;
			default:
				break;
			}
		}

	}


}