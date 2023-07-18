using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_DEBUG : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private void Update()
    {
        // Basic WASD movement in the direction of the camera
        
        var cameraForward = Camera.main.transform.forward;
        var cameraRight = Camera.main.transform.right;
        
        cameraForward.y = 0;
        cameraRight.y = 0;
        
        var direction = (cameraForward * Input.GetAxis("Vertical") + cameraRight * Input.GetAxis("Horizontal")).normalized;
        
        transform.position += direction * (speed * Time.deltaTime);
        
    }
}
