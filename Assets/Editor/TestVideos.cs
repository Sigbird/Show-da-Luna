using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;

public class TestVideos {

	[Test]
	public void TestVideosSimplePasses() {
        string path = Path.Combine(Application.persistentDataPath, VideoDownload.VIDEODIR);
        var files = Directory.GetFiles(path);
        Debug.Log(files.Length * 10);
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestVideosWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
