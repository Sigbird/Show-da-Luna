using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using YupiPlay.Luna;

public class VideoManager : MonoBehaviour {	


	public enum VIDEO_STATES
	{
		DOWNLOAD, PLAYABLE, BLOCKED
	}

    [Tooltip("Sprite de bloqueado")]
    public Sprite blocked;
    [Tooltip("Botão do vídeo")]
    public Button videoBtn;
    [Tooltip("Identifica se o vídeo é da versão Free ou não")]
    public bool isFree = false;
	[Tooltip("Colqoque o numero do video")]
	public int number;
	[Tooltip("Colqoque o numero ad coleçao")]
	public int collection;
	[Tooltip("Janela de compra do video")]
	public GameObject purchaseWindow;

    private Sprite currentIcon;             //Icone a ser setado
    private VideoDownload videoDownload;      //Script FileDoownload do próprio objeto
	private bool purchased;
	private Sprite playable;

    private VIDEO_STATES currentState = VIDEO_STATES.DOWNLOAD;

    void Awake() {
		if (BuildConfiguration.CurrentPurchaseType == BuildType.Free) isFree = true;
        videoDownload = videoBtn.GetComponent<VideoDownload>();
    }

	void Update(){
		//CheckVideoState ();
	}

	void Start(){

		//playable = videoBtn.GetComponent<Button> ().image.sprite;
		CheckVideoState();
	}

	void onBuyVideoListener(string VideoID) {
		string videoid;

		if (collection == 1) {
			videoid = "stars_video_0" + number + "_col_0" + collection + "_id";
		} else {
			videoid = "video_0" + number + "_col_0" + collection + "_id";
		}

		if (videoid == VideoID) {
			changeVideoState();
		}
	}

	void onBuyCollectionListener(string CollectionID) {
		string collectionid;

		if (collection == 1) {
			collectionid = LunaStoreAssets.STARS_COLLECTION01_LTVG_ITEM_ID;
		} else {
			collectionid = "collection_0" + collection + "_id";
		}

		if (CollectionID == collectionid) {
			changeVideoState();
		}

	}

	void changeVideoState() {
		if (videoDownload.FileExists()) {
			currentState = VIDEO_STATES.PLAYABLE;
			videoBtn.GetComponent<Animator>().SetTrigger("DownloadComplete");
			videoBtn.GetComponent<Animator>().ResetTrigger("Blocked");
			videoBtn.GetComponent<Animator>().ResetTrigger("AnimateButton");
			videoBtn.GetComponent<Animator>().ResetTrigger("DownloadIdle");
		} else {
			currentState = VIDEO_STATES.DOWNLOAD;
			videoBtn.GetComponent<Animator>().SetTrigger("DownloadIdle");
			videoBtn.GetComponent<Animator>().ResetTrigger("Blocked");
			videoBtn.GetComponent<Animator>().ResetTrigger("AnimateButton");
			videoBtn.GetComponent<Animator>().ResetTrigger("DownloadComplete");
		}
	}

    void CheckVideoState()
    {
		if (isFree) {
			changeVideoState();
			return;
		}

		if (LunaStoreManager.Instance != null && (LunaStoreManager.Instance.AcquiredVideo(number,collection) || 
		                                          LunaStoreManager.Instance.AcquiredCollection(collection)) ) {
			isFree = true;
			CheckVideoState();
		}else{
			videoBtn.GetComponent<Animator>().SetTrigger("Blocked");
			videoBtn.GetComponent<Animator>().ResetTrigger("AnimateButton");
			videoBtn.GetComponent<Animator>().ResetTrigger("DownloadIdle");
			videoBtn.GetComponent<Animator>().ResetTrigger("DownloadComplete");
		}		
    }

	public void TryDownload(){
		if(isFree){
			videoDownload.DownloadFile();
		}else if(LunaStoreManager.Instance != null && LunaStoreManager.Instance.AcquiredCollection(collection)){
			isFree = true;
			currentState = VIDEO_STATES.DOWNLOAD;
			videoDownload.DownloadFile();
		}else if (LunaStoreManager.Instance != null &&
		          LunaStoreManager.Instance.AcquiredVideo (number,collection)) {
			isFree = true;
			currentState = VIDEO_STATES.DOWNLOAD;
			videoDownload.DownloadFile();
		} else {
			purchaseWindow.SetActive(true);
		}
	}

	public void ChangeVideoState(VIDEO_STATES videoState){
		isFree = true;
		currentState = videoState;
	}

	public void ChangeStateToPayed(){
		currentState = VIDEO_STATES.DOWNLOAD;
		isFree = true;
		CheckVideoState();
	}

	void OnEnable() {
		CheckVideoState();
//		videoBtn.GetComponent<Animator>().ResetTrigger("Blocked");
//		videoBtn.GetComponent<Animator>().ResetTrigger("AnimateButton");
//		videoBtn.GetComponent<Animator>().ResetTrigger("DownloadIdle");
//		videoBtn.GetComponent<Animator>().ResetTrigger("DownloadComplete");

		LunaStoreManager.OnVideoPurchased += onBuyVideoListener;
		LunaStoreManager.OnCollectionPurchased += onBuyCollectionListener;
	}

	void OnDisable() {
		LunaStoreManager.OnVideoPurchased -= onBuyVideoListener;
		LunaStoreManager.OnCollectionPurchased -= onBuyCollectionListener;
	}

}
