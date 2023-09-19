using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TowerSlot : MonoBehaviour
{
    Tower tower;
    public Tower Tower
    {
        set {tower = value;}
        get {return tower;}
    }

    private void OnEnable() 
    {

    }

    public void OnPointerEnter(BaseEventData eventData)
    {

    }

    public void OnPointerExit(BaseEventData eventData)
    {
        
    }
}
