using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;


public class VideoDownloadHandler : DownloadHandlerScript {
    private string fileName;

    public VideoDownloadHandler(string fileName) : base() {
        this.fileName = fileName;
    }

    protected void OnContentComplete() {
        byte[] videoData = this.data;

        if (isDone) {
            try {
                FileStream stream = File.OpenWrite(fileName);

                stream.Write(videoData, 0, data.Length);
                stream.Close();
                // return true;
            } catch (Exception e) {
                Debug.LogError(e.Message);
                // return false;
            }
        }
        
    }
    
}
