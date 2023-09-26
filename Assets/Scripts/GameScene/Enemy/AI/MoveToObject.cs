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
    Animator animator;

    public float acceptanceRadius;

    protected override void OnStart()
    {
        self.TryGetComponent<NavMeshAgent>(out agent);
        target = blackboard.Get<GameObject>("target");
        self.TryGetComponent<Animator>(out animator);
        var collider = target.GetComponent<Collider>();
        if(collider != null)
        {
            destination = collider.ClosestPoint(self.transform.position);
        }
        else
        {
            destination = target.transform.position;
        }
        agent.isStopped = false;
        agent.SetDestination(destination);
        animator?.SetBool("isMoving", true);
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
        
        if(agent.remainingDistance <= acceptanceRadius && agent.pathPending == false)
        {
            agent.isStopped = true;
            animator?.SetBool("isMoving", false);
            
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }
}
