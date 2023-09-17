using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : ScriptableObject 
{
    public int id;
    public string title;
    [Multiline]
    public string description;
    public RewardType type;
    [Range(0, 4)]
    public int rarity;
    public Sprite image;
    public int pickableCount;
}

public enum RewardType
{
    Status,
    Weapon,
    Tower,
    Enhance
}