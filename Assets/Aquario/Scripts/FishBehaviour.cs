using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class FishBehaviour : MonoBehaviour
    {
        public float SwimForce;
        private Rigidbody2D rb;
        public Vector2 direction = Vector2.right;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            StartCoroutine(WaitToChangeDirection());
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Swim(Vector2 direction)
        {
            rb.AddRelativeForce(direction * SwimForce);
        }      

        private void InvertXScale()
        {
            Vector3 scale = new Vector3(1f, 1f, 1f);

            if (transform.localScale.x == 1f)
            {
                scale = new Vector3(-1f, 1f, 1f);
            }

            transform.localScale = scale;
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            ChangeDirection();
        }

        IEnumerator WaitToChangeDirection()
        {
            while (true)
            {
                if (GameController.IsPlaying)
                {
                    int random = Random.Range(5, 11);
                    yield return new WaitForSeconds(random);

                    random = Random.Range(0, 2);
                    if (random > 0)
                    {
                        ChangeDirection();
                    }
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private void ChangeDirection()
        {
            if (direction == Vector2.right)
            {
                //Swim(Vector2.left);
                direction = Vector2.left;
                InvertXScale();               
            }
            else
            {
                //Swim(Vector2.right);
                direction = Vector2.right;
                InvertXScale();                
            }
        }
    }
}