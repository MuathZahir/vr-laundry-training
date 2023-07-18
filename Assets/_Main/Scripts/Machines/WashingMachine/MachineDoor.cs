using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class MachineDoor : MonoBehaviour
{
    public event Action OnDoorClosed;

    [SerializeField] private HandGrabInteractable interactable;
    [SerializeField] private OneGrabRotateTransformer doorRotateTransformer;
    [SerializeField] private float doorSnapDistance = 5f;
    
    public virtual bool IsDoorAlmostClosed()
    {
        var localRotation = transform.localRotation;
        return localRotation.eulerAngles.z < doorSnapDistance || localRotation.eulerAngles.z > 360 - doorSnapDistance;
    }
    
    public void TrySnapToClosed()
    {
        if (IsDoorAlmostClosed())
        {
            CloseDoor();
            OnDoorClosed?.Invoke();
        }
    }

    public virtual void CloseDoor()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        doorRotateTransformer.UpdateAngle(0);
    }

    public void IsDoorEnabled(bool isEnabled)
    {
        interactable.enabled = isEnabled;
    }
}
