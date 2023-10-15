using System;
using System.Collections;
using System.Collections.Generic;
using UCloth;
using UnityEngine;

public class TowelSpawner : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private Towel towelTemplate;
    [SerializeField] private bool instantiate;
    
    private bool _canSpawn = true;
    private List<Towel> _spawnedTowels = new List<Towel>();
    private int _current = 0;
    
    [ContextMenu("Spawn Towel")]
    private void SpawnTowel()
    {
        tutorial.gameObject.SetActive(true);

        Towel towel;
        
        if (instantiate)
        {
            towel = Instantiate(towelTemplate, towelTemplate.transform.position, 
                towelTemplate.transform.rotation, transform.parent);
        }
        else
        {
            if (_current >= transform.childCount) return;
            towel = transform.GetChild(_current).GetComponent<Towel>();
        }
        
        towel.gameObject.SetActive(true);
        
        _canSpawn = false;
        towel.OnPlacedTowel += OnFoldedTowelPlaced;
        
        _spawnedTowels.Add(towel);
        _current++;
    }
    
    private void OnFoldedTowelPlaced()
    {
        _canSpawn = true;
        LevelManager.Instance.CurrentLevel.ClothesRemaining--;
        
        if (LevelManager.Instance.CurrentLevel.ClothesRemaining == 0)
        {
            LevelManager.Instance.LevelComplete();
        }
    }
        
    private void OnLaundryPlaced(GarmentInfo garment)
    {
        garment.OnPutDown -= OnLaundryPlaced;
        garment.transform.parent.gameObject.SetActive(false);
        SpawnTowel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GarmentInfo>(out var garment) && _canSpawn)
        {
            if (!garment.IsHeld) return;
            
            Debug.Log(other.transform.parent.name);
            
            garment.OnPutDown += OnLaundryPlaced;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<GarmentInfo>(out var garment))
        {
            garment.OnPutDown -= OnLaundryPlaced;
        }
    }

    public void Restart()
    {
        foreach (var towel in _spawnedTowels)
        {
            Destroy(towel.gameObject);
        }
        _spawnedTowels.Clear();
        _canSpawn = true;
    }

    public void CanSpawn()
    {
        _canSpawn = true;
    }
    
    // [ContextMenu("Restart")]
    // public void Restart()
    // {
    //     for (int i = 0; i < LevelManager.Instance.CurrentLevel.ClothesRemaining; i++)
    //     {
    //         Instantiate(towelTemplate, towelTemplate.transform.position,
    //             towelTemplate.transform.rotation, transform.parent);
    //     }
    // }
}
