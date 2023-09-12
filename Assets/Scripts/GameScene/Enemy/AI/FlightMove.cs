using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

public class FlightMove : ActionNode
{
    GameObject target;

    float moveSpeed;
    public float acceptanceRadius;

    protected override void OnStart()
    {
        target = blackboard.Get<GameObject>("target");
        moveSpeed = blackboard.Get<float>("moveSpeed");
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if(target == null)
        {
            return State.Failure;
        }
        
        self.transform.LookAt(target.transform);
        Vector3 selfPosition = self.transform.position;
        selfPosition.y = 0;
        Vector3 targetPosition = target.transform.position;
        targetPosition.y = 0;

        if(Vector3.Distance(selfPosition, targetPosition) < acceptanceRadius)
        {
            return State.Success;
        }

        self.transform.Translate(Time.deltaTime * moveSpeed * Vector3.forward);
        return State.Running;
    }
}
