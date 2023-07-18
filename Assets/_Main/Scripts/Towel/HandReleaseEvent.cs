using System;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;
using UnityEngine.Events;

public class HandReleaseEvent : MonoBehaviour
{
    [SerializeField] private OneGrabFreeTransformer oneGrabFreeTransformer;
    [SerializeField] private TwoGrabFreeTransformer twoGrabFreeTransformer;
    [SerializeField] private UnityEvent onBothHandsReleased;
    [SerializeField] private PointableElement pointable;


    private bool handOneGrabbing = false;
    private bool handTwoGrabbing = false;

    private void Start()
    {
        oneGrabFreeTransformer.OnBeginTransform += HandGrabbed;
        oneGrabFreeTransformer.OnEndTransform += HandReleased;
        twoGrabFreeTransformer.OnBeginTransform += HandGrabbed;
        twoGrabFreeTransformer.OnEndTransform += HandReleased;
    }

    // Call this function when one hand is released
    public void HandReleased(int handIndex, IGrabbable grabbable)
    {
        if (handIndex == 1)
        {
            if(pointable.PointsCount == 1)
                handOneGrabbing = false;
        }
        else if (handIndex == 2)
        {
            handTwoGrabbing = false;
        }

        CheckBothHandsReleased();
    }

    // Call this function when one hand is grabbed again
    public void HandGrabbed(int handIndex)
    {
        if (handIndex == 1)
        {
            handOneGrabbing = true;
        }
        else if (handIndex == 2)
        {
            handTwoGrabbing = true;
        }
    }

    // Check if both hands are released and trigger the event
    private void CheckBothHandsReleased()
    {
        if (!handOneGrabbing && !handTwoGrabbing)
        {
            onBothHandsReleased.Invoke();
        }
    }
}