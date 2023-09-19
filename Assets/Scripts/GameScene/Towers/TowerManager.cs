using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public TowerSlotList towerSlotList;
    public TowerMoveController towerMoveController;

    private void Start() 
    {
        
    }

    public void CreateTower(Tower newTower)
    {
        MoveTower(Instantiate(newTower));
    }

    public void MoveTower(Tower targetTower)
    {
        targetTower.ReadyToPut();
        towerMoveController.SetTower(targetTower);
    }
}
