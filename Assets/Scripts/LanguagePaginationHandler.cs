﻿using YupiPlay.Luna;
using UnityEngine;
using YupiStudios.API.Language;

public class LanguagePaginationHandler : MonoBehaviour {
	public LanguageChooser.LanguageObject[] LanguageObjects;
	// Use this for initialization
	void Awake () {
		SystemLanguage lang = Application.systemLanguage;

        if (BuildConfiguration.ManualLanguage != SystemLanguage.Unknown) {
            lang = BuildConfiguration.ManualLanguage;
        }

        foreach (LanguageChooser.LanguageObject l in LanguageObjects) {
			if (l.Language == lang) {
				l.ObjectToActivate.transform.parent = null;
				Destroy(l.ObjectToActivate);
			}
		}
	}

	void Start() {
		
	}

}
