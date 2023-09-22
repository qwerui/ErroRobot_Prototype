using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusReward : Reward
{
    public StatusType statusType;
    public float value;
}

public enum StatusType
{
    MaxHP,
    MaxShield,
    CoreGain,
    ShieldRecover,
    TowerSlot
}