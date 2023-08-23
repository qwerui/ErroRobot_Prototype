using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward", menuName = "Infos/Reward", order = 0)]
public class Reward : ScriptableObject 
{
    public int id;
    public string title;
    public string description;
    public RewardType type;
    public int rarity;
    public Sprite image;
    public bool isPickable;
}

public enum RewardType
{
    Status,
    Weapon,
    Tower
}