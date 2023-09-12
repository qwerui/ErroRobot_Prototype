using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] float radius;

        public GameObject targetPostion; //플레이어 타워

        PhaseManager phaseManager;
        public PlayerStatus status;
        Minimap minimap;

        //다음 웨이브 출현 적 목록
        List<EnemyBase> nextEnemyList = new List<EnemyBase>();
        //json에서 읽어 온 출현 적 목록
        EnemyWave enemyWave;
        //적 프리팹 목록
        Dictionary<int, EnemyBase> enemyPrefabDict = new Dictionary<int, EnemyBase>();

        string jsonPath;

        private void Awake()
        {
            jsonPath = Application.streamingAssetsPath + "/EnemyWave.json";
            enemyWave = JSONParser.ReadJSON<EnemyWave>(jsonPath);
            EnemyInfo[] enemyInfoList = Resources.LoadAll<EnemyInfo>("Enemy");

            foreach(var enemyInfo in enemyInfoList)
            {
                var enemy = enemyInfo.prefab?.GetComponent<EnemyBase>();
                if(enemy != null)
                {
                    enemyPrefabDict[enemyInfo.id] = enemy;
                }
                else
                {
                    //프리팹이 없거나 잘 못된 프리팹이 할당 된 경우
                    Debug.LogWarning($"Not valid prefab!! id: {enemyInfo.id}");
                }
            }
        }

        private void Start() 
        {
            phaseManager = GameObject.FindObjectOfType<PhaseManager>();
            phaseManager.OnWaveStart += OnWaveStart;
            phaseManager.OnWaveEnd += OnWaveEnd;

            //status = GameObject.FindObjectOfType<PlayerStatus>();
            minimap = GameObject.FindObjectOfType<Minimap>();

            //웨이브 종료 시 적 리스트를 초기화하기 때문에 호출
            OnWaveEnd();
        }

        ///<summary>
        ///방어 웨이브 시작 이벤트
        ///</summary>
        public void OnWaveStart()
        {
            StartCoroutine(SpawnEnemy());
        }

        IEnumerator SpawnEnemy()
        {
            WaitForSeconds delayTime = new WaitForSeconds(Random.Range(0.5f, 1.0f));
            int index = 0;
            while (index < nextEnemyList.Count)
            {
                //instantiate
                Vector3 spawnPosition = transform.position;
                float angle = Mathf.Deg2Rad * Random.Range(0, 90);
                spawnPosition.x += Mathf.Sin(angle) * radius;
                spawnPosition.z += Mathf.Cos(angle) * radius;
                var spawned = Instantiate<EnemyBase>(nextEnemyList[index++], spawnPosition, Quaternion.identity);
                spawned.dot = minimap.dotPool.Get();
                spawned.status = status;
                spawned.target = targetPostion;
                spawned.phaseManager = phaseManager;
                yield return delayTime;
            }
        }

        ///<summary>
        ///방어 웨이브 종료 이벤트
        ///</summary>
        ///<remarks>
        ///다음 웨이브 적 리스트 작성 등
        ///</remarks>
        public void OnWaveEnd()
        {
            nextEnemyList.Clear();
            
            var enemyList = enemyWave.GetEnemyLists(phaseManager.wave);
            if(enemyList == null)
            {
                Debug.LogAssertion("Load EnemyList failed!!");
                Debug.Break();
                return;
            }

            foreach(EnemySet enemySet in enemyList)
            {
                EnemyBase enemyPrefab = enemyPrefabDict[enemySet.enemyId];
                
                if(enemyPrefab != null)
                {
                    for(int spawnCounter = 0; spawnCounter < enemySet.count; spawnCounter++)
                    {
                        nextEnemyList.Add(enemyPrefab);
                    }
                }
            }
            phaseManager.remainEnemy += nextEnemyList.Count;
        }

#if UNITY_EDITOR
        //생성 범위 표시
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(transform.position, radius);
        }
#endif
    }
}
