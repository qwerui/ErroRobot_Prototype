using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : AttackTower
{
    private void Awake() 
    {
        AttackTowerInfo = JSONParser.ReadJSON<AttackTowerInfo>($"{Application.streamingAssetsPath}/TowerStatus/AttackTower/SlowTower.json");
    }

    protected override void Fire()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= AttackTowerInfo.delay[Level])
        {
            currentTime = 0;

            var created = Instantiate<TowerProjectile>(projectile, firePositions[0].transform.position, Quaternion.identity);
            created.InitProjectile(target.gameObject, AttackTowerInfo.damage[Level], new SlowCC(2.0f, 0.3f));
        }
    }
}
