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

        public GameObject GetPoolObject(Transform firePoint, Transform cameraPoint)
        {
            GameObject pooledObject = poolQueue.Dequeue();
            pooledObject.transform.position = firePoint.position;

            if (Physics.Raycast(cameraPoint.position, cameraPoint.forward, out RaycastHit hit, 50f))
            {
                if (Vector3.Distance(firePoint.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(cameraPoint.position + cameraPoint.forward * 30f);
            }
            pooledObject.transform.forward = firePoint.forward;

            pooledObject.SetActive(true);
            poolQueue.Enqueue(pooledObject);
            return pooledObject;
        }
    }
}
