using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    public int id;
    public string title;
    [Multiline]
    public string description;
    public RewardType type;
    [Range(0, 4)]
    public int rarity;
    [NonSerialized]
    public Sprite image;
    public string imagePath;
    public int pickableCount;
    public int currentPickable;
    public bool isUnlocked;
}

public enum RewardType
{
    Status,
    Weapon,
    Tower,
    Enhance
}