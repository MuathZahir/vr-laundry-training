using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarmentInfo : MonoBehaviour
{
    public event System.Action<GarmentInfo> OnPickUp = null;
    public event System.Action<GarmentInfo> OnPutDown = null;

    public bool IsHeld { get; set; } = false;
    public LaundryColor LaundryColor { get; set; }
    public StainType StainType { get; set; }
    public bool IsClean { get; set; } = false;
    
    //[SerializeField] private Animator animator;
    [SerializeField] private Garment garment;
    [SerializeField] private GameObject cloth;
    [SerializeField] private GameObject grabArea;
    [SerializeField] private bool overrideInfo = false;
    [SerializeField] private StainType stainType;
    [SerializeField] private LaundryColor laundryColor;
    
    public Garment GetGarment() => garment;

    private void Start()
    {
        cloth.SetActive(false);
        
        if (overrideInfo)
        {
            LaundryColor = laundryColor;
            StainType = stainType;
        }
    }

    public void SetParent(Transform parent)
    {
        garment.transform.SetParent(parent);
    }
    
    public void OnPickup()
    {
        IsHeld = true;
        OnPickUp?.Invoke(this);
    }
    
    public void OnRelease()
    {
        IsHeld = false;
        OnPutDown?.Invoke(this);
    }

    public void GrabAreaEnabled(bool isEnabled)
    {
        grabArea.SetActive(isEnabled);
    }
}
