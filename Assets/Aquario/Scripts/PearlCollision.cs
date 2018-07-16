using System;
using UnityEngine;

namespace YupiPlay.Luna.Aquario 
{
    public class PearlCollision : MonoBehaviour {
        public string[] CollectorTags;

        void OnCollisionEnter2D(Collision2D coll) {            
            if (IsCollector(coll.gameObject.tag)) {
                gameObject.SetActive(false);
            }
        }

        bool IsCollector(string tag) {
            if (Array.Exists(CollectorTags, collectorTag => collectorTag == tag)) {
                return true;
            }

            return false;
        }
    }
}
