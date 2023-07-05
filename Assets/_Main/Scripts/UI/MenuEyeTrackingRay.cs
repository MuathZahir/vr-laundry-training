using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEyeTrackingRay : MonoBehaviour
{
    [SerializeField] private float rayLength = 50f;
    [SerializeField] private LayerMask layerMask;
    
    private EyeInteractables _currentInteractable;

    private void Update()
    {
        var direction = transform.forward;
        
        if(Physics.Raycast(transform.position, direction, out var hit, rayLength, layerMask))
        {
            if (hit.collider.TryGetComponent<EyeInteractables>(out var interactable))
            {
                if (_currentInteractable != interactable)
                {
                    if(_currentInteractable != null)
                        _currentInteractable.Deselect();
                    
                    _currentInteractable = interactable;
                    _currentInteractable.Select();
                }
            }
            else if (_currentInteractable != null)
            {
                _currentInteractable.Deselect();
                _currentInteractable = null;
            }
        }
        else if (_currentInteractable != null)
        {
            _currentInteractable.Deselect();
            _currentInteractable = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLength);
    }
}
