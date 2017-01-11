using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace YupiStudios.API.Utils {

	public class ScreenShot : MonoBehaviour {

        public Camera screenShotCamera;

        public int screenShotWidth;
        public int screenShotHeight;

        public bool IsBusy { get; private set; }

		public Texture2D Texture { get; private set; }

        public string LastName { get; private set; }
        
		private int GetSDKLevel() {
#if UNITY_ANDROID
			AndroidJavaClass build = new AndroidJavaClass("android.os.Build$VERSION");
			return build.GetStatic<int>("SDK_INT");
#endif
			//var clazz = AndroidJNI.FindClass("android.os.Build.VERSION");
			//var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
			//var sdkLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
			//return sdkLevel;
			return 0;
		}

		private string GetDir(string dir) {
			
			#if (UNITY_ANDROID && !UNITY_EDITOR)

			string path = "/sdcard/DCIM";

			if(GetSDKLevel() >= 23){
				path = "/sdcard0/DCIM";
			}

			if (Directory.Exists("/sdcard/DCIM"))
				path = "/sdcard/DCIM";
			else if (Directory.Exists("/sdcard0/DCIM"))
				path = "/sdcard0/DCIM";
			else if (Directory.Exists("/storage/sdcard0/DCIM"))
				path = "/storage/sdcard0/DCIM";
			else if (Directory.Exists("/storage/emulated/0/DCIM"))
				path = "/storage/emulated/0/DCIM";
			else if (Directory.Exists("/storage/emulated/legacy/DCIM"))
				path = "/storage/emulated/legacy/DCIM";
			else if (Directory.Exists("/mnt/sdcard/DCIM"))
				path = "/mnt/sdcard/DCIM";
			
			
			
			return path + "/"+dir+"/";		
			
			
			
			#else
			return Application.persistentDataPath + "/"+dir+"/Saves/";
			#endif
			
			
		}

		private string GetFileName(string name)
		{
			return string.Format ("{0}_{1}.png",
			                     name, 
			                     System.DateTime.Now.ToString ("yyyy-MM-dd_HH-mm-ss"));
		}
		
		private string GetPathName(string name, string dir) {
			return GetDir(dir) + GetFileName(name);
		}

		public void TakeSreenShot(string drawName, string dir, bool waitEndOfFrame)
		{
			IsBusy = true;
            LastName = "";
			StartCoroutine (SaveScreenShot(drawName, dir, waitEndOfFrame));
		}

		IEnumerator GetScreenShot(string drawName, string directory, bool waitEndOfFrame)
		{
			if (Texture != null)
				Destroy(Texture);

			if (waitEndOfFrame)
				yield return new WaitForEndOfFrame();

            //Rect pRect = Camera.main.pixelRect;
			//Texture2D screenShot = new Texture2D((int)pRect.width, (int)pRect.height, TextureFormat.RGB24, true);
			//screenShot.ReadPixels(new Rect((int)pRect.xMin, (int)pRect.yMin, (int)pRect.width, (int)pRect.height), 0, 0);
			Texture = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);
			Texture.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0,false);
			Texture.Apply();

			SaveToDisk(Texture, drawName, directory, waitEndOfFrame);

			IsBusy = false;
		}

		void SaveToDisk(Texture2D texture, string drawName, string directory, bool waitEndOfFrame)
		{
			string dir = GetDir(directory);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			string name = GetPathName(drawName, directory);

            LastName = name;

            byte[] bytes = texture.EncodeToPNG();
			FileStream file = File.Open(name, FileMode.Create);
			if (file != null)
			{
				BinaryWriter binary = new BinaryWriter(file);
				if (binary != null) 
				{

					binary.Write(bytes);					
					file.Close();
				}
			}
		}
		
		IEnumerator SaveScreenShot(string drawName, string directory, bool waitEndOfFrame)
		{
			if (waitEndOfFrame)
				yield return new WaitForEndOfFrame();

			int multX = Mathf.CeilToInt ((float)screenShotWidth / Camera.main.pixelWidth);
			int multY = Mathf.CeilToInt ((float)screenShotHeight / Camera.main.pixelHeight);

			int mult = Mathf.Max (multX,multY);

			RenderTexture rt = new RenderTexture((int)Camera.main.pixelWidth*mult, (int)Camera.main.pixelHeight*mult, 24);
            RenderTexture.active = rt;
			screenShotCamera.targetTexture = rt;
			screenShotCamera.Render();

			Texture2D screenShot = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
			screenShot.ReadPixels(new Rect(0,0, rt.width, rt.height),0,0,false);
			screenShot.Apply();

            screenShotCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

			if (Texture != null)
				Destroy(Texture);
			
			Texture = screenShot;

			SaveToDisk(Texture, drawName, directory, waitEndOfFrame);

            IsBusy = false;
        }
	}


}