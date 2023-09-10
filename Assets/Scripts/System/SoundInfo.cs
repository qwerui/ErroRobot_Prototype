using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundInfo", menuName = "Infos/SoundInfo", order = 0)]
public class SoundInfo : ScriptableObject 
{
    public int id;
    public AudioClip clip;
}