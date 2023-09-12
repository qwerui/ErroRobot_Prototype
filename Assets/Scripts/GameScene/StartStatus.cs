using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 초기 스테이터스
/// </summary>
[System.Serializable]
public class StartStatus
{
    public float maxHp;
    public float maxShield;
    public int startCore;
    public float coreGainPercent;
    public float shieldRecovery;

    [Range(0, 4)]
    public int towerSlot;

    public static StartStatus Create()
    {
        var newStatus = new StartStatus();
        newStatus.maxHp = 100;
        newStatus.maxShield = 100;
        newStatus.startCore = 0;
        newStatus.coreGainPercent = 0;
        newStatus.shieldRecovery = 0;
        newStatus.towerSlot = 1;
        JSONParser.SaveJSON<StartStatus>($"{Application.streamingAssetsPath}/StartStatus.json", newStatus);
        return newStatus;
    }
}
