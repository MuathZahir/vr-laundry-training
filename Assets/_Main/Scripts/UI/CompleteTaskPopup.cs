using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.UI;

public class CompleteTaskPopup : MonoBehaviour
{
    [SerializeField] private Button nextTaskButton;
    [SerializeField] private float animationDuration = 0.5f;

    private Vector3 _initialScale;
    
    private void Awake()
    {
        nextTaskButton.onClick.AddListener(MoveToNext);
        _initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, _initialScale, animationDuration).setEase(LeanTweenType.easeOutBack);
    }
    
    private void MoveToNext()
    {
        LevelManager.Instance.MoveToNextLevel();
    }

}
