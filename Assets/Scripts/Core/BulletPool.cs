using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Core
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject poolObject = null;
        [SerializeField] private float poolSize = 20;
        private Queue<GameObject> poolQueue = new Queue<GameObject>();

        void Start()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject pooledObject = Instantiate(poolObject, transform);
                poolObject.SetActive(false);
                poolQueue.Enqueue(pooledObject);
            }
        }

        public GameObject GetPoolObject(Transform objectTransform)
        {
            GameObject pooledObject = poolQueue.Dequeue();
            pooledObject.transform.position = objectTransform.position;
            pooledObject.transform.forward = objectTransform.forward;
            pooledObject.SetActive(true);
            poolQueue.Enqueue(pooledObject);
            return pooledObject;
        }

    }
}
