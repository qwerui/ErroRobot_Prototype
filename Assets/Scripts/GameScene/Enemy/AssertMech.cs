using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class AssertMech : EnemyBase
{
    [Header("Blackboard")]
    public EnemyProjectile bullet;
    public float delay;
    public List<Transform> shotPositionList;

    protected override void Start()
    {
        treeRunner.tree.blackboard.Set<EnemyProjectile>("bullet", bullet);
        treeRunner.tree.blackboard.Set<float>("delay", delay);
        treeRunner.tree.blackboard.Set<List<Transform>>("shotPosition", shotPositionList);
        base.Start();
    }

    public void ShootBigCanonA()
    {

    }
}
