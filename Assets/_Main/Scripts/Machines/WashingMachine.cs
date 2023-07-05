using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : LaundryContainer
{
    [SerializeField] private int targetLaundryCount = 3;
    [SerializeField] private AudioClip laundryPlacedSound;
    [SerializeField] private AudioClip laundryRemovedSound;
    [SerializeField] private Transform door;
    [SerializeField] private Transform drum;
    [SerializeField] private float speed;
    [SerializeField] private MachineTimer timer;

    private HashSet<GarmentInfo> _clothes = new();
    private AudioSource _audioSource;
    private IEnumerator washingCoroutine;
    
    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        
        timer.OnTimerStart += StartRotating;
        timer.OnTimerDone += StopRotating;
    }
    
    public override void OnLaundryRemoved(GarmentInfo garment)
    {
        if(_clothes.Count == 0)
            throw new Exception("Removing laundry from empty washing machine");
        
        _clothes.Remove(garment);
        
        _audioSource.PlayOneShot(laundryRemovedSound);
    }

    public override void OnLaundryPlaced(GarmentInfo garment)
    {
        if(_clothes.Count == targetLaundryCount)
            throw new Exception("Adding laundry to full washing machine");
        
        _clothes.Add(garment);
        
        garment.SetParent(drum);

        _audioSource.PlayOneShot(laundryPlacedSound);
        
        if (_clothes.Count == targetLaundryCount)
        {
            // TODO: Play winning animation
            // TODO: Move to next level
            
            LevelManager.Instance.CurrentLevel.CompleteLevel();
        }
    }

    public override void Restart()
    {
        _clothes.Clear();
        door.localRotation = Quaternion.Euler(0, 0, 0);
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
    }
    
    private void StopRotating()
    {
        StopCoroutine(washingCoroutine);
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
