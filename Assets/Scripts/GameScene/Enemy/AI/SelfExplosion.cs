using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

public class SelfExplosion : ActionNode
{
    PlayerStatus status;

    protected override void OnStart()
    {
        if(status == null)
        {
            status = blackboard.Get<PlayerStatus>("status");
        }    
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        status?.Damaged(50.0f, self);
        self.GetComponent<Enemy.EnemyBase>().OnDead();
        return State.Success;
    }
}
