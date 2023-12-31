using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.UI;

public class CompleteTaskPopup : MonoBehaviour
{
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private GameObject button;

    private Vector3 _initialScale;
    
    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, _initialScale, animationDuration).setEase(LeanTweenType.easeOutBack)
            .setOnComplete(() => button.SetActive(true));
    }

    private void OnDisable()
    {
        button.SetActive(false);
    }

    public void MoveToNext()
    {
        LevelManager.Instance.MoveToNextLevel();
    }

}
