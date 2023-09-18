using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusReward", menuName = "Infos/Reward/StatusReward", order = 0)]
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
    ShieldRecover
}