using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMachinesAction : LevelRestartAction
{
    [SerializeField] private List<MachineMenu> menus = new List<MachineMenu>();
    
    public override void PerformAction()
    {
        foreach (var menu in menus)
        {
            menu.RestartMenu();
        }
    }
}
