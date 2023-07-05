using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Animator[] animators;
    
    private static readonly int StartTrigger = Animator.StringToHash("Start");
    private static readonly int NextTrigger = Animator.StringToHash("Next");
    
    private bool _wasShown = false;

    public void StartTutorial()
    {
        if(_wasShown) return;
        
        foreach (var animator in animators)
        {
            animator.GetInteger("Start");
        }
    }
    
    public void MoveToNextStep()
    {
        if (_wasShown) return;
        
        foreach (var animator in animators)
        {
            animator.SetTrigger(NextTrigger);
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
