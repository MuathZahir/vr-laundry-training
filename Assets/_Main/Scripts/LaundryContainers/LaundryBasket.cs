using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryBasket : LaundryContainer
{
    [SerializeField] private StainType stainType;
    [SerializeField] private LaundryColor color;
    [SerializeField] private bool isClean;
    [SerializeField] private GameObject rightLight;
    [SerializeField] private GameObject wrongLight;
    [SerializeField] private AudioClip rightClip;
    [SerializeField] private AudioClip wrongClip;
    [SerializeField] private AudioSource audioSource;

    private HashSet<GarmentInfo> _clothes = new();
    
    private void ResetLights()
    {
        rightLight.SetActive(false);
        wrongLight.SetActive(false);
    }
    
    public override void OnLaundryRemoved(GarmentInfo garment)
    {
        Debug.Log("Removing laundry from basket");
        
        // Clothes.RemainingClothes++;
        LevelManager.Instance.CurrentLevel.ClothesRemaining++;
        
        _clothes.Remove(garment);
    }

    public override void OnLaundryPlaced(GarmentInfo garment)
    {
        garment.OnPutDown -= OnLaundryPlaced;
        
        garment.SetParent(transform);
        
        var level = LevelManager.Instance.CurrentLevel;
        
        // Clothes.RemainingClothes--;
        level.ClothesRemaining--;
        
        Debug.LogWarning("Clothes remaining: " + level.ClothesRemaining);
        
        // if (Clothes.RemainingClothes == 0)
        if (level.ClothesRemaining == 0)
        {
            Debug.LogWarning("Level complete!");
            LevelManager.Instance.LevelComplete();
        }
        
        _clothes.Add(garment);
        ResetLights();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GarmentInfo>(out var garment) && !_clothes.Contains(garment))
        {
            if (!garment.IsHeld) return;
            
            Debug.Log(other.transform.parent.name);
            var correct = IsCorrect(garment);

            rightLight.SetActive(correct);
            wrongLight.SetActive(!correct);

            audioSource.PlayOneShot(correct ? rightClip : wrongClip);
            
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
            
            ResetLights();
        }
    }
    
    private bool IsCorrect(GarmentInfo clothes)
    {
        var correctColor = color == LaundryColor.Any || clothes.LaundryColor == color;
        var correctStain = stainType == StainType.Any || clothes.StainType == stainType;
        
        return correctColor && correctStain && isClean == clothes.IsClean;
    }
    
    public override void Restart()
    {
        ResetLights();
        _clothes.Clear();
    }
}

[System.Serializable]
public enum LaundryColor
{
    Any,
    Light,
    Dark,
    Color
}

[System.Serializable]
public enum StainType
{
    Any,
    Hard,
    Soft
}