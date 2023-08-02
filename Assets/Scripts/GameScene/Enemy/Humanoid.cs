using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class Humanoid : EnemyBase
{
    [Header("Blackboard")]
    public float delay;

    protected override void Start()
    {
        treeRunner.tree.blackboard.Set<float>("delay", delay);
        base.Start();
    }

    public void OnAttack()
    {
        //데미지 판정
        Debug.Log("OnAttack");
    }
}
