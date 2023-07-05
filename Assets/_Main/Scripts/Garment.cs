using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garment : MonoBehaviour
{
    [SerializeField] private GarmentInfo garmentInfo;
    [SerializeField] private GameObject idleCloth;
    [SerializeField] private GameObject heldCloth;
    
    
    public GarmentInfo GetGarmentInfo() => garmentInfo;

    public void OnGrab()
    {
        StopAllCoroutines();
        StartCoroutine(OnGrab_Coroutine());
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
}
