using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RedeemCode : MonoBehaviour {
    public Text Code;
    public UnityEvent OnSuccess;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RequestRedeemCode()
    {
        StartCoroutine(StubSuccess());
    }

    private IEnumerator StubSuccess()
    {
        yield return new WaitForSeconds(3);
        OnSuccess.Invoke();
    }
}
