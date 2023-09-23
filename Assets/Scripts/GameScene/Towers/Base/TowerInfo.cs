using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo
{
    public int id;
    public float[] maxHp;
    public int[] upgradeCore;
    public int maxLevel;
    
    /// <summary>
    /// 타워의 정보를 담은 딕셔너리를 반환한다.
    /// 타워의 정보를 출력할 객체가 5개가 한계기 때문에 5개를 넘는 정보를 담지 않도록 한다.
    /// </summary>
    /// <param name="level">타워 정보를 얻을 레벨</param>
    /// <returns>매핑된 타워 정보 텍스트 딕셔너리</returns>
    public virtual Dictionary<string, string> GetTowerInfoString(int level)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        result["MaxHP"] = maxHp[level].ToString();
        return result;
    }
}
