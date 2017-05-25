using UnityEngine;
using System.Collections;
using YupiStudios.Luna.Config;
using YupiPlay.Luna;

namespace YupiStudios.Luna.Menu {

	public class LunaLogic : MonoBehaviour {

		public PathLogic path;

		public PathEntryLogic toFind;

		private PathEntryLogic currentPath;
		private PathEntryLogic lastToFind;
		private PathEntryLogic pastPath;
		private string sceneToLoad;

		public float speed;

		private Animator animatorLuna;

		private int walkHash, idleHash;

		private static Vector3 lastPosition = Vector3.zero;
		public static string NextStageSceneToLoad = "";

		public GameObject purchaseCanvas;

		public bool IsFree = false;

		void Start()
		{
			if (YupiPlay.Luna.BuildConfiguration.CurrentPurchaseType == YupiPlay.Luna.BuildType.Free) {
				IsFree = true;
			}

			animatorLuna = GetComponent<Animator> ();
			walkHash = Animator.StringToHash("lunaWalk");
			idleHash = Animator.StringToHash("lunaIdle");

			if (lastPosition != Vector3.zero)
				((RectTransform)transform).position = lastPosition;
			else 
			{
				transform.position = path.GetFirst().position;
				lastPosition = transform.position;
			}

			if (!string.IsNullOrEmpty (NextStageSceneToLoad)) {
				PathEntryLogic entry = path.GetPathEntryByLoadscene(NextStageSceneToLoad);
				NextStageSceneToLoad = "";
				if (entry != null)
					LunaEnterScene(entry);
			}

			speed = speed * (Screen.width / 800.0f);//(Screen.height/480.0f) * (Screen.width/800.0f)
		}

		public void LunaEnterScene(PathEntryLogic pathToFind)
		{
			if (pathToFind != null) {
				if (IsFree) {
					toFind = pathToFind;
					sceneToLoad = pathToFind.SceneToLoad;
					return;
				}

				if (GameConfiguration.gameVersion == GameConfiguration.EGameVersion.DemoVersion)
				{
					switch (pathToFind.SceneToLoad)
					{
					case "Backyard":
						break;
					case "Market":
						break;
					case "Room":
						break;
					#region WHITOUT IN-APP
					//ESSES CASES SOH SERAO USADOS NA VERSAO DA AMAZON (SEM IN-APP)
					/*case "Pet":
						break;
					case "Beach":
						break;
					case "Zoo":
						break;*/
					#endregion
					default:
						purchaseCanvas.SetActive(true);		//Linha que controla chama a janela pra comprar
						return;
					}
				}

				toFind = pathToFind;
				sceneToLoad = pathToFind.SceneToLoad;
			}		
		}

		void Update()
		{
			if (lastToFind != toFind)
			{
				if (currentPath != null && lastToFind != null)
				{
					if (currentPath.PathNumber < lastToFind.PathNumber)
					{
						if (currentPath.PathNumber > toFind.PathNumber)
							currentPath = null;
					}
					else if (currentPath.PathNumber > lastToFind.PathNumber)
					{
						if (currentPath.PathNumber < toFind.PathNumber)
							currentPath = null;
					}
				}

				lastToFind = toFind;
			}

			if (toFind != null)
			{
				RectTransform currentPos = (RectTransform)transform;
				RectTransform destTransform;

				PathEntryLogic newPath;

				if (currentPath == null)
				{
					currentPath = path.GetClosestPath(currentPos, toFind);
				}

				newPath = path.GetClosestPath(currentPos, currentPath, toFind);

				if (currentPath != newPath)
				{
					transform.position = currentPath.transform.position;
					currentPath = newPath;
				}

				if (currentPath != null)
				{
					destTransform = currentPath.GetRectTransform();
					Vector3 diff = destTransform.position - currentPos.position;
					transform.position += diff.normalized * Time.deltaTime * speed * (1+Mathf.Min (1.4f, Mathf.Abs(currentPath.PathNumber - toFind.PathNumber)));

					Vector3 size = transform.localScale;

					if (diff.x > 0 && size.x < 0)
					{
						size.x *= -1;
						transform.localScale = size;
					} else if (diff.x < 0 && size.x > 0)
					{
						size.x *= -1;
						transform.localScale = size;
					}

					animatorLuna.Play(walkHash);

				}
				else
				{
					toFind = null;
					lastToFind = null;
					animatorLuna.Play(idleHash);
					if (!string.IsNullOrEmpty(sceneToLoad))
					{
						lastPosition = transform.position;
						Application.LoadLevel(sceneToLoad);
					}
				}

			}

		}


	}

}