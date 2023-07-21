using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Achievement
{
    public int id;
    public string title;
    [Multiline]
    public string description;
    public string reward;
    [Multiline]
    public string story;
    public string imagePath;
    [System.NonSerialized]
    public Sprite image; //instanceID로 저장되기 때문에 NonSerialized로 변경
    public AchievementEvent eventType;

    [HideInInspector]
    public bool isAchieved = false;

    //각 업적 달성 여부 체크
    public virtual bool Check(int value) { return false; }
    public virtual bool Check(float value) { return false; }
    public virtual bool Check(System.Enum value) { return false; }
}

public enum AchievementEvent
{
    PlayCount
}

[System.Serializable]
public class AchievementList
{
    public List<Achievement> achievements = new List<Achievement>();
}
