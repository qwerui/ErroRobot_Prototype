using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class Guardian : EnemyBase
{
    [Header("Blackboard")]
    public EnemyProjectile bullet;
    public List<Transform> shotPositionList;
    public float delay;
    public float damage;
    public float towerDetectRadius;

    protected override void Start()
    {
        treeRunner.tree.blackboard.Set<EnemyProjectile>("bullet", bullet);
        treeRunner.tree.blackboard.Set<List<Transform>>("shotPosition", shotPositionList);
        treeRunner.tree.blackboard.Set<float>("delay", delay);
        treeRunner.tree.blackboard.Set<float>("damage", damage/2.0f);
        treeRunner.tree.blackboard.Set<float>("radius", towerDetectRadius);
        base.Start();
    }
}
