using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public TowerSlotList towerSlotList;
    public TowerMoveController towerMoveController;
    public SaveManager saveManager;
    public TextNotifier textNotifier;

    public List<SerializedTower> GetSerializedTowerList()
    {
        List<SerializedTower> serializedTowers = new List<SerializedTower>();

        for(int i=0;i<towerSlotList.GetSlotCount();i++)
        {
            serializedTowers.Add(towerSlotList.GetSerializedTower(i));
        }

        return serializedTowers;
    }

    public void CreateSlot() => towerSlotList.CreateSlot();

    public void CreateSlot(int amount)
    {
        towerSlotList.CreateSlot(amount);
    }

    public void CreateTower(Tower newTower)
    {
        Tower created = Instantiate(newTower);
        bool isHaveBlankSlot = towerSlotList.SetSlot(created);
        created.OnPut += saveManager.SaveGame;

        if(isHaveBlankSlot)
        {
            MoveTower(created);
        }
        else
        {
            textNotifier.Activate("변경할 타워를 선택해주세요");
            towerSlotList.alterTowerCallback += textNotifier.Deactivate;
            towerSlotList.alterTowerCallback += () => MoveTower(created);
            towerSlotList.AlterTower(newTower);
        }
    }

    public void LoadTower(List<SerializedTower> towers)
    {
        for(int i=0;i<towers.Count;i++)
        {
            var mapper = Resources.Load<TowerMapper>($"Reward/Tower/{towers[i].id}");
            var loaded = Instantiate(mapper.tower, towers[i].position, Quaternion.identity);
            loaded.OnPut += saveManager.SaveGame;
            
            for(int level = 0; level < towers[i].level; level++)
            {
                loaded.Upgrade();
            }
            towerSlotList.SetSlot(loaded, i);
        }
    }

    public void MoveTower(Tower targetTower)
    {
        targetTower.ReadyToPut();
        towerMoveController.SetTower(targetTower);
    }
}
