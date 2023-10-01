using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField]
    string playerStatusJson;
    public int wave;
    public int towerSlots;
    public List<SerializedTower> towers;
    public int weaponSlots;
    public List<SerializedWeapon> weapons;
    
    public bool isLoadable;
    public void SetPlayerStatus(PlayerStatus playerStatus)
    {
        playerStatusJson = JsonUtility.ToJson(playerStatus);
    }
    public void GetPlayerStatus(PlayerStatus playerStatus)
    {
        JsonUtility.FromJsonOverwrite(playerStatusJson, playerStatus);
    }
}

[System.Serializable]
public class SerializedTower
{
    public int id;
    public int level;
    public Vector3 position;
}

[System.Serializable]
public class SerializedWeapon
{
    public int id;
    public List<int> enhance = new List<int>();
}
