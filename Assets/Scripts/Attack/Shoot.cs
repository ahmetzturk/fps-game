using FPS.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Attack
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private float shootFrequency = 0.5f;
        [SerializeField] private BulletPool bulletPool = null;
        [SerializeField] private Transform firePoint = null;
        private float lastShoot = 0.5f; 

        void Update()
        {
            Fire();
        }

        private void Fire()
        {
            if (Input.GetButton("Fire1") && lastShoot >= shootFrequency)
            {
                bulletPool.GetPoolObject(firePoint);              
                lastShoot = 0;
            }
            lastShoot += Time.deltaTime;
        }
    }
}
