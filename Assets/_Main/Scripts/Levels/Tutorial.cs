using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Animator[] animators;
    
    private bool _wasShown = false;

    private void OnEnable()
    {
        StartTutorial();
    }

    public void StartTutorial()
    {
        if(_wasShown) return;
        
        foreach (var animator in animators)
        {
            animator.SetTrigger("Start");
        }
    }
    
    public void MoveToNextStep()
    {
        if (_wasShown) return;
        
        foreach (var animator in animators)
        {
            animator.SetTrigger("Next");
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        _wasShown = false;
    }

    public void CompleteTutorial()
    {
        gameObject.SetActive(false);
        _wasShown = true;
    }
}
