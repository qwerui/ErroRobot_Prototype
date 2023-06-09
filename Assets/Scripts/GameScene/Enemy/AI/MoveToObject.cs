using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;
using UnityEngine.AI;

public class MoveToObject : ActionNode
{
    NavMeshAgent agent;
    GameObject target;
    Vector3 destination;

    public float acceptanceRadius;

    protected override void OnStart()
    {
        self.TryGetComponent<NavMeshAgent>(out agent);
        target = blackboard.Get<GameObject>("target");
        destination = target.transform.localPosition;
        destination.z += Random.Range(-200f, 200f);
        destination = target.transform.parent.TransformPoint(destination);
        agent.SetDestination(destination);
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

        agent.Move(Vector3.zero);
        
        if(agent.remainingDistance <= acceptanceRadius)
        {
            agent.isStopped = true;
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }
}
