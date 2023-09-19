using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlotList : MonoBehaviour
{
    public TowerSlot towerSlotPrefab;
    List<TowerSlot> towerSlots = new List<TowerSlot>();

    /// <summary>
    /// 지정한 인덱스의 슬롯에 타워를 넣는 메소드
    /// </summary>
    /// <param name="tower">슬롯에 넣을 타워</param>
    /// <param name="index">대상이 되는 슬롯 인덱스</param>
    public void SetSlot(Tower tower, int index)
    {
        if(towerSlots.Count > index)
        {
            towerSlots[index].Tower = tower;
        }
    }

    /// <summary>
    /// 자동으로 마지막 슬롯에 타워를 넣는 메소드
    /// </summary>
    /// <param name="tower">슬롯에 넣을 타워</param>
    public void SetSlot(Tower tower)
    {
        for(int i=0;i<towerSlots.Count; i++)
        {
            if(towerSlots[i].Tower == null)
            {
                towerSlots[i].Tower = tower;
            }
        }
    }

    public void CreateSlot(int amount)
    {
        for(int i = transform.childCount; i < amount; i++)
        {
            towerSlots.Add(Instantiate(towerSlotPrefab, transform));
        }
    }
}
