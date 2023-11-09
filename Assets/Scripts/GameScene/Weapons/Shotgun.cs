using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseWeapon
{
    [SerializeField] private int BulletAmount;
    
    public override void Shoot(Vector3 firePos, Vector3 direction)
    {
        for (int i = 0; i < BulletAmount; i++)
        {
            base.Shoot(firePos, direction);
        }
    }

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
            case EnhanceType.AddtionalBullet:
            BulletAmount += 1;
            break;
        }
    }
}
