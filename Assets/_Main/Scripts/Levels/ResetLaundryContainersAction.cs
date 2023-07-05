using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLaundryContainersAction : LevelRestartAction
{
    [SerializeField] private List<LaundryContainer> laundryContainers = new List<LaundryContainer>();

    public override void PerformAction()
    {
        foreach (var container in laundryContainers)
        {
            container.Restart();
        }
    }
}
