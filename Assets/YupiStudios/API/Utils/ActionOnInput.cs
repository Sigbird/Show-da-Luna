using UnityEngine;
using System.Collections;

namespace YupiStudios.API.Utils {

	public class ActionOnInput : MonoBehaviour {

		public InputType keyToActivate;
		public ActionObject []actions;
				
		// Update is called once per frame
		void Update () {
			#region EXIT_WINDOW
			if(Input.GetKeyDown(KeyCode.Escape))
				this.gameObject.SetActive(false);

			#endregion

			if (keyToActivate.TestInput())
			{
				foreach (ActionObject action in actions)
				{
					action.DoAction();
				}
			}
		}
	}

}