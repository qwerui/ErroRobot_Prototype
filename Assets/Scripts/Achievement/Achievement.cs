using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Achievement
{
    public int id;
    public string title;
    public string description;
    public string reward;
    public string story;
    public string imagePath;
    [System.NonSerialized]
    public Sprite image; //instanceID로 저장되기 때문에 NonSerialized로 변경
    public AchievementEvent eventType;
    public AchievementRewardType rewardType;
    public float rewardValue;
    public StatusType statusType;
    public float requireValue;

    public bool isAchieved = false;
}

public enum AchievementEvent
{
    PlayCount,
    KillCount,
    ClearCount,
    WaveCount
}

[System.Serializable]
public class AchievementList
{
    public List<Achievement> achievements = new List<Achievement>();
}

public enum AchievementRewardType
{
    Enhance,
    Unlock
}