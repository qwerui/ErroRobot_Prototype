using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.BehaviorTree
{
    public class SelectorNode : CompositeNode
    {
        int current;

        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop()
        {

        }

        //자식을 순회하며 실패하면 다음 자식을 실행, 성공하면 종료
        protected override State OnUpdate()
        {
            var child = children[current];
            switch (child.Update())
            {
                case State.Running:
                    return State.Running;
                case State.Failure:
                    current++;
                break;
                case State.Success:
                    return State.Success;
            }

            return current == children.Count ? State.Failure : State.Running;
        }
    }
}

