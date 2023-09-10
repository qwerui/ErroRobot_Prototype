using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerStatus : MonoBehaviour
{
    public float currentShield;
    public float currentHp;
    public float maxShield;
    public float maxHp;
    public float shieldRecovery;
    public float coreGainPercent;
    public int core;

    public int killCount;

    public delegate void OnDeadDelegate();
    public delegate void OnValueChangedDelegate(PlayerStatus newStatus);
    public delegate void OnDamagedDelegate(GameObject source);

    public event OnDeadDelegate onDead;
    public event OnValueChangedDelegate OnValueChanged;
    public event OnDamagedDelegate OnDamaged;

    public void Init(StartStatus startStatus)
    {  
        maxHp = startStatus.maxHp;
        maxShield = startStatus.maxShield;
        currentShield = maxShield;
        currentHp = maxHp;
        shieldRecovery = startStatus.shieldRecovery;
        core = startStatus.startCore;
        coreGainPercent = startStatus.coreGainPercent;
        
        killCount = 0;
        
        OnValueChanged.Invoke(this);
    }

    public void Damaged(float damage, GameObject source)
    {
        float remainShield = currentShield - damage;

        if(remainShield <= 0)
        {
            //실드 초과 데미지
            currentHp = Mathf.Clamp(currentHp+remainShield, 0, maxHp);
        }
        
        currentShield = Mathf.Clamp(remainShield, 0, maxShield);

        OnValueChanged.Invoke(this);
        OnDamaged.Invoke(source);
        
        if (currentHp <= 0)
        {
            onDead.Invoke();
        }
    }
}
