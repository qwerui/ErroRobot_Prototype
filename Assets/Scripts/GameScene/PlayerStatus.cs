using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//프로토타입용 임시 클래스
public class PlayerStatus : MonoBehaviour
{
    public int currentShield;
    public int currentHp;
    public int maxShield;
    public int maxHp;
    public int gold;
    
    public TMP_Text shieldText;
    public TMP_Text hpText;
    public TMP_Text goldText;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        shieldText.SetText($"{currentShield}/{maxShield}");
        hpText.SetText($"{currentHp}/{maxHp}");
        goldText.SetText($"{gold}");
    }

    public void GainShield()
    {
        maxShield += 30;
        currentShield += 30;
    }

    public void GainHp()
    {
        maxHp += 30;
        currentHp += 30;
    }

    public void GainGold()
    {
        gold += 50;
    }
}
