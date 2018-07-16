using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario 
{
    public class FloatBehaviour : MonoBehaviour {
        private Rigidbody2D rb;
                
        // Use this for initialization
        void Start () {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            FloatAgainstGravity();
        }

        private void FloatAgainstGravity()
        {
            rb.AddRelativeForce(Vector2.up * (rb.mass * Physics2D.gravity.magnitude * rb.gravityScale));
        }       
    }
}
