using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class Match : MonoBehaviour
{
    public static event Action<Transform> OnMatchGrabbed;
    
    public void OnMatchGrab()
    {
        //GameObject.Find("HandGrabInteractorRight").GetComponent<HandGrabInteractor>().Unselect();
        Debug.Log("Match Unselected");
    }
}
