using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using MiniJSON;
using System;

public class EmailForm : MonoBehaviour {
	public InputField EmailField;

	private const string mailgunUrl     = "api.mailgun.net/v3";
	private const string domain         = "yupiplay.com";
	private const string listAddress    = "lunayupitime@yupiplay.com";
	private const string apiKey 	    = "key-f10f6a1d9b8cd2fe9cf34e6d925cb60d";
	private const string pubKey         = "pubkey-5b1389f2f92592b0cae4e70c9346e798";	

	private const string validationFormat = "https://{0}/address/validate?address={1}";
	private const string listFormat = "https://{0}/lists/{1}/members";

	private string endpoint;
	private string validationEndpoint;

	public void OnSubmit() {
		string email = EmailField.text;

		SendEmailToList(email);

	}

	public void SendEmailToList(string email) {
		StartCoroutine(SendEmailToListCo(email));
	}


	public IEnumerator SendEmailToListCo(string email) {
		string validateEmailUrl = string.Format(validationFormat, mailgunUrl, WWW.EscapeURL(email));
		Debug.Log (validateEmailUrl);

		WWW validationReq = new WWW(validateEmailUrl, null, getAuthHeaders("api", pubKey));
		yield return validationReq;

		Debug.Log(validationReq.text);

		Dictionary<string, object> json = Json.Deserialize(validationReq.text) as Dictionary<string, object>;

		if (json != null) {
            object valid;
            bool isValid = false;

            if (json.TryGetValue("is_valid", out valid)) {
                isValid = (bool) valid;
            }

            if (isValid) {
                Debug.Log ("IT IS VALID");

                string url = string.Format(listFormat, mailgunUrl, WWW.EscapeURL(listAddress));
                Debug.Log (url);

                WWWForm form = new WWWForm();
                form.AddField("address", email);
                form.AddField("subscribed", "yes");
                form.AddField ("vars", getVars());

                Dictionary<string,string> headers = form.headers;
                headers["Authorization"] = getAuthString("api",apiKey);

                WWW req = new WWW(url, form.data, headers);
                yield return req;

                Debug.Log (req.text);
    		}            
		}
	}

	private string getAuthString(string username, string password) {
		return "Basic " + System.Convert
			.ToBase64String(System.Text.Encoding.ASCII.GetBytes(username + ":" + password));
	}

	private Dictionary<string,string> getAuthHeaders(string username, string password) {

		Dictionary<string, string> headers = new Dictionary<string,string>();
		headers.Add("Authorization", getAuthString(username, password));

		return headers;
	}

	private string getVars() {
		Hashtable vars = new Hashtable();
		vars.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd"));
		return Json.Serialize(vars);
	}
}
