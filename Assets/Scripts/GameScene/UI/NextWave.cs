using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NextWave : MonoBehaviour, IGameUI
{
    public PhaseManager phaseManager;
    GameUIController gameUIController;

    int index;
    Outline[] outlines;

    private void OnEnable() 
    {
        if(gameUIController == null)
        {
            gameUIController = GetComponent<GameUIController>();
            outlines = GetComponentsInChildren<Outline>();
            System.Array.Sort(outlines, (Outline a, Outline b)=>{return a.transform.GetSiblingIndex()-b.transform.GetSiblingIndex();});
        }
        index = 1;
        outlines[index].enabled = true;
        gameUIController.enabled = true;
    }

    // public void OnPointerEnter(BaseEventData eventData)
    // {
    //     //동작 없음
    // }

    // public void OnPointerExit(BaseEventData eventData)
    // {
    //     //동작 없음
    // }

    // public void OnPointerClick(BaseEventData eventData)
    // {
    //     phaseManager.InvokeNextWave();
    // }

    public void OnNavigate(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) < Mathf.Epsilon)
        {
            return;
        }
        
        outlines[index].enabled = false;
        index += direction.x > 0 ? 1 : -1;
        index = Mathf.Clamp(index, 0, 1);
        outlines[index].enabled = true;
    }

    public void OnSubmit_Mouse()
    {
        index = 0;
        OnSubmit();
    }

    public void OnSubmit()
    {
        /*
        GameUIController에서의 pointer 객체 On/Off는 건설 단계에만 작동해야한다.
        PhaseManager의 OnWaveStart, OnWaveEnd에서 pointer의 isBuild 변수가 변경되기 때문에
        OnWaveStart가 먼저 호출되어야 aim과 Pointer가 겹치지 않는다.
        (UIManager 참조)
        */

        if(index == 0)
        {
            phaseManager.InvokeNextWave();
        }

        OnCancel();
    }

    public void OnCancel()
    {
        foreach(Outline o in outlines)
        {
            o.enabled = false;
        }
        gameUIController.enabled = false;
        gameObject.SetActive(false);
    }
}
