using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imagine.WebAR.Samples
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        void Start()
        {
            //mainCamera = Camera.main;
        }
        void LateUpdate()
        {
            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}
