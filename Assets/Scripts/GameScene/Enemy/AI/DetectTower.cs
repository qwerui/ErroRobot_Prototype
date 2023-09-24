using System.Collections;
using System.Collections.Generic;
using AI.BehaviorTree;
using UnityEngine;

public class DetectTower : DecoratorNode
{
    float radius;
    GameObject player;
    GameObject tower;
    Collider[] colliders = new Collider[5];

    protected override void OnStart()
    {
        if(player == null)
        {
            player = blackboard.Get<GameObject>("target");
        }
        radius = blackboard.Get<float>("radius");
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        return tower != null ? child.Update() : State.Failure;
    }

    public override bool CheckAbort()
    {
        if(tower != null)
        {
            Vector3 distanceVector = tower.transform.position - self.transform.position;
            distanceVector.y = 0;

            if(Vector3.Magnitude(distanceVector) > radius)
            {
                tower = null;
                blackboard.Set<GameObject>("target", player);
                return true;
            }
            else
            {
                return false;
            }
        }

        blackboard.Set<GameObject>("target", player);

        Physics.OverlapSphereNonAlloc(self.transform.position, radius, colliders);

        foreach (Collider hit in colliders)
        {
            if (hit != null)
            {
                if (hit.CompareTag("Tower"))
                {
                    tower = hit.gameObject;
                    blackboard.Set<GameObject>("target", tower);
                    break;
                }
            }
        }

        return tower != null;
    }
}
