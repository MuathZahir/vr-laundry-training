using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Serialization;

public class WashingMachine : LaundryContainer
{
    [SerializeField] private int targetLaundryCount = 3;
    [SerializeField] private Transform drum;
    [SerializeField] private float speed;
    [SerializeField] private MachineTimer timer;
    [SerializeField] private WashingMachineMenu menu;
    [SerializeField] private MachineDoor machineDoor;

    private HashSet<GarmentInfo> _clothes = new();
    private IEnumerator washingCoroutine;
    
    private void Start()
    {
        timer.OnTimerStart += StartRotating;
        timer.OnTimerStop += StopRotating;
        timer.OnTimerDone += StopRotating;
        
        machineDoor.OnDoorClosed += () => menu.ShowDoorOpenWarning(false);
    }
    
    public override void OnLaundryRemoved(GarmentInfo garment)
    {
        _clothes.Remove(garment);
    }

    public override void OnLaundryPlaced(GarmentInfo garment)
    {
        _clothes.Add(garment);
        
        garment.SetParent(drum);
        
        if (_clothes.Count == targetLaundryCount)
        {
        }
    }

    public override void Restart()
    {
        _clothes.Clear();
        machineDoor.CloseDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GarmentInfo>(out var garment) && !_clothes.Contains(garment))
        {
            garment.OnPutDown += OnLaundryPlaced;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<GarmentInfo>(out var garment))
        {
            garment.OnPutDown -= OnLaundryPlaced;
            if (_clothes.Contains(garment))
            {
                OnLaundryRemoved(garment);
            }
        }
    }
    
    private void StartRotating()
    {
        washingCoroutine = RotateDrum();
        StartCoroutine(washingCoroutine);
        
        // Lock door
        machineDoor.IsDoorEnabled(false);
    }
    
    private void StopRotating()
    {
        StopCoroutine(washingCoroutine);

        foreach (var garmentInfo in _clothes)
        {
            garmentInfo.GetGarment().Clean();
        }
        
        // Unlock door
        machineDoor.IsDoorEnabled(true);
    }
    
    // Begin rotating drum
    private IEnumerator RotateDrum()
    {
        // rotate drum
        while (true)
        {
            drum.Rotate(transform.up * (speed * Time.deltaTime));
            yield return null;
        }
    }
}
