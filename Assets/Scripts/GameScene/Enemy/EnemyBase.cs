using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] 
        float speed;
        [SerializeField]
        float hp;

        NavMeshAgent agent;

        protected virtual void Awake() 
        {
            TryGetComponent<NavMeshAgent>(out agent);
            agent.speed = speed;
        }

        protected virtual void Start()
        {
            if(agent.destination != null)
            {
                agent.Move(Vector3.zero);
            }
        }

        public void SetDestination(Vector3 target)
        {
            agent.SetDestination(target);
        }

        public void Damaged()
        {

        }
    }
}

