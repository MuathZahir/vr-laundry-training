using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IroningMachineCollector : MonoBehaviour
{
    private List<GarmentInfo> _garments = new List<GarmentInfo>();
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<GarmentInfo>(out var garment) && !_garments.Contains(garment))
        {
            _garments.Add(garment);

            LevelManager.Instance.CurrentLevel.ClothesRemaining--;
            
            if (LevelManager.Instance.CurrentLevel.ClothesRemaining == 0)
            {
                LevelManager.Instance.LevelComplete();
            }
        }
    }

    public void Restart()
    {
        _garments.Clear();
    }
}
