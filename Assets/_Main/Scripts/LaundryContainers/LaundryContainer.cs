using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaundryContainer : MonoBehaviour
{
    public abstract void OnLaundryRemoved(GarmentInfo garment);
    public abstract void OnLaundryPlaced(GarmentInfo garment);
    
    public abstract void Restart();
}
