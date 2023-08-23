using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField]
    string playerStatusJson;
    public int wave;
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
