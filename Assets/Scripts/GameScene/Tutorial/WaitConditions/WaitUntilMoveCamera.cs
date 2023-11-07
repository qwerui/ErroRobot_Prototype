using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilMoveCamera : IWaitConditionBase
{

    private CameraController cameraController;
    private int code;

    public void Init()
    {
        PlayerController.instance.AddController(PhaseManager.instance.buildController);
        code = 0;
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        cameraController.OnCameraMoved.AddListener(Test);
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
        return code == 15;
    }

    public void Test(Vector2 direction)
    {
        int t = 0;
        if (direction.x > 0)
            t = 0;
        else if (direction.x < 0)
            t = 1;
        else if (direction.y > 0)
            t = 2;
        else if (direction.y < 0)
            t = 3;
        
        code |= (1 << t);
    }

    public void Remove()
    {
        cameraController.OnCameraMoved.RemoveListener(Test);
        PhaseManager.instance.buildController.cameraController.DisableRotation();
        PlayerController.instance.DeleteController(PhaseManager.instance.buildController);
        TutorialManager.instance.Next();
    }
    
}
