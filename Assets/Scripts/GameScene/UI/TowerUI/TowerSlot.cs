using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSlot : MonoBehaviour
{
    Tower tower;
    public Image towerImage;
    public Image HpBar;
    public Outline outline;

    public Tower Tower
    {
        set 
        {
            tower = value;
            if(tower != null)
            {
                towerImage.sprite = tower.icon;
                tower.OnValueChange += UpdateHpBar;
                HpBar.fillAmount = 1.0f;
                towerImage.enabled = true;
                HpBar.enabled = true;
            }
            else
            {
                towerImage.enabled = false;
                HpBar.enabled = false;
            }
        }
        get {return tower;}
    }

    public void Activate() => outline.enabled = true;
    public void Deactivate() => outline.enabled = false;

    public void AlterTower(Tower newTower)
    {
        Destroy(tower.gameObject);
        tower = newTower;
    }

    void UpdateHpBar()
    {
        float hpPercentage = tower.CurrentHp / tower.MaxHp;
        HpBar.fillAmount = hpPercentage;

        if(hpPercentage > 0.5f)
        {
            HpBar.color = Color.green;
        }
        else if(hpPercentage > 0.2f)
        {
            HpBar.color = Color.yellow;
        }
        else
        {
            HpBar.color = Color.red;
        }
    }

    public SerializedTower GetSerializedTower()
    {
        if (tower != null)
        {
            return new SerializedTower
            {
                level = tower.Level,
                position = tower.transform.position
            };
        }
        return null;
    }

    public void OnPointerEnter(BaseEventData eventData)
    {

    }

    public void OnPointerExit(BaseEventData eventData)
    {
        
    }
}
