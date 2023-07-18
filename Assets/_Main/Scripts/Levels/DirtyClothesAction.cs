using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyClothesAction : LevelRestartAction
{
    [SerializeField] private Transform clothesParent;
    private List<Garment> clothes = new List<Garment>();

    private void Start()
    {
        foreach (Transform child in clothesParent)
        {
            clothes.Add(child.GetComponent<Garment>());
        }
    }

    public override void PerformAction()
    {
        foreach (var garment in clothes)
        {
            garment.Dirty();
        }
    }
}
