using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;

        void Start()
        {

        }

        void LateUpdate()
        {
            // move the camera
            transform.position = targetTransform.position;

            // rotate the camera
            transform.rotation = targetTransform.rotation;
        }
    }
}
