using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TempReward : MonoBehaviour
{
    public PlayerStatus status;

    public void Gold10(BaseEventData eventData)
    {
        status.gold+=10;
        status.UpdateText();
        gameObject.SetActive(false);
    }

    public void Gold20(BaseEventData eventData)
    {
        status.gold+=20;
        status.UpdateText();
        gameObject.SetActive(false);
    }

    public void Gold30(BaseEventData eventData)
    {
        status.gold+=30;
        status.UpdateText();
        gameObject.SetActive(false);
    }
}
