using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI.BehaviorTree;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] 
        float speed;
        [SerializeField]
        float hp;
        [SerializeField]
        float armor;

        GameObject phaseManager;

        NavMeshAgent agent;
        protected BehaviorTreeRunner treeRunner;
        public GameObject target;

        protected virtual void Awake() 
        {
            TryGetComponent<NavMeshAgent>(out agent);
            TryGetComponent<BehaviorTreeRunner>(out treeRunner);
            agent.speed = speed;

            phaseManager = GameObject.Find("PhaseManager");
            phaseManager.GetComponent<PhaseManager>().remainEnemy += 1;
        }

        protected virtual void Start()
        {

        }

        public void Damaged(float damage)
        {
            hp -= damage;
            if(hp <= 0)
            {
                OnDead();
            }
        }

        void OnDead()
        {
            Destroy(gameObject);
            phaseManager.GetComponent<PhaseManager>().remainEnemy -= 1;
        }
    }
}

