using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class ObjectPool : MonoBehaviour
    {
        public GameObject Prefab;
        public int InitialSize = 5;
        public int MaxSize = 50;

        private List<GameObject> objects;

        // Use this for initialization
        void Start()
        {
            objects = new List<GameObject>();

            if (Prefab != null)
            {
                for (int i = 0; i < InitialSize; i++)
                {
                    createObject();
                }
            }            
        }

        private GameObject createObject()
        {
            GameObject newObject = Instantiate(Prefab, transform.position, Quaternion.identity, transform);
            newObject.SetActive(false);
            objects.Add(newObject);
            return newObject;
        }

        public GameObject GetObject()
        {
            foreach (GameObject pooledObject in objects)
            {
                if (pooledObject.activeSelf == false)
                {
                    pooledObject.SetActive(true);
                    return pooledObject;
                }
            }

            if (objects.Count < MaxSize)
            {
                GameObject newObject = createObject();
                newObject.SetActive(true);
                return newObject;
            }

            return null;
        }

        public void Shrink()
        {
            if (objects.Count <= InitialSize)
            {
                return;
            }

            foreach (GameObject pooledObject in objects)
            {
                if (pooledObject.activeSelf == false)
                {
                    Destroy(pooledObject);
                    objects.Remove(pooledObject);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

