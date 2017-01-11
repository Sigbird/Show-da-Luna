using UnityEngine;
using UnityEngine.UI;

using YupiStudios.Analytics;

using System;
using System.Globalization;
using System.Text.RegularExpressions;

public class YupiTimeSendEmail : MonoBehaviour {

	public InputField emailInput;
	public EmailForm emailForm;
	public Text placeHolderPtBr;

	void Start(){
		SystemLanguage lang = Application.systemLanguage;
		if(lang == SystemLanguage.Portuguese){
			if(emailInput != null){
				emailInput.GetComponent<InputField>().placeholder.GetComponent<Text>().text = placeHolderPtBr.text;
			}
		}
	}

    public void OnEnable()
    {
        YupiAnalyticsEventHandler.AdvertisingEvent("YupiTimeSliders", "YupiTime");
    }

    bool invalid = false;
	
	public bool IsValidEmail(string strIn)
	{
		invalid = false;
		if (String.IsNullOrEmpty(strIn))
			return false;
		
		// Use IdnMapping class to convert Unicode domain names.
		try {
			strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
			                      RegexOptions.None);
		}
		catch (Exception) {
			return false;
		}
		
		if (invalid)
			return false;
		
		// Return true if strIn is in valid e-mail format.
		try {
			return Regex.IsMatch(strIn,
			                     @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			                     @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
			                     RegexOptions.IgnoreCase);
		}
		catch (Exception) {
			return false;
		}
	}
	
	private string DomainMapper(Match match)
	{
		// IdnMapping class with default property values.
		IdnMapping idn = new IdnMapping();
		
		string domainName = match.Groups[2].Value;
		try {
			domainName = idn.GetAscii(domainName);
		}
		catch (ArgumentException) {
			invalid = true;
		}
		return match.Groups[1].Value + domainName;
	}

	// Use this for initialization
	public void SendEmail () {
		string email = emailInput.text;
		if (!IsValidEmail (email)) {
			emailInput.text = "E-mail invalido";
            YupiAnalyticsEventHandler.AdvertisingEvent("SendInvalidEmail", "YupiTime");
        } else {            
            emailForm.SendEmailToList(email);
			emailInput.text = "Obrigado!";
            YupiAnalyticsEventHandler.AdvertisingEvent("SendValidEmail", "YupiTime");
        }
	}
}
