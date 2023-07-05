using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UCloth;
using UnityEngine;

public class Towel : MonoBehaviour
{
    [HideInInspector] public List<TowelFixedPoint> FixedPoints = new List<TowelFixedPoint>();
    
    [SerializeField] private Transform fixedPoints;
    [SerializeField] private Tutorial tutorial;

    private UCCloth _cloth;

    private void Awake()
    {
        _cloth = gameObject.GetComponent<UCCloth>();
    }

    public void CheckIfFolded()
    {
        switch (FixedPoints.Count)
        {
            // Folded first time
            case 4:
                tutorial.MoveToNextStep();
                break;
            // Folded second time
            case 2:
                _cloth.enabled = false;
                tutorial.CompleteTutorial();
                break;
        }
    }

}
