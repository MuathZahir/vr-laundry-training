using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Transform _playerOrigin;
    [SerializeField] private Transform _playerHead;
    
    public void MoveToPosition(Vector3 targetPosition)
    {
        // Calculate the distance between the player and the position
        var distance = Vector3.Distance(transform.position, targetPosition);
        
        // Calculate the time it will take to move to the position
        var time = distance / speed;
        
        var positionOffset = _playerOrigin.position - _playerHead.position;
        positionOffset.y = 0f;

        LeanTween.move(_playerOrigin.gameObject, targetPosition + positionOffset, time);
    }
}
