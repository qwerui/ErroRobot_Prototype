using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class WaitUntilRobotRemoved : IWaitConditionBase
{

    private int code;

    public void Init()
    {
        PlayerController.instance.AddController(PhaseManager.instance.defenceController);
        code = 0;
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
        return TutorialManager.instance.tutorialEnemy.isDead;
    }

    public void Remove()
    {
        PhaseManager.instance.defenceController.cameraController.DisableRotation();
        PhaseManager.instance.defenceController.weaponController.ReleaseButton();
        PlayerController.instance.DeleteController(PhaseManager.instance.defenceController);
        TutorialManager.instance.Next();
    }
    
}
