using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Transform userRig;
    [SerializeField] private Transform playerOrigin;
    [SerializeField] private Transform playerHead;
    
    public void MoveToTransform(Transform targetTransform)
    {
        var targetPos = targetTransform.position;
        // Calculate the distance between the player and the position
        var distance = Vector3.Distance(transform.position, targetPos);
        
        // Calculate the time it will take to move to the position
        var time = distance / speed;
        
        var positionOffset = playerOrigin.position - playerHead.position;
        positionOffset.y = 0f;

        LeanTween.move(userRig.gameObject, targetPos, time).setOnComplete(() => Rotate(targetTransform));
        
    }
    
    public void Rotate(Transform targetTransform)
    {
        var targetRotation = targetTransform.rotation;
        
        // Rotate player
        Vector3 headForward = Vector3.ProjectOnPlane(playerHead.forward, playerOrigin.up).normalized;
        Quaternion headFlatRotation = Quaternion.LookRotation(headForward, playerOrigin.up);
        Quaternion rotationOffset = Quaternion.Inverse(playerOrigin.rotation) * headFlatRotation;
        
        var rotation = Quaternion.Inverse(rotationOffset) * targetRotation;
        playerOrigin.rotation = rotation;
        Debug.Log("Player rotation: " + playerOrigin.eulerAngles.y);
    }
}
