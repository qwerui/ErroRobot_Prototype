using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTowerInfo : TowerInfo
{
    public float[] recoverAmount;

    public override Dictionary<string, string> GetTowerInfoString(int level)
    {
        var result = base.GetTowerInfoString(level);
        result["Recover Amount"] = recoverAmount[level].ToString();
        return result;
    }
}
