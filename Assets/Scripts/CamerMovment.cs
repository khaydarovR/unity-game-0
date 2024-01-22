using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CamerMovment : MonoBehaviour
{
    public float cameraSpeed = 3f;

    private void Start()
    {
  
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Перемещение камеры
            float horizontal = Input.GetAxis("Mouse X") * cameraSpeed;
            float vertical = Input.GetAxis("Mouse Y") * cameraSpeed;

            transform.Translate(-horizontal, -vertical, 0);         
        }
        
    }
}
