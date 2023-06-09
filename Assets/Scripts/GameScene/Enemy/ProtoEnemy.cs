using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ProtoEnemy : EnemyBase
    {
        protected override void Start()
        {
            base.Start();
            PlayerStatus status = GameObject.FindObjectOfType<PlayerStatus>();
            treeRunner.tree.blackboard.Set<GameObject>("target", target);
            treeRunner.tree.blackboard.Set<PlayerStatus>("status", status);
            treeRunner.RunTree();
        }
    }
}

