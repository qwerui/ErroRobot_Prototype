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

        public float Speed
        {
            set
            {
                speed = value;
                agent.speed = speed;
            }

            get
            {
                return speed;
            }
        }

        public PhaseManager phaseManager;
        [HideInInspector]
        public EnemyDot dot;
        [HideInInspector]
        public PlayerStatus status;

        NavMeshAgent agent;
        protected BehaviorTreeRunner treeRunner;
        public GameObject target;

        public List<CrowdControl> crowdControls = new List<CrowdControl>();

        bool isDead;

        protected virtual void Awake() 
        {
            TryGetComponent<NavMeshAgent>(out agent);
            TryGetComponent<BehaviorTreeRunner>(out treeRunner);
            agent.speed = speed;
            agent.acceleration = speed;
            isDead = false;
        }

        protected virtual void Start()
        {
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

            //상태이상 업데이트
            foreach(CrowdControl cc in crowdControls)
            {
                cc.OnUpdate(this);
            }
        }

        public void Damaged(float damage, CrowdControl cc = null)
        {
            hp -= damage;
            if(hp <= 0 && !isDead)
            {
                OnDead();
            }

            if(cc != null)
            {
                cc.OnStart(this);
            }
        }

        public void OnDead()
        {
            isDead = true;
            phaseManager.UpdateRemainEnemy();
            dot.DestroyDot();
            status.GainCore(core);
            Destroy(gameObject);
        }

        public void DeleteCC(CrowdControl cc)
        {
            crowdControls.Remove(cc);
        }
    }
}

