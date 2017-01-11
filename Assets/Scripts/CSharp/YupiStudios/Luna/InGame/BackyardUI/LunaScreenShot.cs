using UnityEngine;
using UnityEngine.UI;

using YupiStudios.API.Utils;
using YupiStudios.Luna.InGame;

[RequireComponent ( typeof(FacebookManager) ) ]
[RequireComponent( typeof (YupiStudios.API.Utils.ScreenShot) )]
public class LunaScreenShot : MonoBehaviour {

	private const string FB_MSG_TXT = "Brincar de colorir ficou ainda mais divertido com a Luna e sua turma! =D\nBaixe você também o novo jogo educativo de \"O Show da Luna!\": https://goo.gl/rzmn0F";

    public BackyardControl backyardControl;
    public GameObject screenShotCanvas;
	public GameObject screenShotStartBG;
	public GameObject screenShotFBPublished;
	public InputField FacebookInput;
	public Text placeHolderPtBr;
	public GameObject TextHolder;
	public Text ShareButtonText;
	public GameObject ShareButton;
	public GameObject Sending;
	public GameObject SendingChooser;

    public Image screenShotImageDest;
    public Text screenShotFile;

    private ScreenShot screenShotComponent;
	private FacebookManager faceBookManagerComponent;
    private bool WaitingSS { get; set; }
    private bool needSS;

	private bool share = false;

    // Use this for initialization
    void Start () {
        screenShotComponent = GetComponent<ScreenShot> ();
		faceBookManagerComponent = GetComponent<FacebookManager>();
		//Placeholder
		SystemLanguage lang = Application.systemLanguage;
		if (FacebookInput != null)
			FacebookInput.GetComponent<InputField> ().placeholder.GetComponent<Text> ().text = placeHolderPtBr.text;
    }

    public void TakeScreenShot()
    {
        //if (backyardControl.PictureChanged)
            needSS = true;
    }

	public void SendToFacebook()
	{
		SendFacebookRequest();
		if (screenShotComponent.Texture) {
			TextHolder.SetActive(false);
			string lang = Application.systemLanguage.ToString();
			if(lang.Equals("Portuguese"))
				ShareButtonText.text = "Publicar";
			if(lang.Equals("English"))
				ShareButtonText.text = "Post";
			FacebookInput.gameObject.SetActive (true);
			share = true;
		}			
	}

	public void SendFacebookRequest() {
		if (share) {
			ShareButton.SetActive(false);
			//Sending.SetActive (true);
			SendingChooser.SetActive(true);
			string userInput = FacebookInput.text;
			faceBookManagerComponent.PhotoToFacebook (userInput, screenShotComponent.Texture.EncodeToPNG(),ShareCallback);
		}
	}

	public void ShareCallback(FBResult result) {
		if (gameObject.activeInHierarchy) {
			if (result.Error != null) {
				Debug.Log ("facebook photo error");
			} else {
				StarsSystemEvents.Event05();
				screenShotStartBG.SetActive(false);
				screenShotFBPublished.SetActive(true);
				YupiStudios.Analytics.YupiAnalyticsEventHandler.InGameEvent("SendToFaceBook",backyardControl.currentItemNum.ToString());
				ExitScreenShot();
			}
		}
	}

    private void ScreenShotAction()
    {
		screenShotStartBG.SetActive (true);
		screenShotFBPublished.SetActive (false);
        backyardControl.SetUIActive(false);
        screenShotFile.text = "";
        screenShotComponent.TakeSreenShot(backyardControl.GetSSName(), "Luna", true);
        WaitingSS = true;
		YupiStudios.Analytics.YupiAnalyticsEventHandler.InGameEvent("TakeScreenShot",backyardControl.currentItemNum.ToString());
    }

    void Update()
    {
        if (needSS)
        {
            ScreenShotAction();
            needSS = false;
        }
        

        if (WaitingSS)
        {
            if (!screenShotComponent.IsBusy)
            {
                WaitingSS = false;
                screenShotFile.text = screenShotComponent.LastName;
                if (screenShotComponent.Texture != null)
                	screenShotImageDest.overrideSprite = Sprite.Create(screenShotComponent.Texture, new Rect( 0,0, screenShotComponent.Texture.width, screenShotComponent.Texture.height), new Vector2(0.5f,0.5f));
                screenShotCanvas.SetActive(true);
                backyardControl.SetUIActive(true);                
            }
        }
    }

	public void ExitScreenShot() {
		FacebookInput.gameObject.SetActive(false);
		FacebookInput.text = "";
		ShareButtonText.text = "Facebook";
		TextHolder.SetActive(true);
		ShareButton.SetActive(true);
		//Sending.SetActive (false);
		SendingChooser.SetActive (false);
        share = false;
	}
}
