using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class TowelGatherer : MonoBehaviour
{
    [SerializeField] private AudioClip rightClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TowelSpawner towelSpawner;

    private HashSet<FoldedTowel> _clothes = new();
    
    public void OnLaundryRemoved(FoldedTowel garment)
    {
        Debug.Log("Removing laundry from basket");
        
        // Clothes.RemainingClothes++;
        LevelManager.Instance.CurrentLevel.ClothesRemaining++;
        
        _clothes.Remove(garment);
    }

    public void OnLaundryPlaced(FoldedTowel garment)
    {
        garment.OnPutDown -= OnLaundryPlaced;

        if (_clothes.Contains(garment)) return;
        
        var level = LevelManager.Instance.CurrentLevel;
        
        // Clothes.RemainingClothes--;
        level.ClothesRemaining--;
        
        towelSpawner.CanSpawn();
        
        // if (Clothes.RemainingClothes == 0)
        if (level.ClothesRemaining == 0)
        {
            Debug.LogWarning("Level complete!");
            LevelManager.Instance.LevelComplete();
        }
        
        _clothes.Add(garment);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FoldedTowel>(out var towel) && !_clothes.Contains(towel))
        {
            Debug.Log(other.transform.parent.name);

            audioSource.PlayOneShot(rightClip);
            
            towel.OnPutDown += OnLaundryPlaced;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FoldedTowel>(out var towel))
        {
            towel.OnPutDown -= OnLaundryPlaced;
            if (_clothes.Contains(towel))
            {
                OnLaundryRemoved(towel);
            }
        }
    }
    
    public void Restart()
    {
        _clothes.Clear();
    }
}
