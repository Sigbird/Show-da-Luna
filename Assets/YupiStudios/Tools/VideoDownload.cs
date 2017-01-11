using UnityEngine;
using System.IO;

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

    [Tooltip("Caminho em o arquivo será salvo")]
	public string Path;
	// Use this for initialization
	private WWW request;
	private bool downloadComplete = false;
	private string absoluteFileName;
	private string dirPath;
	private bool downloadStarted = false;
	private bool downloadError = false;

	private string iosPath;

	//private static int smallScreen = 480;
	//private static int mediumScreen = 720;

	public delegate void DownloadStartError(string error);
	public static event DownloadStartError OnDownloadStartError;

	void Awake() {
		string filename = FileNameEnglish;
		if (Application.systemLanguage == SystemLanguage.Portuguese) {
			filename = FileName;
		}
		dirPath = System.IO.Path.Combine(Application.persistentDataPath, Path);
		Directory.CreateDirectory(dirPath);
		absoluteFileName = System.IO.Path.Combine(dirPath, filename);
		//Debug.Log(absoluteFileName);

#if UNITY_IOS
		iosPath = "file://" + absoluteFileName;
		Debug.Log(iosPath);
#endif
	}

	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		if (!downloadComplete) {
			if (request != null && request.isDone){
				if (string.IsNullOrEmpty(request.error)) {
					downloadComplete = true;
					downloadStarted = false;
					downloadError = false;
					SaveFile();
				} else {
					if (OnDownloadStartError != null) {
						OnDownloadStartError(request.error);

						downloadComplete = true;
						downloadStarted = false;
						downloadError = true;

						DeleteFile();
						return;
                    }                    
				}
			}

			if (!string.IsNullOrEmpty(request.error)) {
				if (OnDownloadStartError != null) {
					OnDownloadStartError(request.error);
					
					downloadComplete = true;
					downloadStarted = false;
					downloadError = true;
					
					DeleteFile();
				}     
			}
		}
	}	

	public void DownloadFile() {	
		downloadComplete = false;
		downloadError = false;

		if ((!FileExists() && !downloadStarted) || downloadError) {	
			request = new WWW(getVideoUrlForScreen());

			downloadStarted = true;
			return;
		}

		if (!downloadError && !downloadStarted) {
			PlayVideoOnMobile();
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
		request = new WWW(getVideoUrlForScreen());
	}

	public string GetError() {
		if (request != null) {
			return request.error;
		}
		return null;
	}

	public void PlayVideoOnMobile() {
#if UNITY_IOS
		Handheld.PlayFullScreenMovie(iosPath);
		return;
#endif
		Handheld.PlayFullScreenMovie(absoluteFileName);
	}

	public string getVideoUrlForScreen() {
//		if (Screen.height < smallScreen) {
//			return UrlSmall;
//		}
//		if (Screen.height < mediumScreen) {
//			if (!string.IsNullOrEmpty(UrlMedium)) {
//				return UrlMedium;
//			}
//			return UrlSmall;
//		}
//		if (!string.IsNullOrEmpty(UrlLarge)) {
//			return UrlLarge;
//		}
		if (Application.systemLanguage == SystemLanguage.Portuguese) {
			return Url;
		} else if (Application.systemLanguage == SystemLanguage.Spanish) {
			return UrlSpanish;
		}
		return UrlEnglish;
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

}
