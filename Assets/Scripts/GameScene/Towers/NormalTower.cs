using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : AttackTower
{
    private void Awake() 
    {
        AttackTowerInfo = JSONParser.ReadJSON<AttackTowerInfo>($"{Application.streamingAssetsPath}/TowerStatus/AttackTower/NormalTower.json");
    }

    protected override void Fire()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= AttackTowerInfo.delay[level])
        {
            currentTime = 0;

            foreach(GameObject firePosition in firePositions)
            {
                TowerProjectile createdProjectile = Instantiate<TowerProjectile>(projectile, firePosition.transform.position, Quaternion.identity);
                createdProjectile.InitProjectile(target.gameObject, AttackTowerInfo.damage[level]/2.0f);
            }
        }
    }
}
