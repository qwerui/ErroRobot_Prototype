using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;
using UnityEngine.AI;

public class MoveToObject : ActionNode
{
    NavMeshAgent agent;
    GameObject target;

    public float acceptanceRadius;

    protected override void OnStart()
    {
        self.TryGetComponent<NavMeshAgent>(out agent);
        target = blackboard.Get<GameObject>("target");
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if(agent == null || target == null)
        {
            return State.Failure;
        }
        
        agent.SetDestination(target.transform.position);
        agent.Move(Vector3.zero);
        
        if(agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }
}
