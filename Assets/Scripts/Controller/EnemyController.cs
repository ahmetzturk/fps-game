using FPS.Attack;
using FPS.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPS.Controller 
{ 
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent = null;
        [SerializeField] private float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;
        [SerializeField] private float waitTime = 5f;
        [SerializeField] private float waitTimeForShoot = 2f;
        private float waitTimeCounter;
        private float waitTimeForShootCounter;
        private Transform target;
        private bool chasing;
        private Vector3 initialPosition;

        void Start()
        {
            initialPosition = transform.position;
            waitTimeCounter = waitTime;
            waitTimeForShootCounter = 0;
            target = GameManager.Instance.Player.transform;
        }

        void Update()
        {
            Vector3 targetModifiedPosition = new Vector3(target.transform.position.x, 
                transform.position.y, target.transform.position.z);

            Vector3 vectorToTarget = targetModifiedPosition - transform.position;

            if (chasing == false)
            {
                if (vectorToTarget.magnitude < distanceToChase)
                {
                    GetComponent<Shoot>().ShootCounter = 1f;
                    chasing = true;
                }

                else
                {
                    if (navMeshAgent.remainingDistance < 0.5f)
                    {
                        waitTimeCounter -= Time.deltaTime;
                    }
                    if (waitTimeCounter <= 0)
                    {
                        navMeshAgent.SetDestination(initialPosition);
                        waitTimeCounter = waitTime;
                    }
                }
            }

            else if (chasing == true)
            {
                //waitTimeForShootCounter -= Time.deltaTime;

                //if (waitTimeForShootCounter <= 0)
                //{
                    GetComponent<Shoot>().Fire();
                    //waitTimeForShootCounter = waitTimeForShoot;
                //}

                if (navMeshAgent.remainingDistance < distanceToStop && vectorToTarget.magnitude < distanceToStop)
                {
                    navMeshAgent.SetDestination(transform.position);
                    transform.LookAt(targetModifiedPosition);
                }
                else
                {
                    navMeshAgent.SetDestination(targetModifiedPosition);
                }

                if (vectorToTarget.magnitude > distanceToLose)
                {
                    chasing = false;
                }
            }
        }
    }
}
