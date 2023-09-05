using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextWave : MonoBehaviour
{
    public PhaseManager phaseManager;

    public void OnPointerEnter(BaseEventData eventData)
    {
        //동작 없음
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        //동작 없음
    }

    public void OnPointerClick(BaseEventData eventData)
    {
        phaseManager.InvokeNextWave();
    }
}
