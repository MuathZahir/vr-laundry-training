using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarmentInfo : MonoBehaviour
{
    public event System.Action<GarmentInfo> OnPickUp = null;
    public event System.Action<GarmentInfo> OnPutDown = null;

    public bool IsHeld { get; set; } = false;
    
    //[SerializeField] private Animator animator;
    [SerializeField] private Garment garment;
    [SerializeField] private LaundryColor color;
    [SerializeField] private StainType stainType;
    [SerializeField] private GameObject cloth;
    
    public LaundryColor GetColor() => color;
    public StainType GetStainType() => stainType;

    private void Start()
    {
        cloth.SetActive(false);
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

}
