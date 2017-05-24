using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using YupiStudios.Fico;
using YupiStudios.YupiPlay.API;

using YupiStudios.Fico.Managers;

namespace YupiStudios.YupiPlay.Plugin.Icon {

	
	public class YupiPlayIcon : MonoBehaviour {
		
		public Button AvatarButton;
		public GameObject NotificationsArea;
		public Text NotificationsText;
		public Image PlayerNameBG;
		public Text PlayerNameText;
		public Image PopupMessageBG;
		public Text PopupMessageTXT;
		public Text PopupText;
		public Image PopupImg;
		public Text PopupCadastrar;

		
		private static float popupTime = 0.0f;
		
		private YupiPlayPlugin yupiPlayPlugin = null;	
		private float updateNotifications;

		private Texture avatarTexture = null;

		private YupiPlayAPI apiInstance = null;
	
		GameFacade facade;
	
		void Awake() {
			apiInstance = YupiPlayInit.getAPI();
			zeroAlpha();
		}
		
		
		// Use this for initialization
		void Start () {
			facade = GameFacade.Instance;

			if (facade.LevelManager.StartedGame)
			{
				popupTime = 0.0f;
				facade.LevelManager.StartedGame = false;
			}

			updateNotifications = 180.0f;
			yupiPlayPlugin = YupiPlayPlugin.getInstance();

			string id = yupiPlayPlugin.getChildId();
			SetChild(yupiPlayPlugin.getChildName(), yupiPlayPlugin.getChildAge().ToString(), yupiPlayPlugin.getChildGender(), id);
			if ( !string.IsNullOrEmpty( id ) )
			{
				changeAvatar(yupiPlayPlugin.getChildAvatar());
			}
		}
		
		public void OnClickYupiplayButton() {
			YupiStudios.GoogleAnalytics.GAEventHandler eHandler = YupiStudios.GoogleAnalytics.GAEventHandler.Instance;
			int access = PlayerPrefs.GetInt ("yupiplay_access",0);
			int starts = PlayerManager.Instance.StartNumber;
			eHandler.YupiPlayEvent ("Yupiplay Click", access.ToString (), starts);
			PlayerPrefs.SetInt("yupiplay_access",++access);
			Debug.Log ("openYupiPlay");
			yupiPlayPlugin.openYupiPlay();
		}

		public void OnClickAvatarButton() {
			YupiStudios.GoogleAnalytics.GAEventHandler eHandler = YupiStudios.GoogleAnalytics.GAEventHandler.Instance;
			int access = PlayerPrefs.GetInt ("avatar_access",0);
			int starts = PlayerManager.Instance.StartNumber;
			eHandler.YupiPlayEvent ("Avatar Click", access.ToString(), starts);
			PlayerPrefs.SetInt("avatar_access",++access);
			Debug.Log ("openAvatar");
			yupiPlayPlugin.openAvatar();
		}
		
		public void changeAvatar(string avatarName) {
			// "_reset"			
			if (avatarName ==  "_reset" || string.IsNullOrEmpty(avatarName)) {
				avatarName = "avatar1";
				AvatarButton.gameObject.SetActive(false);
				return;
			}

			AvatarButton.gameObject.SetActive(true);

			avatarName = string.Concat("Avatars/", avatarName);

			avatarTexture = Resources.Load<Texture>(avatarName);
			
			Texture2D tex =  (Texture2D) avatarTexture;

			Debug.Log (avatarName);

			AvatarButton.image.sprite = Sprite.Create(tex,
			                                            new Rect(0, 0, tex.width, tex.height),
			                                            new Vector2(0.5f,0.5f),AvatarButton.image.sprite.pixelsPerUnit
			                                            );
			
			//texture.texture = avatarTexture;
		}
		
		void onSetAvatar(string avatarName) {
			changeAvatar(avatarName);
		}
		
		private void SetChild(string name, string age, string gender, string id)
		{
			if ( string.IsNullOrEmpty( id ) )
			{
				changeAvatar("_reset");
				facade.PlayerManager.SetPlayerInfos("", "", "");
			}
			else
			{
				facade.PlayerManager.SetPlayerInfos(name, age, gender, id);
				PlayerNameText.text = name;
			}

		}

		void Update()
		{
			updateNotifications -= Time.deltaTime;


			if (popupTime < 11.0f)
			{
				PlayerManager player = PlayerManager.Instance;
				string pid = player.PlayerId;

				if (pid != "guest" && !string.IsNullOrEmpty(pid))
				{
					zeroAlpha();
				}


				popupTime += Time.deltaTime;

				if (popupTime > 0.5f && popupTime < 1.5f)
				{
					fadeIn();

				}
				else if (popupTime <= 10.0f && popupTime >= 1.5f)
				{
					if (PopupMessageBG.color.a != 200/255.0f)
					{
						Color c;
						c = PopupMessageBG.color;
						c.a = 200/255.0f;
						PopupMessageBG.color = c;
						
						c = PopupMessageTXT.color;
						c.a = 1;
						PopupMessageTXT.color = c;
					}
				}
				else if (popupTime > 10.0f)
				{
					fadeOut();
				}
			} else 
			{
				if (PopupMessageBG.color.a != 0)
				{
					zeroAlpha();
				}
			}


			if (updateNotifications <= 0) {
				if (apiInstance != null) {
					apiInstance.getNotificationsNumber();
				}

				updateNotifications = 180.0f;
			}

		}
		
		void onActiveChild(string childString) {
			// childString: nome, idade e genero separado por ponto e virgula			
			string[] child = childString.Split(';');
			string name   = child[0];
			string age    = child[1];
			string gender = child[2];
			string id = child[3];

			YupiPlayAPI.getInstance ().getNotificationsNumber ();
			
			SetChild(name, age, gender, id);			
			//substituir no nome acima do icone por name :)
		}

		// Resposta de apiInstance.getNotificationsNumber
		void onNotificationsResponse(string number) {
			try {
				int n = System.Convert.ToInt32(number);

				if (n > 0) {
					NotificationsArea.SetActive(true);
					NotificationsText.text = number;
				} else {
					NotificationsArea.SetActive(false);
				}
			} catch {
				NotificationsArea.SetActive(false);
			}
		}

		private void zeroAlpha() {
			Color color = PopupMessageBG.color;
			color.a = 0;
			PopupMessageBG.color = color;

			color = PopupMessageTXT.color;
			color.a = 0;
			PopupMessageTXT.color = color;

			color = PopupText.color;
			color.a = 0;
			PopupText.color = color;

			color = PopupImg.color;
			color.a = 0;
			PopupImg.color = color;

			color = PopupCadastrar.color;
			color.a = 0;
			PopupCadastrar.color = color;
		}

		private void fadeIn() {
			Color c;
			c = PopupMessageBG.color;
			c.a = Mathf.Min(1, (popupTime-0.5f)*200/255.0f);
			PopupMessageBG.color = c;
			
			c = PopupMessageTXT.color;
			c.a = Mathf.Min(1, (popupTime-0.5f));
			PopupMessageTXT.color = c;

			c = PopupText.color;
			c.a = Mathf.Min(1, (popupTime-0.5f));
			PopupText.color = c;

			c = PopupImg.color;
			c.a = Mathf.Min(1, (popupTime-0.5f));
			PopupImg.color = c;

			c = PopupCadastrar.color;
			c.a = Mathf.Min(1, (popupTime-0.5f));
			PopupCadastrar.color = c;
		}

		private void fadeOut() {
			Color c;
			c = PopupMessageBG.color;
			c.a = Mathf.Max(0,(1 - (popupTime-10.0f)) * 200/255.0f);
			PopupMessageBG.color = c;
			
			c = PopupMessageTXT.color;
			c.a = Mathf.Max(0, (1 - (popupTime-10.0f)) );
			PopupMessageTXT.color = c;

			c = PopupText.color;
			c.a = Mathf.Max(0, (1 - (popupTime-10.0f)) );
			PopupText.color = c;

			c = PopupImg.color;
			c.a = Mathf.Max(0, (1 - (popupTime-10.0f)) );
			PopupImg.color = c;

			c = PopupCadastrar.color;
			c.a = Mathf.Max(0, (1 - (popupTime-10.0f)) );
			PopupCadastrar.color = c;
		}

		private void oneAlpha() {
			Color c;
			c = PopupMessageBG.color;
			c.a = 200/255.0f;
			PopupMessageBG.color = c;
			
			c = PopupMessageTXT.color;
			c.a = 1;
			PopupMessageTXT.color = c;

			c = PopupText.color;
			c.a = 1;
			PopupText.color = c;

			c = PopupImg.color;
			c.a = 1;
			PopupImg.color = c;

			c = PopupCadastrar.color;
			c.a = 1;
			PopupCadastrar.color = c;
		}
	}
}
