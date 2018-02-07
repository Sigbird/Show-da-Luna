using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using YupiStudios.API.Language;
using UnityEngine.Networking;
using MiniJSON;

[System.Serializable]
public class OnRedeemEvent : UnityEvent<int>
{  
}


public class RedeemCode : MonoBehaviour {
    private const string REDEEMURL_DEV = "http://localhost:1337/";
	private const string REDEEMURL_PROD = "http://lunapromocode.azurewebsites.net/";

    public InputField InputCode;
    public GameObject RedeemMessage;
    public UnityEvent OnStart;
    public UnityEvent OnValidateInput;
    public UnityEvent OnSuccess;
    public OnRedeemEvent OnRedeem;
    public UnityEvent OnFailure;
    public UnityEvent OnError;
    public UnityEvent OnFinish;
    public OnRedeemEvent OnFinishRedeem;
	public UnityEvent OnEnd;

    private int redeemedStars = 0;
    	
	void Start () {
        OnStart.Invoke();
        InputCode.onValueChanged.AddListener(ToUpperCase);
        InputCode.onEndEdit.AddListener(_validateInput);
        OnRedeem.AddListener(FormatRedeemMessage);
        OnFinishRedeem.AddListener(RedeemStars);
	}

	void OnEnable() {
		OnStart.Invoke();
	}

    public void ValidateInput()
    {
        _validateInput(InputCode.text);
    }

    public void _validateInput(string code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            OnValidateInput.Invoke();
        }
    }

    public void ToUpperCase(string code)
    {
        InputCode.text = code.ToUpperInvariant();
    }

    public void RequestRedeemCode()
    {        
        _requestRedeemCode(InputCode.text);
    }

    public void _requestRedeemCode(string code)
    {      
        StartCoroutine(SendRequest(code));      
    }

    private IEnumerator SendRequest(string code)
    {
        string url = REDEEMURL_PROD;
    #if UNITY_EDITOR
        url = REDEEMURL_DEV;
    #endif

        url = url + InputCode.text;

        UnityWebRequest req = UnityWebRequest.Get(url);
        yield return req.Send();

        if (req.isNetworkError)
        {
            OnError.Invoke();
        } else
        {
            string res = req.downloadHandler.text;
            Dictionary<string,object> data = Json.Deserialize(res) as Dictionary<string,object>;

            if (data != null)
            {                                            
                if (data.ContainsKey("redeem"))
                {
                    if ((bool) data["redeem"] == false)
                    {
                        OnFailure.Invoke();
                    } else
                    {
                        if (data.ContainsKey("stars"))
                        {
                            long stars = (long) data["stars"];
							redeemedStars = (int) stars;
                            OnSuccess.Invoke();
                            OnRedeem.Invoke(redeemedStars);
                        }
                    }
                } else
                {
                    OnFailure.Invoke();
                }
            } else
            {
                OnError.Invoke();
            } 
        }

        yield break;
    }

    private IEnumerator StubSuccess()
    {
        yield return new WaitForSeconds(3);
        OnSuccess.Invoke();
        redeemedStars = 5;
        OnRedeem.Invoke(redeemedStars);
    }

    public void FormatRedeemMessage(int stars)
    {
        LanguageChooser languageChooser = RedeemMessage.GetComponent<LanguageChooser>();
        Text currentText = languageChooser.GetCurrent().GetComponent<Text>();
        Text text = currentText.GetComponent<Text>();

        string formatted = string.Format(text.text, stars);

        Text textFormattedRedeem = languageChooser.Formatted.GetComponent<Text>();
        textFormattedRedeem.text = formatted;

        currentText.gameObject.SetActive(false);
        languageChooser.Formatted.SetActive(true);
    }

    public void FinishRedeem()
    {
        OnFinish.Invoke();
        OnFinishRedeem.Invoke(redeemedStars);
    }
   
    public void RedeemStars(int stars)
    {
        //StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, stars);
        //LunaStoreManager.CallBoughtStarsEvent();        
    }

	public void CleanInput() {
		InputCode.text = "";
	}

	public void EndDialog() {
		OnEnd.Invoke();
	}

    
}
