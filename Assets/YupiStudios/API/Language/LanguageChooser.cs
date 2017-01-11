using UnityEngine;
using System.Collections;


namespace YupiStudios.API.Language {

	public class LanguageChooser : MonoBehaviour {

		[System.Serializable]
		public struct LanguageObject 
		{
			public SystemLanguage Language;
			public GameObject ObjectToActivate;
		}


		public LanguageObject[] languageObjects;
		
		public GameObject activateAsDefault; 

		void Awake () {

			SystemLanguage lang = Application.systemLanguage;

			//Teste
			//Debug.Log (lang.ToString());
			
			bool found = false;
			
			foreach (LanguageObject l in languageObjects)
			{
				
				if (lang == l.Language)
				{
					found = true;
					if (l.ObjectToActivate)
					{					
						l.ObjectToActivate.SetActive(true);					
					}
					break;
				}
				
			}
			
			if (!found && activateAsDefault)
			{
				activateAsDefault.SetActive(true);
			}
		}

	}

}
