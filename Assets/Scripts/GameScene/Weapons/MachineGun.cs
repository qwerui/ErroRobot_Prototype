using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : BaseWeapon
{
    public override void Enhance(EnhanceType type, float value)
    {
        switch(type)
        {
            case EnhanceType.Damage:
            damage += (int)value;
            break;
            case EnhanceType.MaxBullet:
            damage += (int)value;
            break;
            case EnhanceType.ReloadDelay:
            reloadDelay -= value;
            break;
            case EnhanceType.Accuracy:
            accuracy -= value;
            break;
        }
    }

}
