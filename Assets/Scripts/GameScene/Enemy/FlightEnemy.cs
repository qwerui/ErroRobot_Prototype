using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class FlightEnemy : EnemyBase
{
    [Header("Blackboard")]
    public float initialHeight;
    public float delay;
    public List<Transform> shotPositionList;
    public EnemyProjectile bullet;

    protected override void Start()
    {
        transform.Translate(Vector3.up * initialHeight);
        treeRunner.tree.blackboard.Set<EnemyProjectile>("bullet", bullet);
        treeRunner.tree.blackboard.Set<float>("delay", delay);
        treeRunner.tree.blackboard.Set<List<Transform>>("shotPosition", shotPositionList);
        treeRunner.tree.blackboard.Set<float>("moveSpeed", speed);
        base.Start();
    }
}