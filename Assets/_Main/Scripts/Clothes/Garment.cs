using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garment : MonoBehaviour
{
    [SerializeField] private GarmentInfo garmentInfo;
    [SerializeField] private GarmentRandomizer garmentRandomizer;
    [SerializeField] private GameObject idleCloth;
    [SerializeField] private GameObject heldCloth;
    
    public GarmentInfo GetGarmentInfo() => garmentInfo;

    private void Start()
    {
        if(garmentRandomizer != null)
            garmentRandomizer.RandomizeMaterials(garmentInfo);
    }

    [ContextMenu("Randomize")]
    public void Randomize()
    {
        garmentRandomizer.RandomizeMaterials(garmentInfo);
    }

    public void OnGrab()
    {
        StopAllCoroutines();
        StartCoroutine(OnGrab_Coroutine());
        garmentInfo.OnPickup();
    }
    
    private IEnumerator OnGrab_Coroutine()
    {
        yield return new WaitForSeconds(0.2f);
        idleCloth.SetActive(false);

        heldCloth.SetActive(true);
    }
    
    public void OnRelease()
    {
        StopAllCoroutines();
     
        idleCloth.SetActive(true);
        heldCloth.SetActive(false);
        
        garmentInfo.OnRelease();
    }
    
    public void Clean()
    {
        garmentInfo.IsClean = true;
        garmentRandomizer.CleanGarment();
    }
    
    public void Dirty()
    {
        garmentInfo.IsClean = false;
        garmentRandomizer.DirtyGarment();
    }
    
}
