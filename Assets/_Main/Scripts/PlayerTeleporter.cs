using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Transform _playerOrigin;
    [SerializeField] private Transform _playerHead;
    [SerializeField] private OVREyeGaze _eyeTracker;
    
    public void MoveToTransform(Transform targetTransform)
    {
        var targetPos = targetTransform.position;
        // Calculate the distance between the player and the position
        var distance = Vector3.Distance(transform.position, targetPos);
        
        // Calculate the time it will take to move to the position
        var time = distance / speed;
        
        var positionOffset = _playerOrigin.position - _playerHead.position;
        positionOffset.y = 0f;

        LeanTween.move(_playerOrigin.gameObject, targetPos + positionOffset, time);
        
        var targetRotation = targetTransform.rotation;
        
        // Rotate player
        Vector3 headForward = Vector3.ProjectOnPlane(_playerHead.forward, _playerOrigin.up).normalized;
        Quaternion headFlatRotation = Quaternion.LookRotation(headForward, _playerOrigin.up);
        Quaternion rotationOffset = Quaternion.Inverse(_playerOrigin.rotation) * headFlatRotation;
        
        var rotation = Quaternion.Inverse(rotationOffset) * targetRotation;
        _playerOrigin.rotation = rotation;
        Debug.Log("Player rotation: " + _playerOrigin.eulerAngles.y);
        _eyeTracker.SetHeadRotation(-_playerOrigin.eulerAngles.y);
    }
}
