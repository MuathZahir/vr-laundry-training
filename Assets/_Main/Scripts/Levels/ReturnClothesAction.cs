using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Samples;
using UnityEngine;

public class ReturnClothesAction : LevelRestartAction
{
    [SerializeField] private Transform clothesParent;
    [SerializeField] private List<Transform> clothes = new List<Transform>();
    public override void PerformAction()
    {
        foreach (var garment in clothes)
        {
            garment.SetParent(clothesParent);
            // Assuming that the clothes have the respawn on drop script and that the threshold is less than 50
            garment.position -= new Vector3(0, 50, 0);
        }
    }
}
