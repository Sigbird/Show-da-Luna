
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


using UnityEngine.UI;


namespace YupiStudios.Luna.InGame {

	public class BackyardControl : TintMixControl {

		private const float WAIT_TIME_TO_ACTIVATE_EXIT = 1.5f;

		public Transform backyardStageItemPrefab;

		public int currentItemNum;

		public AudioSource exitAudio;

        public GameObject UI;

		public GameObject caseLayer;

		private ItemChooser[] items;

		private GameObject currentItem;

        public Button exitButton;

        private bool end = false;

		private float waitToEnableExitButton;

		//public bool PictureChanged { get; private set; }

		public override bool TryToColor(TintLogic tint)
		{
			bool res = base.TryToColor (tint);

			if (res)
			{
				ColorableObject obj = GetObjectInMouse();
				BackyardItemAnimController controller = obj.GetComponentInParent<BackyardItemAnimController>();
				if (controller)
				{
					controller.PlayPaintAnim();
				}
				//PictureChanged = true;
			}
			
			return res;
		}

        private void UpdateColorableObjects(GameObject backyardItem, int itemNum)
		{
			int i = 10;
			List<string> loadedItems = new List<string>();

			items = backyardItem.GetComponentsInChildren<ItemChooser>();
			foreach (ItemChooser randomItem in items)
			{
				string name = randomItem.LoadItem(itemNum, loadedItems, itemNum+i++);

				if (name != null)
					loadedItems.Add( name );

				//randomItem.SetColor( GetColorFromDisk(randomItem.name) );
			}

			colorableObjects = GetComponentsInChildren<ColorableObject>();
			System.Array.Sort<ColorableObject> (colorableObjects);

			SetColorableObjects(colorableObjects);
		}

		public GameObject LoadNewItem(int itemNum)
		{
			GameObject obj = (Instantiate( backyardStageItemPrefab ) as Transform).gameObject;
			obj.transform.parent = transform;
			return obj;
		}

		private IEnumerator createNewItem()
		{
			// espera proximo frame
			yield return new WaitForEndOfFrame();

			currentItem = LoadNewItem(currentItemNum);		
			UpdateColorableObjects(currentItem, currentItemNum);

		}

		private void CreateItem()
		{
			Destroy(currentItem);
			StartCoroutine(createNewItem());
           // PictureChanged = false;
		}

		public string GetSSName()
		{
			return "LUNA_CENA"+ currentItemNum;
		}

		public void SetUIActive(bool active)
		{
			UI.SetActive(active);
			caseLayer.SetActive(active);
		}

		public void ChangeItem(int itemNum)
		{
			//SaveColorsToDisk();
			//if (currentItemNum != itemNum)
			{
				YupiStudios.Analytics.YupiAnalyticsEventHandler.InGameEvent ("SceneMenu","ChangeItem",itemNum);
				currentItemNum = itemNum;
				CreateItem();
			}
		}

		public void NextItem()
		{
			if (currentItemNum < 11) {
				ChangeItem (currentItemNum + 1);
			}
		}

		public void PreviousItem()
		{
			if (currentItemNum > 0)
			{
				ChangeItem(currentItemNum-1);
			}
		}

		private void ExitCallBack()
		{
			Application.LoadLevel("Menu");
		}

		public void ExitToMenu () {
			if (end)
				return;

			exitAudio.Play ();

			end = true;
            
			Invoke ("ExitCallBack",EXIT_DELAY);
		}

		public void DeActivateExit()
		{
			exitButton.interactable = false;
			waitToEnableExitButton = WAIT_TIME_TO_ACTIVATE_EXIT;

			GetComponent<LunaScreenShot>().ExitScreenShot();
		}

		public void ActivateExit()
		{
			exitButton.interactable = true;
		}

		public override void Start () {
            base.Start();
			CreateItem();
		}

		void Update() {
			if (!exitButton.interactable) {
				waitToEnableExitButton -= Time.deltaTime;
				if (waitToEnableExitButton <= 0)
				{
					ActivateExit();
				}
			}
	   }


	}

}