using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerReward : Reward
{
    public int towerId;
    [NonSerialized]
    public Tower towerPrefab;
}
