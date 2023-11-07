using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilTurretPlaced : IWaitConditionBase
{

    private bool placed = false;
    
    public void Init()
    {
        PlayerController.instance.AddController(PhaseManager.instance.buildController);
        Tower tower = Resources.Load<TowerMapper>($"Reward/Tower/0").tower;
        TowerManager._instance.CreateTower(tower).OnPut += Test;
    }
    
    public IEnumerator ConditionCheck()
    {
        while (true)
        {
            yield return null;
            if (Check())
            {
                Remove(); 
                yield break;
            }
        }
    }
    


    public bool Check()
    {
        return placed;
    }

    public void Remove()
    {
        PhaseManager.instance.buildController.cameraController.DisableRotation();
        PlayerController.instance.DeleteController(PhaseManager.instance.buildController);
        TutorialManager.instance.Next();
    }

    public void Test()
    {
        placed = true;
    }
    
}
