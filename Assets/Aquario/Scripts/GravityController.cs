using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class GravityController : MonoBehaviour
    {
        public bool ChangeGravityVector = true;
        const float gravityAcc = 9.81f;

        void Awake()
        {                
            Input.gyro.enabled = true;
            if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                Screen.orientation = ScreenOrientation.LandscapeRight;
                return;
            }
            if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                return;
            }
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GyroGravity();
        }

        private void GyroGravity()
        {	
			//Gyroscope gravity may not work on some devices
			//Vector2 newGravity = new Vector2(gravityAcc * Input.gyro.gravity.x, gravityAcc * Input.gyro.gravity.y);
            #if !UNITY_EDITOR
            if (ChangeGravityVector) {
                var acc = Input.acceleration.normalized;
                Vector2 newGravity = new Vector2(gravityAcc * acc.x, 
                    gravityAcc * acc.y);		    
		        Physics2D.gravity = newGravity;
            }			
            #endif
        }
    }
}
