using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;
using System;

public class DownloadManager : MonoBehaviour {

    private static DownloadManager Instance;

    private List<UnityWebRequest> requests;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public static UnityWebRequest AddDownload(string url, VideoDownload caller) {
        UnityWebRequest req = UnityWebRequest.Get(url);
        Instance.StartCoroutine(Instance.StartDownload(req, caller));

        return req;
    }

    private IEnumerator StartDownload(UnityWebRequest request, VideoDownload caller) {
        yield return request.Send();

        if (request.isError) {
            caller.OnDownloadError();
        }

        if (request.isDone && !request.isError) {
            caller.OnDownloadFinished();            
        }
    }
    
}
