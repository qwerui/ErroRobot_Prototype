using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponEnhanceReward", menuName = "Infos/Reward/WeaponEnhanceReward", order = 0)]
public class WeaponEnhanceReward : ScriptableObject 
{
    //target weapon
    public EnhanceType enhanceType;
}

public enum EnhanceType
{
    Damage,
    MaxHP,
    Delay
}