using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

public class EnemyShoot : ActionNode
{
    EnemyProjectile projectile;
    float delay;
    float remainDelay;
    GameObject target;
    Animator animator;

    List<Transform> shotPositionList;

    protected override void OnStart()
    {
        projectile = blackboard.Get<EnemyProjectile>("bullet");
        delay = blackboard.Get<float>("delay");
        shotPositionList = blackboard.Get<List<Transform>>("shotPosition");
        target = blackboard.Get<GameObject>("target");
        self.TryGetComponent<Animator>(out animator);
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if(projectile == null || shotPositionList.Count == 0)
        {
            return State.Failure;
        }

        if(remainDelay > 0)
        {
            remainDelay -= Time.deltaTime;
            return State.Running;
        }
        
        animator.SetTrigger("isAttacking");
        foreach(Transform shotPosition in shotPositionList)
        {
            var createdProjectile = Instantiate<EnemyProjectile>(projectile, shotPosition.position, Quaternion.identity);
            createdProjectile.Init(target);
        }
        remainDelay = delay;
        return State.Success;
    }
}
