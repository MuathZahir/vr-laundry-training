using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomResetAction : LevelRestartAction
{
    [SerializeField] private UnityEvent onReset;
    public override void PerformAction()
    {
        onReset?.Invoke();
    }
}
