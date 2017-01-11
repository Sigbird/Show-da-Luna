using UnityEngine;
using System.Collections;

public class OneSignalPushInit : MonoBehaviour {

	private const string ONESIGNALID = "c2c539c5-76a0-49ad-86c9-db7583a8c096";
	private const string GOOGLEAPPID = "688900388454";

	// Use this for initialization
	void Start () {
		OneSignal.StartInit(ONESIGNALID, GOOGLEAPPID)
			.HandleNotificationOpened(HandleNotificationOpened)
				.EndInit();
	}
	


	private static void HandleNotificationOpened(OSNotificationOpenedResult result) {
	}
}
