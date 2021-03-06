﻿using UnityEngine;
using YupiPlay.Luna;
using UnityEngine.UI;

namespace YupiStudios.API.Language {

	public class LanguageChooserInputField : MonoBehaviour {

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

            if (BuildConfiguration.ManualLanguage != SystemLanguage.Unknown) {
                lang = BuildConfiguration.ManualLanguage;
            }

            if (YupiPlay.Luna.BuildConfiguration.VideoDownloadsEnabled == false)
            {
                lang = SystemLanguage.English;
            }

            InputField input = GetComponent<InputField>();
			//Teste
			//Debug.Log (lang.ToString());
			
			//bool found = false;
			
			foreach (LanguageObject l in languageObjects)
			{
				
				if (lang == l.Language)
				{
					if (l.ObjectToActivate != null)
					{		
						l.ObjectToActivate.SetActive(true);
						input.placeholder =  l.ObjectToActivate.GetComponent<Text>();
					}
					return;
				}
				
			}
			
			if (activateAsDefault != null)
			{
				activateAsDefault.SetActive(true);
				input.placeholder =  activateAsDefault.GetComponent<Text>();			
			}
		}

	}

}
