using UnityEngine;
using System.IO;
using YupiPlay;
using YupiPlay.Luna;
using System.Collections;
using System;
using YupiPlay.Luna.LunaPlayer;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VideoDownload : MonoBehaviour {

    [Tooltip("Nome do arquivo")]
	public string FileName;
	[Tooltip("Nome do arquivo em ingles")]
	public string FileNameEnglish;

    // deprecated
	//[Tooltip("URL do arquivo")]
	//public string Url;
	//[Tooltip("URL do arquivo em ingles")]
	//public string UrlEnglish;
	//[Tooltip("URL do arquivo em espanhol")]
	//public string UrlSpanish;

    public string FilePT;
    public string FileEN;
    public string FileES;
       	
	private bool downloadComplete = false;
	private string absoluteFileName;
	public string[] absolutelocalFileNames;
	public string[] localFileNames;
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

	private string offlineFile;
	private const float timeLimit = 30f;
	private float timeOut = 0f;
    private float oldProgress;
	public int videosIndex;
	private bool endVideoTest;

    private UnityWebRequest webRequest;

    void Awake() {
		if (!BuildConfiguration.VideoDownloadsEnabled) {			
			offlineFile = System.IO.Path.Combine(Application.streamingAssetsPath, VIDEODIR);		
			offlineFile = System.IO.Path.Combine(offlineFile, FileEN);
		}

		string filename = FileNameEnglish;
		if (Application.systemLanguage != SystemLanguage.English) {
			filename = FileName;
		}

		dirPath = System.IO.Path.Combine(Application.persistentDataPath, VIDEODIR);
		Directory.CreateDirectory(dirPath);


		absoluteFileName = System.IO.Path.Combine(dirPath, filename);


		localFileNames = Directory.GetFiles (dirPath);
		absolutelocalFileNames = new string[localFileNames.Length];
		if (localFileNames != null) {
			int x = 0;
			foreach (string localfilename in localFileNames) {
				absolutelocalFileNames [x] = System.IO.Path.Combine (dirPath, Path.GetFileName(localfilename));
				x++;
			}
		}


#if UNITY_IOS
        absoluteFileName = "file://" + absoluteFileName;		
		Debug.Log(absoluteFileName);
#endif
    }

    void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
//		debug.text = absoluteFileName;
//		debug2.text = absolutelocalFileNames[0];
		if (!downloadComplete && downloadStarted) {		

			if (timeOut >= timeLimit) {
				downloadError = true;
				downloadStarted = false;
				downloadComplete = true;

				DeleteFile();
                Debug.Log("file deleted");

				if (OnDownloadStartError != null) {
					OnDownloadStartError(webRequest.error);	
				}
				return;
			}	

			if (webRequest.downloadProgress > 0 
                && webRequest.downloadProgress > oldProgress) {

                timeOut = 0f;
			}

			timeOut += Time.unscaledDeltaTime;
            if (webRequest.downloadProgress > 0 && webRequest.downloadProgress < 1) {                
                oldProgress = webRequest.downloadProgress;                
            }
		}

		if (localFileNames != null) {
			if (videosIndex > localFileNames.Length) {
				videosIndex = 0;
			}
		}

		if (endVideoTest) {
			endVideoTest = false;


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
			StartDownload();
			return;
		}

		if (!downloadError && !downloadStarted) {
			PlayVideoOnMobile();
		}
	}

	private void StartDownload() {
        //webRequest = UnityWebRequest.Get(getVideoUrl());
        var url = getVideoUrl();
        Debug.Log(url);
        webRequest = DownloadManager.AddDownload(url, this);
        //webRequest.downloadHandler = new VideoDownloadHandler(absoluteFileName);
        
		downloadStarted = true;
		timeOut = 0f;        				
	}

	public float GetProgress() {
		if (webRequest != null) {
			return webRequest.downloadProgress;		
		}
		return 0;
	}

	public bool IsDone() {
		if (webRequest != null) {
			return webRequest.isDone;
		}
		return false;
	}

	public byte[] GetBytes() {
		if (webRequest != null && webRequest.isDone) {
			return webRequest.downloadHandler.data;
		}
		return null;
	}

	public bool SaveFile() {
		try {
			FileStream stream = File.OpenWrite(absoluteFileName);

			stream.Write(webRequest.downloadHandler.data, 0, webRequest.downloadHandler.data.Length);
			stream.Close();
			return true;	
		} catch (Exception e) {
            Debug.LogError(e.Message);
			return false;
		}

	}

	public bool FileExists() {		
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

	public UnityWebRequest GetWebRequest() {
		return webRequest;
	}

	public void ReDownloadFile() {
		webRequest = UnityWebRequest.Get(getVideoUrl());
        webRequest.Send();
    }

	public string GetError() {
		if (webRequest != null) {
			return webRequest.error;
		}
		return null;
	}

	public bool hasError() {
		return downloadError;
	}

	public void PlayVideoOnMobile() {
#if UNITY_ANDROID || UNITY_IOS
		StartCoroutine(PlayVideoCoroutine(absoluteFileName));
        //Handheld.PlayFullScreenMovie(absoluteFileName);
		//VideoPlayerController.Instance.Play(absoluteFileName,localFileNames);

#endif
#if UNITY_EDITOR || UNITY_STANDALONE
		StartCoroutine(PlayVideoCoroutine(absoluteFileName));
		//Handheld.PlayFullScreenMovie(absoluteFileName);
		//StartCoroutine(PlayVideoCoroutine(absoluteFileName));

#endif
        //VideoPlayerController.Instance.Play();
    }

	public string getVideoUrl() {          
        string hostUrl = DownloadRedundant.Instance.GetServerRoundRobin(myPriority);

        SystemLanguage language = Application.systemLanguage;
        if (BuildConfiguration.ManualLanguage != SystemLanguage.Unknown) {
            language = BuildConfiguration.ManualLanguage;
        }

        try {
            if (language == SystemLanguage.Portuguese) {
                return hostUrl + FilePT;
            }
            if (language == SystemLanguage.Spanish) {
                return hostUrl + FileES;
            }
                    
            return hostUrl + FileEN;
        } catch (NullReferenceException e) {
            return "";
        }		
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

	private IEnumerator PlayVideoCoroutine(string videoPath)
	{
		Debug.Log ("tocou");
		yield return new WaitForSeconds(1);
		Handheld.PlayFullScreenMovie(videoPath, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);    
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		Debug.Log ("Saiu");



		if (absolutelocalFileNames != null) {
			videosIndex++;
			if (videosIndex >= absolutelocalFileNames.Length) {
				videosIndex = 0;
			} 
		}
		StartCoroutine(PlayVideoCoroutine(absolutelocalFileNames[videosIndex])); 

//		StartCoroutine(PlayVideoCoroutine(absoluteFileName)); 

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

    public void OnDownloadError() {
        timesTried++;
        timesTriedCurrentPriority++;

        int currentPriorityCount = DownloadRedundant.Instance.GetCurrentPriorityCount();
        int mirrorsCount = DownloadRedundant.Instance.GetCount();

        downloadStarted = false;
        if (timesTriedCurrentPriority < currentPriorityCount) {
            DownloadFile();
            return;
        } else if (timesTried < mirrorsCount) {
            myPriority++;
            timesTriedCurrentPriority = 0;
            DownloadFile();
            return;
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
                OnDownloadStartError(webRequest.error);
            }            
        }
    }

    public void OnDownloadFinished() {
        downloadComplete = true;
        downloadStarted = false;
        downloadError = false;
		UpdateFilesList ();
        SaveFile();
    }

    public string GetFileName() {
        return absoluteFileName;
    }

	public void UpdateFilesList(){
		localFileNames = Directory.GetFiles (dirPath);
		absolutelocalFileNames = new string[localFileNames.Length];
		if (localFileNames != null) {
			int x = 0;
			foreach (string localfilename in localFileNames) {
				absolutelocalFileNames [x] = System.IO.Path.Combine (dirPath, Path.GetFileName(localfilename));
				x++;
			}
		}
	}
}
