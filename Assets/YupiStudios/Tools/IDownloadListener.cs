using System;


public interface IDownloadListener
{
	void OnProgress();
	void OnDownloadComplete();
	void OnDownloadError(string error = null);
	void OnRequestStarted();
}


