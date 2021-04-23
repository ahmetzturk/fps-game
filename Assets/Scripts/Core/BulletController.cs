using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Core
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 100f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private GameObject impactEffect = null;
        [SerializeField] private Rigidbody myRigidbody = null;
        private float maxLifeTime;      
 
        void Start()
        {
            maxLifeTime = lifeTime;
            Move();
        }

        void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                lifeTime = maxLifeTime;
                gameObject.SetActive(false);
            }

            Move();
        }

        private void Move()
        {
            myRigidbody.velocity = transform.forward * moveSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
            Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime * Time.deltaTime)), transform.rotation);
        }
    }
}
