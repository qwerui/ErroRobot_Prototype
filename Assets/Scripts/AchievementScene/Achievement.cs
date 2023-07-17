using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Infos/Achievement", order = 0)]
public class Achievement : ScriptableObject 
{
    public int id;
    public string title;
    [Multiline]
    public string description;
    public string reward;
    [Multiline]
    public string story;
    public Sprite image;
}
