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
        [SerializeField] private Transform cameraPoint = null;
        private float shootCounter = 0f;

        public float ShootCounter { get => shootCounter; set => shootCounter = value; }

        public void Fire()
        {
            if (shootCounter <= 0)
            {
                bulletPool.GetPoolObject(firePoint, cameraPoint);
                shootCounter = shootFrequency;
            }
            shootCounter -= Time.deltaTime;
        }
    }
}
