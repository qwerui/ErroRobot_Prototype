using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]
public class PlayerStatus : MonoBehaviour
{
#region Field
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
    int killCount;
    [SerializeField, HideInInspector]
    int gainedAllCore;
    [SerializeField]
    GameRecord gameRecord;
    int beforeKillCount;
#endregion

    float recoverDelay = 0;

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
        set
        {
            killCount = value;
            gameRecord.killCount = killCount + beforeKillCount;
            OnChange_KillCount?.Invoke(gameRecord.killCount);
            SaveGameRecord();
        }
        get{return killCount;}
    }
    public int PlayCount
    {
        set
        {
            gameRecord.playCount = value;
            OnChange_PlayCount?.Invoke(gameRecord.playCount);
            SaveGameRecord();
        }
        get{return gameRecord.playCount;}
    }
    public int ClearCount
    {
        set
        {
            gameRecord.clearCount = value;
            OnChange_ClearCount?.Invoke(gameRecord.clearCount);
            SaveGameRecord();
        }
        get{return gameRecord.clearCount;}
    }
    public int WaveCount
    {
        set
        {
            gameRecord.waveCount = value;
            OnChange_WaveCount?.Invoke(gameRecord.waveCount);
            SaveGameRecord();
        }
        get {return gameRecord.waveCount;}
    }
    public int GainedAllCore
    {
        get{return gainedAllCore;}
    }
#endregion

    public delegate void OnDeadDelegate();
    public delegate void OnValueChangedDelegate(PlayerStatus newStatus);
    public delegate void OnDamagedDelegate(GameObject source);

    public event OnDeadDelegate OnDead;
    public event OnValueChangedDelegate OnValueChanged;
    public event OnDamagedDelegate OnDamaged;

#region ValueChangeEvent

    public event System.Action<int> OnChange_KillCount;
    public event System.Action<int> OnChange_PlayCount;
    public event System.Action<int> OnChange_WaveCount;
    public event System.Action<int> OnChange_ClearCount;
    
#endregion

    void SaveGameRecord() => JSONParser.SaveJSON<GameRecord>($"{Application.streamingAssetsPath}/GameRecord.json", gameRecord);

    private void Update() 
    {
        if(recoverDelay > 0.5f)
        {
            recoverDelay = 0f;
            CurrentShield += ShieldRecovery / 2;
            OnValueChanged.Invoke(this);
        }
        else
        {
            recoverDelay += Time.deltaTime;
        }
    }

    public void Init(StartStatus startStatus)
    {
        gameRecord = JSONParser.ReadJSON<GameRecord>($"{Application.streamingAssetsPath}/GameRecord.json") ?? new GameRecord();

        maxHp = startStatus.maxHp;
        maxShield = startStatus.maxShield;
        currentShield = maxShield;
        currentHp = maxHp;
        shieldRecovery = startStatus.shieldRecovery;
        core = startStatus.startCore;
        coreGainPercent = startStatus.coreGainPercent;
        gainedAllCore = 0;
        
        beforeKillCount = gameRecord.killCount;
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

    public void RecoverAllShield()
    {
        currentShield = maxShield;
        OnValueChanged.Invoke(this);
    }

    public void GainCore(float value)
    {
        int gainedCore = (int)(value * (1+CoreGainPercent));
        Core += gainedCore;
        gainedAllCore += gainedCore;
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
