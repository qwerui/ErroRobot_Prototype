using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TowerDetail : MonoBehaviour
{
    public TMP_Text towerName;
    public TMP_Text towerDamage;
    public TMP_Text towerRange;
    public TMP_Text towerDelay;

    Transform[] children;

    private void Start() {
        children = GetComponentsInChildren<Transform>();
    }

    public void ShowTowerInfo(BaseTower tower)
    {
        towerName.SetText(tower.name);
        towerDamage.SetText($"Damage: {tower.weapon.damage:0}");
        towerRange.SetText($"Range: {tower.range:0}");
        towerDelay.SetText($"Delay: {tower.weapon.fireDelay:0.00}");
        gameObject.SetActive(true);
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        PointerEventData pointerEvent = eventData as PointerEventData;
        bool meetChild = false;

        foreach(Transform child in children)
        {
            if(child.gameObject == pointerEvent.pointerCurrentRaycast.gameObject)
            {
                meetChild = true;
                break;
            }
        }

        if(!meetChild)
        {
            gameObject.SetActive(false);
        }
    }
}

