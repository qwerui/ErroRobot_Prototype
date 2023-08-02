using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

public class MeleeAttack : ActionNode
{
    Animator animator;
    float delay;
    float currentDelay;
    GameObject target;

    protected override void OnStart()
    {
        delay = blackboard.Get<float>("delay");
        self.TryGetComponent<Animator>(out animator);
        target = blackboard.Get<GameObject>("target");
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if(currentDelay > 0)
        {
            currentDelay -= Time.deltaTime;
            return State.Running;
        }

        self.transform.LookAt(target.transform);
        animator.SetTrigger("isAttacking");
        currentDelay = delay;
        return State.Success;
    }
}
