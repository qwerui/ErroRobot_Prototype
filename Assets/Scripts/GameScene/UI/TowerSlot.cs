using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour
{
    public BaseTower tower;
    TowerDetail towerDetail;

    private void OnEnable() 
    {
        towerDetail = GameObject.FindObjectOfType<TowerDetail>(true);
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        if(towerDetail != null)
        {
            towerDetail.ShowTowerInfo(tower);
        }
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        
    }
}
