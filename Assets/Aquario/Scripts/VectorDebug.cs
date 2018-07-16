using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorDebug : MonoBehaviour {
    private Text text;
    // Use this for initialization
    private static VectorDebug Instance;
    private Vector3 debugVector;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start () {
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {       
        text.text = "X: " + debugVector.x.ToString("F2") 
            + ", Y: " + debugVector.y.ToString("F2") 
            + ", Z: " + debugVector.z.ToString();       		
	}

    public static void SetVector(Vector3 vector)
    {
        Instance.debugVector = vector;
    }
}
