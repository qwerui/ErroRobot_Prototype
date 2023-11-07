using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotSpawn : IWaitConditionBase
{

    private Camera mainCamera;
    private GameObject turretHead;

    private Vector3 prevPosition;
    private Quaternion prevRotation;
    private Vector3 newPosition;
    private Quaternion newRotation;
    
    public void Init()
    {
        mainCamera = Camera.current;
        turretHead = mainCamera.gameObject.transform.Find("turret_head1").gameObject;
        turretHead.SetActive(false);

        prevRotation = Quaternion.Euler(30, 90, 0);
        prevPosition = new Vector3(0, 0, 0);
        
        newRotation = Quaternion.Euler(30, 90, 0);
        newPosition = new Vector3(100, -30, 0);
        
        mainCamera.transform.localPosition = newPosition;
        mainCamera.transform.localRotation = newRotation;
        
    }
    
    public IEnumerator ConditionCheck()
    {
        yield return new WaitForSeconds(5f);
        Remove();
        yield break;
    }
    
    public bool Check()
    {
        return true;
    }
    
    public void Remove()
    {
        TutorialManager.instance.Next();
        turretHead.SetActive(true);

        mainCamera.transform.localPosition = prevPosition;
        mainCamera.transform.localRotation = prevRotation;
    }
    
}
