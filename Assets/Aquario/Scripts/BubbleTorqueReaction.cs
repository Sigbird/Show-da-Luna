using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class BubbleTorqueReaction : MonoBehaviour
    {     
        private Rigidbody2D rb;        

        public float JetMultiplier = 2f;
        public float StreamMultiplier = 1f;
        public float TorqueAmount = 0.1f;
        public bool IsReceivingTorque = false;

        public float torqueTimeout = 2f;
        private float torqueTimeoutCounter = 0f;
        private int rotationDirection = 1;
               
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();            
        }
       
        void Update()
        {
            torqueTimeoutCounter += Time.deltaTime;
            if (torqueTimeoutCounter >= torqueTimeout)
            {
                IsReceivingTorque = false;                                
            }
        }

        void OnParticleCollision(GameObject gameObject)
        {            
            if (!IsReceivingTorque)
            {
                rotationDirection = -rotationDirection;              
            }                        
            
            if (gameObject.tag == "BubbleJet")
            {
                AddTorque(rotationDirection * JetMultiplier);
            }
            if (gameObject.tag == "BubbleStream")
            {
                AddTorque(rotationDirection * StreamMultiplier);
            }

            IsReceivingTorque = true;
            torqueTimeoutCounter = 0f;
        }

        void AddTorque(float multiplier = 1f)
        {
            if (rb != null)
            {                         
                rb.AddTorque(TorqueAmount * multiplier);
            }            
        }        
    }
}
