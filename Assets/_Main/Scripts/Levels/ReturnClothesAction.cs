using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Samples;
using UnityEngine;

public class ReturnClothesAction : LevelRestartAction
{
    [SerializeField] private Transform clothesParent;
    private List<GarmentRespawner> clothes = new List<GarmentRespawner>();

    private void Start()
    {
        foreach (Transform child in clothesParent)
        {
            clothes.Add(child.GetComponent<GarmentRespawner>());
        }
    }

    public override void PerformAction()
    {
        foreach (var garment in clothes)
        {
            garment.Respawn();
        }
    }
}
