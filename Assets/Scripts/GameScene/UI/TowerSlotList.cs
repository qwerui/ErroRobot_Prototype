using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlotList : MonoBehaviour, IGameUI
{
    public TowerSlot towerSlotPrefab;
    List<TowerSlot> towerSlots = new List<TowerSlot>();
    GameUIController gameUIController;
    Tower alterTowerCache;

    public delegate void AlterTowerCallback();
    public AlterTowerCallback alterTowerCallback;
    int index;

    private void Awake() 
    {
        gameUIController = GetComponent<GameUIController>();
    }

    public int GetSlotCount() => towerSlots.Count;
    public SerializedTower GetSerializedTower(int index) => towerSlots[index].GetSerializedTower();

    /// <summary>
    /// 지정한 인덱스의 슬롯에 타워를 넣는 메소드
    /// </summary>
    /// <param name="tower">슬롯에 넣을 타워</param>
    /// <param name="index">대상이 되는 슬롯 인덱스</param>
    /// <returns>슬롯에 추가 성공</returns>
    public bool SetSlot(Tower tower, int index)
    {
        if(towerSlots.Count > index)
        {
            towerSlots[index].Tower = tower;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 자동으로 마지막 슬롯에 타워를 넣는 메소드
    /// </summary>
    /// <param name="tower">슬롯에 넣을 타워</param>
    /// <returns>슬롯에 추가 성공</returns>
    public bool SetSlot(Tower tower)
    {
        for(int i=0;i<towerSlots.Count; i++)
        {
            if(towerSlots[i].Tower == null)
            {
                towerSlots[i].Tower = tower;
                return true;
            }
        }
        return false;
    }

    public void CreateSlot(int amount)
    {
        for(int i = transform.childCount; i < amount; i++)
        {
            towerSlots.Add(Instantiate(towerSlotPrefab, transform));
        }
    }

#region Alter

    public void AlterTower(Tower tower)
    {
        gameUIController.enabled = true;
        index = 0;
        alterTowerCache = tower;
        towerSlots[index].Activate();
    }

    public void OnNavigate(Vector2 direction)
    {
        if(Mathf.Abs(direction.y) < Mathf.Epsilon)
        {
            return;
        }

        towerSlots[index].Deactivate();
        index += Mathf.Clamp(index, 0, towerSlots.Count);
        towerSlots[index].Activate();
    }

    public void OnSubmit()
    {
        towerSlots[index].AlterTower(alterTowerCache);
        alterTowerCache = null;
        gameUIController.enabled = false;
        alterTowerCallback.Invoke();
        alterTowerCallback = null;
    }

    public void OnCancel()
    {
        //타워를 골랐을 경우 반드시 교체해야한다.
    }
#endregion
}
