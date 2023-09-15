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

}
