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
        public GameObject Formatted;

        private GameObject currentlyActive;

		void Awake () {
			SystemLanguage lang = Application.systemLanguage;

			if (YupiPlay.Luna.BuildConfiguration.VideoDownloadsEnabled == false) {
				lang = SystemLanguage.English;
			}				
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
                        currentlyActive = l.ObjectToActivate;		
					}
					break;
				}
				
			}
			
			if (!found && activateAsDefault)
			{
				activateAsDefault.SetActive(true);
                currentlyActive = activateAsDefault;
			}
		}

        public GameObject GetCurrent()
        {
            return currentlyActive;
        }

	}

}
