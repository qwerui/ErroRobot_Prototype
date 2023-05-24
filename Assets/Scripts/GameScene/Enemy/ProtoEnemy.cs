using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ProtoEnemy : EnemyBase
    {
        public GameObject target;

        protected override void Start()
        {
            base.Start();
            treeRunner.tree.blackboard.Set<GameObject>("target", target);
            treeRunner.RunTree();
        }
    }
}

