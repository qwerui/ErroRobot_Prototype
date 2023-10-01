using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponMapper", menuName = "Infos/Rewards/WeaponMapper", order = 0)]
public class WeaponMapper : ScriptableObject 
{
    public int id;
    public Sprite icon;
    public BaseWeapon weapon;
}