using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class Walls : MonoBehaviour
    {
        public enum Borders
        {
            Top, Bottom, Left, Right
        }

        public Borders BorderPosition;

        void Awake()
        {


        }
        // Use this for initialization
        void Start()
        {
            switch (BorderPosition)
            {
                case Borders.Top:
                    transform.position = new Vector3(0, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y);
                    break;
                case Borders.Bottom:
                    transform.position = new Vector3(0, Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y );
                    break;
                case Borders.Left:
                    transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, 0);
                    break;
                case Borders.Right:
                    transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x, 0);
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

