using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class AttackTower : Tower
{
    public TowerProjectile projectile;
    public GameObject head;
    public GameObject[] firePositions;

    protected AttackTowerInfo AttackTowerInfo
    {
        set {towerInfo = value;}
        get {return towerInfo as AttackTowerInfo;}
    }

    protected float currentTime = 0.0f;
    protected const float turnSpeed = 10.0f;

    protected EnemyBase target;

    protected override void Execute()
    {
        if(target == null)
        {
            foreach(Collider hit in Physics.OverlapSphere(transform.position, AttackTowerInfo.range[level]))
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

            if(Vector3.Distance(targetPosition, selfPosition) > AttackTowerInfo.range[level])
            {
                target = null;
            }
            else
            {
                if(head != null)
                {
                    //포탑의 상부만 움직이는 경우
                    TurnHead();
                }
                Fire();
            }
        }
        
    }

    protected void TurnHead()
    {
        //포탑 회전
        Vector3 dir = target.transform.position - head.transform.position;
        head.transform.rotation = Quaternion.Lerp(head.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * turnSpeed);
    }

    protected virtual void Fire(){}
}
