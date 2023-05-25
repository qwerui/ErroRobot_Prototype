using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    public abstract class Node : ScriptableObject 
    {
        ///<summary>
        ///노드의 작업 상태
        ///</summary>
        public enum State
        {
            Running,
            Failure,
            Success
        }

        [HideInInspector] public State state = State.Running;
        [HideInInspector] public bool started = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 postion;
        [HideInInspector] public Blackboard blackboard;
        [HideInInspector] public GameObject self;
        [TextArea] public string description;

        public State Update()
        {
            if(!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if(state == State.Failure || state == State.Success)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }
        
        ///<summary>
        ///노드 시작 시 작업
        ///</summary>
        protected abstract void OnStart();

        ///<summary>
        ///노드 종료 시 작업
        ///</summary>
        protected abstract void OnStop();

        ///<summary>
        ///주 노드 작업
        ///</summary>
        protected abstract State OnUpdate();
    }
}
