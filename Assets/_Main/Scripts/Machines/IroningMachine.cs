using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IroningMachine : MonoBehaviour
{
    [SerializeField] private ConveyorBelt conveyorBelt;
    [SerializeField] private IroningMachineCollector collector;
    [SerializeField] private MachineMenu menu;

    private AudioSource _audioSource;

    private void Awake()
    {
    }

    public void StartConveyorBelt(float speedMultiplier)
    {
        conveyorBelt.SpeedMultiplier = speedMultiplier;
        conveyorBelt.IsOn = true;
    }
    
    public void StopConveyorBelt()
    {
        conveyorBelt.IsOn = false;
    }

    public void Restart()
    {
        StopConveyorBelt();
        collector.Restart();
        menu.RestartMenu();
    }
}
