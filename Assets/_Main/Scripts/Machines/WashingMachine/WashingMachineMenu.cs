using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineMenu : MachineMenu
{
    [SerializeField] private GameObject doorOpenWarning;
    [SerializeField] private MachineTimer timer;
    [SerializeField] private GameObject timeSelectMenu;

    private bool _isDone = false;
    
    private void Start()
    {
        timer.OnTimerDone += OnTimerDone;
    }

    public void ShowDoorOpenWarning(bool show)
    {
        if (_isDone)
        {
            RestartMenu();
        }
        timeSelectMenu.SetActive(!show);
        doorOpenWarning.SetActive(show);
    }
    
    private void OnTimerDone()
    {
        NextPage();
        _isDone = true;
    }

    public override void RestartMenu()
    {
        base.RestartMenu();
        _isDone = false;
    }
}
