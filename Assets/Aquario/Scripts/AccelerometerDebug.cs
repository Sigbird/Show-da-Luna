using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay.Luna.Aquario
{
    public class AccelerometerDebug : MonoBehaviour
    {
        private Text text;
        // Use this for initialization
        void Start()
        {
            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = "acc: " + Input.acceleration.ToString() + "\n";
            text.text += "G: " + Input.gyro.gravity.ToString() + "\n";
            text.text += "R: " + Input.gyro.rotationRateUnbiased.ToString();
        }
    }
}
