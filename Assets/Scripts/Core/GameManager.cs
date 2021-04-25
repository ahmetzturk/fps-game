using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance = null;
        [SerializeField] private GameObject player; 

        public static GameManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
                return instance;
            }
        }

        public GameObject Player { get => player; }

        private void OnEnable()
        {
            instance = this;
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {

        }
    }
}
