using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldedTowel : MonoBehaviour
{
    public event System.Action<FoldedTowel> OnPickUp = null;
    public event System.Action<FoldedTowel> OnPutDown = null;

    public bool IsHeld { get; set; } = false;
    
    //[SerializeField] private Animator animator;

    private void OnEnable()
    {
        LevelManager.OnLevelRestart += OnRestart;
    }
    
    private void OnDisable()
    {
        LevelManager.OnLevelRestart -= OnRestart;
    }

    public void OnPickup()
    {
        IsHeld = true;
        OnPickUp?.Invoke(this);
    }
    
    public void OnRelease()
    {
        IsHeld = false;
        OnPutDown?.Invoke(this);
    }

    private void OnRestart()
    {
        Destroy(transform.parent.gameObject);
    }
}
