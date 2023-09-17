using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]
public class PlayerStatus : MonoBehaviour
{
    [SerializeField, HideInInspector]
    float currentShield;
    [SerializeField, HideInInspector]
    float currentHp;
    [SerializeField, HideInInspector]
    float maxShield;
    [SerializeField, HideInInspector]
    float maxHp;
    [SerializeField, HideInInspector]
    float shieldRecovery;
    [SerializeField, HideInInspector]
    float coreGainPercent;
    [SerializeField, HideInInspector]
    int core;
    [SerializeField, HideInInspector]
    int playCount;
    [SerializeField, HideInInspector]
    int killCount;

#region Property
    public float CurrentShield
    {
        set{currentShield = Mathf.Clamp(value, 0, maxShield);}
        get{return currentShield;}
    }
    public float CurrentHp
    {
        set{currentHp = Mathf.Clamp(value, 0, maxHp);}
        get{return currentHp;}
    }
    public float MaxShield
    {
        set{maxShield = value;}
        get{return maxShield;}
    }
    public float MaxHp
    {
        set{maxHp = value;}
        get{return maxHp;}
    }
    public float ShieldRecovery
    {
        set{shieldRecovery = value;}
        get{return shieldRecovery;}
    }
    public float CoreGainPercent
    {
        set{coreGainPercent = value;}
        get{return coreGainPercent;}
    }
    public int Core
    {
        set{core = value;}
        get{return core;}
    }
    public int KillCount
    {
        set{killCount = value;}
        get{return killCount;}
    }
    public int PlayCount
    {
        set{playCount = value;GameManager.instance.achievementManager.CheckAchievement(AchievementEvent.PlayCount, playCount);}
        get{return playCount;}
    }
#endregion

    public delegate void OnDeadDelegate();
    public delegate void OnValueChangedDelegate(PlayerStatus newStatus);
    public delegate void OnDamagedDelegate(GameObject source);

    public event OnDeadDelegate OnDead;
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

    public void Enhance(StatusType type, float value)
    {
        switch(type)
        {
            case StatusType.MaxHP:
                MaxHp += value;
                CurrentHp += value;
            break;
            case StatusType.MaxShield:
                MaxShield += value;
                CurrentShield += value;
            break;
            case StatusType.ShieldRecover:
                ShieldRecovery += value;
            break;
            case StatusType.CoreGain:
                CoreGainPercent += value;
            break;
        }
        OnValueChanged.Invoke(this);
    }

    public void GainCore(float value)
    {
        Core += (int)(value * CoreGainPercent);
    }

    public void Damaged(float damage, GameObject source)
    {
        
        float remainDamage = damage - currentShield;
        currentShield -= damage;

        if(remainDamage > 0)
        {
            //실드 초과 데미지
            currentHp -= damage;
        }

        OnValueChanged.Invoke(this);
        OnDamaged.Invoke(source);
        
        if (currentHp <= 0)
        {
            OnDead.Invoke();
        }
    }
}
