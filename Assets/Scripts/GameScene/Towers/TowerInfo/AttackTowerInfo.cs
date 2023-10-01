using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackTowerInfo : TowerInfo
{
    public float[] range;
    public float[] delay;
    public float[] damage;

    public override Dictionary<string, string> GetTowerInfoString(int level)
    {
        var result = base.GetTowerInfoString(level);
        result["Damage"] = damage[level].ToString();
        result["Delay"] = delay[level].ToString();
        result["Range"] = range[level].ToString();
        return result;
    }
}
