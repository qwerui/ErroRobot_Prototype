using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

/// <summary>
/// 상태이상 클래스 적 클래스의 OnDamage에서 damage와 함께 넘긴다
/// 오버라이딩 가능 메소드(OnStart, OnUpdate, OnEnd)
/// </summary>
public abstract class CrowdControl
{
    protected float consistenceTime;
    float currentTime = 0;

    public virtual void OnStart(EnemyBase enemy)
    {
        enemy.crowdControls.Add(this);
    }
    public virtual void OnUpdate(EnemyBase enemy)
    {
        if(currentTime >= consistenceTime)
        {
            OnEnd(enemy);
        }
        currentTime += Time.deltaTime;
    }
    public virtual void OnEnd(EnemyBase enemy)
    {
        enemy.DeleteCC(this);
    }
}
