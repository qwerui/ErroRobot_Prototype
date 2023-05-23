using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.BehaviorTree
{
    public class LoopNode : DecoratorNode
    {
        public int loopCount = 0;
        int count;

        protected override void OnStart()
        {
            count = 0;
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            if(count < loopCount)
            {
                child.Update();
                count++;
                return State.Running;
            }
            return count == loopCount ? State.Success : State.Running;
        }
    }

}
