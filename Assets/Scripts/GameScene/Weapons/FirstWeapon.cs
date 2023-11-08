using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWeapon : BaseWeapon
{
    public override void Enhance(EnhanceType type, float value)
    {
        switch(type)
        {
            case EnhanceType.Damage:
                damage += (int)value;
            break;
            case EnhanceType.MaxBullet:
                maxBulletCount += (int)value;
            break;
            case EnhanceType.ReloadDelay:
                reloadDelay -= value;
            break;
        }
    }
}
