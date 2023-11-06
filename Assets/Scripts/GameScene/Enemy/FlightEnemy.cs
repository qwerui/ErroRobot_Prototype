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
    public float damage;
    public AudioClip shootClip;

    protected override void Start()
    {
        transform.Translate(Vector3.up * initialHeight);
        treeRunner.tree.blackboard.Set<EnemyProjectile>("bullet", bullet);
        treeRunner.tree.blackboard.Set<float>("delay", delay);
        treeRunner.tree.blackboard.Set<List<Transform>>("shotPosition", shotPositionList);
        treeRunner.tree.blackboard.Set<float>("moveSpeed", speed);
        treeRunner.tree.blackboard.Set<float>("damage", damage/4.0f);
        treeRunner.tree.blackboard.Set<AudioClip>("shootClip", shootClip);
        base.Start();
    }
}
