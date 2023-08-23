using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public PhaseManager gameplayManager;

    SaveData saveData;

    public void SaveGame()
    {
        saveData ??= new SaveData();

        saveData.SetPlayerStatus(playerStatus);
        saveData.wave = gameplayManager.wave;
        saveData.isLoadable = true;
        JSONParser.SaveJSON<SaveData>($"{Application.persistentDataPath}/SaveData.json", saveData);
    }

    public void LoadGame()
    {
        var saveData = JSONParser.ReadJSON<SaveData>($"{Application.persistentDataPath}/SaveData.json");
        saveData.GetPlayerStatus(playerStatus);
        gameplayManager.wave = saveData.wave;
    }

    public void DeleteSave()
    {
        if(saveData != null)
        {
            saveData.isLoadable = false;
        }
    }
}
