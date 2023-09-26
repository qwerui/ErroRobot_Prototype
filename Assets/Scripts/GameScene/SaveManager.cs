using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public PhaseManager gameplayManager;
    public TowerManager towerManager;
    public WeaponManager weaponManager;

    SaveData saveData;

    public void SaveGame()
    {
        saveData ??= new SaveData();

        saveData.SetPlayerStatus(playerStatus);
        saveData.wave = gameplayManager.wave;
        saveData.towerSlots = towerManager.towerSlotList.GetSlotCount();
        saveData.towers = towerManager.GetSerializedTowerList();
        saveData.isLoadable = true;
        saveData.weaponSlots = weaponManager.GetSlotCount();
        saveData.weapons = weaponManager.GetSerializedWeapons();
        JSONParser.SaveJSON<SaveData>($"{Application.persistentDataPath}/SaveData.json", saveData);
    }

    public void LoadGame()
    {
        var saveData = JSONParser.ReadJSON<SaveData>($"{Application.persistentDataPath}/SaveData.json");
        saveData.GetPlayerStatus(playerStatus);
        gameplayManager.wave = saveData.wave;
        towerManager.CreateSlot(saveData.towerSlots);
        towerManager.LoadTower(saveData.towers);
        weaponManager.CreateSlot(saveData.weaponSlots);
        weaponManager.LoadWeapon(saveData.weapons);
    }

    public void DeleteSave()
    {
        if(saveData != null)
        {
            saveData.isLoadable = false;
        }
    }
}
