using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpFixTower : FixTower
{
    PlayerStatus playerStatus;

    private void Awake() 
    {
        FixTowerInfo = JSONParser.ReadJSON<FixTowerInfo>($"{Application.streamingAssetsPath}/TowerStatus/FixTower/HpFixTower.json");
    }

    protected override void Start()
    {
        base.Start();
        gameplayManager.OnWaveEnd += FixHp;
        playerStatus = GameObject.FindFirstObjectByType<PlayerStatus>();
    }

    protected override void OnDestroy() 
    {
        base.OnDestroy();
        gameplayManager.OnWaveEnd -= FixHp;
    }

    void FixHp()
    {
        playerStatus.CurrentHp += FixTowerInfo.recoverAmount[Level];
    }
}
