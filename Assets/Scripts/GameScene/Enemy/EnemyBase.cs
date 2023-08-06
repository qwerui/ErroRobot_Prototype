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
        protected float speed;
        [SerializeField]
        protected float hp;
        [SerializeField]
        protected float armor;
        [SerializeField]
        protected float core;

        PhaseManager phaseManager;
        [HideInInspector]
        public EnemyDot dot;

        NavMeshAgent agent;
        protected BehaviorTreeRunner treeRunner;
        public GameObject target;

        protected virtual void Awake() 
        {
            TryGetComponent<NavMeshAgent>(out agent);
            TryGetComponent<BehaviorTreeRunner>(out treeRunner);
            agent.speed = speed;
            agent.acceleration = speed;
        }

        protected virtual void Start()
        {
            phaseManager = GameObject.FindObjectOfType<PhaseManager>();
            PlayerStatus status = GameObject.FindObjectOfType<PlayerStatus>();
            Minimap minimap = GameObject.FindObjectOfType<Minimap>();
            dot = minimap.dotPool.Get();
            treeRunner.tree.blackboard.Set<GameObject>("target", target);
            treeRunner.tree.blackboard.Set<PlayerStatus>("status", status);
            treeRunner.RunTree();
        }

        protected virtual void Update() 
        {
            //미니맵 업데이트
            Vector2 dotPosition = Vector2.zero;
            dotPosition.x = (transform.position.x - 150)/800*150;
            dotPosition.y = (transform.position.z - 150)/800*150;
            dot.SetPosition(dotPosition);
        }

        public void Damaged(float damage)
        {
            hp -= damage;
            if(hp <= 0)
            {
                OnDead();
            }
        }

        public void OnDead()
        {
            phaseManager.UpdateRemainEnemy();
            dot.DestroyDot();
            Destroy(gameObject);
        }
    }
}

