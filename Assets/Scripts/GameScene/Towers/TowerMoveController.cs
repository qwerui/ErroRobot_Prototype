using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMoveController : MonoBehaviour, IControllerBase
{
    public CameraController cameraController;
    Tower targetTower;

    //타워 위치를 최신화하기 위한 레이캐스트
    Ray ray;
    RaycastHit[] hits;

    private void OnEnable() 
    {
        PlayerController.instance.AddController(this);
        
        hits = new RaycastHit[10];
    }

    private void OnDisable() 
    {
        PlayerController.instance?.DeleteController(this);    
    }

    /// <summary>
    /// 움직일 타워를 할당한다. 이 때 컨트롤러가 활성화된다.
    /// </summary>
    /// <param name="tower">대상이될 타워</param>
    public void SetTower(Tower tower)
    {
        targetTower = tower;
        gameObject.SetActive(true);
    }

    private void FixedUpdate() 
    {
        //레이캐스트를 통한 타워 이동
        ray = new Ray(cameraController.transform.position, cameraController.transform.forward);
        Physics.RaycastNonAlloc(ray, hits, 500);

        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.CompareTag("Ground"))
            {
                targetTower.Move(hit.point);
                break;
            }
        }
    }

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            targetTower.RevertMove();
            gameObject.SetActive(false);
        }
    }

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        cameraController.controlWithKey(direction);
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            targetTower.Put();
            gameObject.SetActive(false);
        }
    }
}
