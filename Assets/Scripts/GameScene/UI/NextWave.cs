using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextWave : MonoBehaviour
{
    public PhaseManager phaseManager;

    public void OnPointerEnter(BaseEventData eventData)
    {

    }

    public void OnPointerExit(BaseEventData eventData)
    {

    }

    public void OnPointerClick(BaseEventData eventData)
    {
        phaseManager.OnWaveStart.Invoke();
    }
}
