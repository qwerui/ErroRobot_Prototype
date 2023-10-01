using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponEnhanceReward : Reward
{
    public int targetId;
    public EnhanceType enhanceType;
    public float enhanceValue;
}

public enum EnhanceType
{
    Damage,
    MaxHP,
    Delay
}