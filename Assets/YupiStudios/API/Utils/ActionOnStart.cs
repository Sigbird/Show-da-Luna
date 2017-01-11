using UnityEngine;
using System.Collections;


namespace YupiStudios.API.Utils {
	
	public class ActionOnStart : MonoBehaviour {
		
		public ActionObject []actions;

		void Start () {
			foreach (ActionObject action in actions)
			{
				action.DoAction();
			}
		}

	}
	
}