using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class PearlPool : MonoBehaviour
    {
        public static PearlPool Instance { get; private set; }
        public ObjectPool Pool {get; private set; } 
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Pool = GetComponent<ObjectPool>();
        }        
    }

}
