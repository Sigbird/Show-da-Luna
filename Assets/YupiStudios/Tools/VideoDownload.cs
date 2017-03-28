using UnityEngine;
using System.IO;
using YupiPlay;
using YupiPlay.Luna;
using System.Collections;

public class VideoDownload : MonoBehaviour {

    [Tooltip("Nome do arquivo")]
	public string FileName;
	[Tooltip("Nome do arquivo em ingles")]
	public string FileNameEnglish;

	[Tooltip("URL do arquivo")]
	public string Url;
	[Tooltip("URL do arquivo em ingles")]
	public string UrlEnglish;
	[Tooltip("URL do arquivo em espanhol")]
	public string UrlSpanish;

    public string FilePT;
    public string FileEN;
    public string FileES;
       
	private WWW request;
	private bool downloadComplete = false;
	private string absoluteFileName;
	private string dirPath;
	private bool downloadStarted = false;
	private bool downloadError = false;
	public const string VIDEODIR = "videos";

	private string iosPath;    

	public delegate void DownloadStartError(string error);
	public static event DownloadStartError OnDownloadStartError;

    private int timesTried = 0;
    private int myPriority = 1;
	private int timesTriedCurrentPriority = 0;
	private float progress = 0;

	private string offlineFile;
	private const float timeLimit = 30f;
	private float timeOut = 0f;
	private float oldProgress = 0f;

	void Awake() {
		if (!BuildConfiguration.VideoDownloadsEnabled) {			
			offlineFile = System.IO.Path.Combine(Application.streamingAssetsPath, VIDEODIR);		
			offlineFile = System.IO.Path.Combine(offlineFile, FileEN);
		}

		string filename = FileNameEnglish;
		if (Application.systemLanguage == SystemLanguage.Portuguese) {
			filename = FileName;
		}
		dirPath = System.IO.Path.Combine(Application.persistentDataPath, VIDEODIR);
		Directory.CreateDirectory(dirPath);
		absoluteFileName = System.IO.Path.Combine(dirPath, filename);
		Debug.Log(absoluteFileName);

#if UNITY_IOS
		iosPath = "file://" + absoluteFileName;
		Debug.Log(iosPath);
#endif
	}

	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		if (!downloadComplete && downloadStarted) {		

			if (timeOut >= timeLimit) {
				downloadError = true;
				downloadStarted = false;
				downloadComplete = true;

				DeleteFile();

				if (OnDownloadStartError != null) {
					OnDownloadStartError(request.error);	
				}
				return;
			}	

			if (request.progress > 0 && request.progress > oldProgress) {
				timeOut = 0f;
			}

			timeOut += Time.unscaledDeltaTime;
			if (request.progress > 0) oldProgress = request.progress;
		}
	}	

	public void DownloadFile() {
		if (!BuildConfiguration.VideoDownloadsEnabled) {			
			StartCoroutine(PlayOfflineVideo());
			return;
		}

		downloadComplete = false;
		downloadError = false;

		if ((!FileExists() && !downloadStarted) || downloadError) {	
			StartCoroutine(Downloader());
			return;
		}

		if (!downloadError && !downloadStarted) {
			PlayVideoOnMobile();
		}
	}

	private IEnumerator Downloader() {
		request = new WWW(getVideoUrl());
		downloadStarted = true;
		timeOut = 0f;

		yield return request;

		if (!string.IsNullOrEmpty(request.error)) {
			timesTried++;
			timesTriedCurrentPriority++;

			int currentPriorityCount = DownloadRedundant.Instance.GetCurrentPriorityCount();
			int mirrorsCount = DownloadRedundant.Instance.GetCount();

			downloadStarted = false;
			if (timesTriedCurrentPriority < currentPriorityCount) {				
				DownloadFile();
				yield break;
			} else if (timesTried < mirrorsCount) {
				myPriority++;
				timesTriedCurrentPriority = 0;
				DownloadFile();
				yield break;
			}

			//se ja tentou todos
			if (timesTried >= DownloadRedundant.Instance.GetCount()) {
				myPriority = 1;
				timesTried = 0;
				timesTriedCurrentPriority = 0;

				downloadComplete = true;
				downloadStarted = false;
				downloadError = true;

				DeleteFile();

				if (OnDownloadStartError != null) {
					OnDownloadStartError(request.error);	
				}
					
				yield break;
			} 
		}

		if (request.isDone && string.IsNullOrEmpty(request.error)) {
			downloadComplete = true;
			downloadStarted = false;
			downloadError = false;
			SaveFile();
		}
	}

	public float GetProgress() {
		if (request != null) {
			return request.progress;		
		}
		return 0;
	}

	public bool IsDone() {
		if (request != null) {
			return request.isDone;
		}
		return false;
	}

	public byte[] GetBytes() {
		if (request != null && request.isDone) {
			return request.bytes;
		}
		return null;
	}

	public bool SaveFile() {
		FileStream stream = File.OpenWrite(absoluteFileName);

		stream.Write(request.bytes, 0, request.bytesDownloaded);
		return true;
	}

	public bool FileExists() {
		#if UNITY_IOS
		return File.Exists(iosPath);
        #endif
		if (!BuildConfiguration.VideoDownloadsEnabled) {
			#if UNITY_ANDROID
			return true;
			#endif
			return File.Exists(offlineFile);
		}
		return File.Exists(absoluteFileName);
	}

	public byte[] GetFileBytes() {
		if (File.Exists(absoluteFileName)) {	
			FileStream stream = File.OpenRead(absoluteFileName);
			byte[] buffer = new byte[stream.Length];
			stream.Read(buffer, 0, (int)stream.Length);

			return buffer;
		}
		return null;
	}

	public WWW GetWWWRequest() {
		return request;
	}

	public void ReDownloadFile() {
		request = new WWW(getVideoUrl());
	}

	public string GetError() {
		if (request != null) {
			return request.error;
		}
		return null;
	}

	public bool hasError() {
		return downloadError;
	}

	public void PlayVideoOnMobile() {
#if UNITY_IOS
		Handheld.PlayFullScreenMovie(iosPath);
		return;
#endif
		Handheld.PlayFullScreenMovie(absoluteFileName);
	}

	public string getVideoUrl() {          
        string hostUrl = DownloadRedundant.Instance.GetServerRoundRobin(myPriority);

		if (Application.systemLanguage == SystemLanguage.Portuguese) {
			return hostUrl + FilePT;
		} else if (Application.systemLanguage == SystemLanguage.Spanish) {
			return hostUrl + FilePT;
		}
		return hostUrl + FileEN;
	}

	public bool IsDownloadStarted() {
		return downloadStarted;
	}

	public void DeleteFile() {
		#if UNITY_IOS
		File.Delete(iosPath);
		return;
		#endif
		
		File.Delete(absoluteFileName);
	}		

	public IEnumerator PlayOfflineVideo() {
		DeleteExtractedVideos();
		Directory.CreateDirectory(dirPath);

		WWW video = new WWW(offlineFile);
		yield return video;

		FileStream stream = File.OpenWrite(absoluteFileName);
		stream.Write(video.bytes, 0, video.bytes.Length);
		stream.Close();
		PlayVideoOnMobile();
	}

	public void DeleteExtractedVideos() {
		if (Directory.Exists(dirPath)) {
			Directory.Delete(dirPath, true);
		}
	}

	/*
	private void OldDownloaderUpdate() {
		if (!downloadComplete) {
			if (request != null && request.isDone){
				if (string.IsNullOrEmpty(request.error)) {
					downloadComplete = true;
					downloadStarted = false;
					downloadError = false;
					SaveFile();
				} else {
					timesTried++;

					int currentPriorityCount = DownloadRedundant.Instance.GetCurrentPriorityCount();

					if (timesTried < currentPriorityCount) {
						downloadStarted = false;
						DownloadFile();
						return;
					}
					myPriority++;

					//se ja tentou todos
					if (timesTried >= DownloadRedundant.Instance.GetCount()) {
						myPriority = 1;

						downloadComplete = true;
						downloadStarted = false;
						downloadError = true;

						DeleteFile();

						if (OnDownloadStartError != null) {
							OnDownloadStartError(request.error);	
						}

						return;    
					}                        					                                       
				}
			}

			if (request != null && !string.IsNullOrEmpty(request.error)) {
				if (OnDownloadStartError != null) {
					OnDownloadStartError(request.error);

					downloadComplete = true;
					downloadStarted = false;
					downloadError = true;

					DeleteFile();
				}     
			}
		}
	}*/
}
