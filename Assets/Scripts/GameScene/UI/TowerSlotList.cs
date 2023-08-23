using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlotList : MonoBehaviour
{
    public TowerSlot towerSlotPrefab;

    public void LoadSlot()
    {
        //타워 로드
    }

    public void CreateSlot(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(towerSlotPrefab, transform);
        }
    }
}
