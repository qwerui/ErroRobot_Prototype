using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//프로토타입용 임시 클래스
public class PlayerStatus : MonoBehaviour
{
    public float currentShield;
    public float currentHp;
    public float maxShield;
    public float maxHp;
    public int gold;
    
    public TMP_Text shieldText;
    public TMP_Text hpText;
    public TMP_Text goldText;

    public GameplayManager gameplayManager;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        shieldText.SetText($"{currentShield:0}/{maxShield:0}");
        hpText.SetText($"{currentHp:0}/{maxHp:0}");
        goldText.SetText($"{gold}");
    }

    public void Damaged(float damage)
    {
        float remainShield = currentShield - damage;

        if(remainShield <= 0)
        {
            //실드 초과 데미지
            currentHp = Mathf.Clamp(currentHp+remainShield, 0, maxHp);
        }
        
        currentShield = Mathf.Clamp(remainShield, 0, maxShield);

        UpdateText();

        if (currentHp <= 0)
        {
            gameplayManager.Gameover();
        }
    }
}
