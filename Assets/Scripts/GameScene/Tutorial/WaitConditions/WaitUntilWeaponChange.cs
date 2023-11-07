using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilWeaponChange : IWaitConditionBase
{
    private bool weaponChanged;

    public void Init()
    {
        PlayerController.instance.AddController(PhaseManager.instance.defenceController);
        PhaseManager.instance.defenceController.weaponManager.OnWeaponChange.AddListener(Test);
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
        return weaponChanged;
    }

    public void Test()
    {
        weaponChanged = true;
        
    }

    public void Remove()
    {
        PhaseManager.instance.defenceController.cameraController.DisableRotation();
        PhaseManager.instance.defenceController.weaponController.ReleaseButton();
        PlayerController.instance.DeleteController(PhaseManager.instance.defenceController);
        TutorialManager.instance.Next();
        PhaseManager.instance.defenceController.weaponManager.OnWeaponChange.RemoveListener(Test);
    }
    
}
