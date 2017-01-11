using UnityEngine;
using System.Collections;

public class MenuLogic : MonoBehaviour {
	private const float TIME_TO_SHOW_STAGES = 0.7f;
	private const float TIME_TO_SHOW_GIFTS = 1.5f;

	public static int stageShow = 0;

	public GameObject gift;
	public GameObject[] stageIconsActive;
	public GameObject[] stageIconsInactive;
	public GameObject[] showParticles;

	private float timePast;
	private bool needShowGift;

	public static int GetCurrentStage()
	{
		return PlayerPrefs.GetInt ("CurrentStage",0);
	}

	public static void SetCurrentStage(int stage)
	{
		PlayerPrefs.SetInt ("CurrentStage",stage);
		stageShow = stage;
	}

	public static bool GetNewItem(int i)
	{
		return PlayerPrefs.GetInt ("NewItem_"+i,0) != 0;
	}

	public static void SetNewItem(int i)
	{
		ChangeNewItem (i,1);
	}

	public static void ClearNewItems()
	{
		for (int i = 0; i < 5; ++i) {
			PlayerPrefs.SetInt ("NewItem_"+i,0);
		}
		PlayerPrefs.Save ();
	}

	private static void ChangeNewItem(int i, int value)
	{
		PlayerPrefs.SetInt ("NewItem_"+i,value);
		PlayerPrefs.Save ();
	}

	private void ShowGift()
	{
		for (int i = 0; i < 5; ++i) {			
			if (GetNewItem(i))
			{
				gift.SetActive(true);
			}
		}
	}

	private void StageShow() {
		if (stageShow > 0 && stageShow <= showParticles.Length)
		{
			showParticles[stageShow-1].SetActive(true);
			EnableStageIcon(stageShow-1);
		}
		stageShow = 0;
	}

	private void EnableStageIcon(int i)
	{
		stageIconsActive[i].SetActive(true);
		stageIconsInactive[i].SetActive(false);
	}

	private void EnableStages()
	{
		for (int i = 0; i < stageIconsActive.Length; ++i) {
			if (i+1 <= GetCurrentStage() && i+1 != stageShow)
			{
				EnableStageIcon(i);
			}
		}
	}

	// Use this for initialization
	void Start () {
		EnableStages ();
		timePast = 0;
		needShowGift = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (stageShow > 0) {
			if (timePast < TIME_TO_SHOW_STAGES) {
				timePast += Time.deltaTime;
			} else {
				StageShow ();
			}
		} else {
			if (timePast < TIME_TO_SHOW_GIFTS)
			{
				timePast += Time.deltaTime;
			} else {
				if (needShowGift)
				{
					ShowGift ();
					needShowGift = false;
				}
			}
		}
	
	}
}
