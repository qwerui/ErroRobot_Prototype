using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class NormalTower : Tower
{
    public TowerProjectile projectile;
    public GameObject head;
    public GameObject[] firePositions;

    float range = 300.0f;
    float currentTime = 0.0f;
    float delay = 1.0f;
    float damage = 20.0f;
    float turnSpeed = 10.0f;

    EnemyBase target;

    protected override void Execute()
    {
        if(target == null)
        {
            foreach(Collider hit in Physics.OverlapSphere(transform.position, range))
            {
                EnemyBase enemyBase = hit.GetComponent<EnemyBase>();
                if(enemyBase != null)
                {
                    target = enemyBase;
                    break;
                }
            }
        }
        else
        {
            Vector3 targetPosition = target.transform.position;
            targetPosition.y = 0;
            Vector3 selfPosition = transform.position;
            selfPosition.y = 0;

            if(Vector3.Distance(targetPosition, selfPosition) > range)
            {
                target = null;
            }
            else
            {
                //포탑 회전
                Vector3 dir = target.transform.position - head.transform.position;
                head.transform.rotation = Quaternion.Lerp(head.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * turnSpeed);
                Fire();
            }
        }
        
    }

    public override void Upgrade()
    {

    }

    void Fire()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= delay)
        {
            currentTime = 0;

            foreach(GameObject firePosition in firePositions)
            {
                TowerProjectile createdProjectile = Instantiate<TowerProjectile>(projectile, firePosition.transform.position, Quaternion.identity);
                createdProjectile.InitProjectile(target.gameObject, damage/2.0f);
            }
        }
    }
}
