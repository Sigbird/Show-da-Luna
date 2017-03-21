using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using YupiStudios.API.Language;
using Soomla.Store;

[System.Serializable]
public class OnRedeemEvent : UnityEvent<int>
{  
}


public class RedeemCode : MonoBehaviour {
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

    private int redeemedStars;
    
	// Use this for initialization
	void Start () {
        OnStart.Invoke();
        InputCode.onValueChanged.AddListener(ToUpperCase);
        InputCode.onEndEdit.AddListener(_validateInput);
        OnRedeem.AddListener(FormatRedeemMessage);
        OnFinishRedeem.AddListener(RedeemStars);
	}		

    public void RequestRedeemCode()
    {        
        _requestRedeemCode(InputCode.text);
    }

    public void _requestRedeemCode(string code)
    {
        if (!string.IsNullOrEmpty(InputCode.text)) {
            StartCoroutine(StubSuccess());
        }        
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

    public void FinishRedeem()
    {
        OnFinish.Invoke();
        OnFinishRedeem.Invoke(redeemedStars);
    }

    private IEnumerator StubSuccess()
    {
        yield return new WaitForSeconds(3);                
        OnSuccess.Invoke();
        redeemedStars = 5;
        OnRedeem.Invoke(redeemedStars);        
    }

    public void RedeemStars(int stars)
    {
        StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, stars);
        LunaStoreManager.CallBoughtStarsEvent();        
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

    public void ToUpperCase(string code)
    {
        InputCode.text = code.ToUpperInvariant();
    }
}
