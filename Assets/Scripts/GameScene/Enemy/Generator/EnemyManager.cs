using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] int width;
        [SerializeField] int height;

        public EnemyBase TempEnemy;
        public GameObject targetPostion; //임시 위치

        List<EnemyBase> nextEnemyList;
        PhaseManager phaseManager;

        private void Awake()
        {
            nextEnemyList = new List<EnemyBase>();
        }

        private void Start() 
        {
            phaseManager = GameObject.FindObjectOfType<PhaseManager>();
            phaseManager.OnWaveStart += OnWaveStart;
            phaseManager.OnWaveEnd += OnWaveEnd;

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
            WaitForSeconds delayTime = new WaitForSeconds(Random.Range(0.5f, 1.5f));
            int index = 0;
            Vector3 spawnPosition = new Vector3();
            while (index < nextEnemyList.Count)
            {
                //instantiate
                spawnPosition = transform.position;
                spawnPosition.x = transform.localPosition.x + Random.Range(-0.05f, 0.05f);
                spawnPosition.z = transform.localPosition.z + Random.Range(-0.4f, 0.4f);
                spawnPosition = transform.parent.TransformPoint(spawnPosition);
                var spawned = Instantiate<EnemyBase>(nextEnemyList[index++], spawnPosition, Quaternion.identity);
                spawned.target = targetPostion;
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
            //임시 생성
            for(int i=0;i<10;i++)
            {
                nextEnemyList.Add(TempEnemy);
            }

            phaseManager.remainEnemy += nextEnemyList.Count;
        }

        //생성 범위 표시
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.DrawCube(Vector3.zero, new Vector3(width, (width + height) * 0.5f, height));
        }
    }

}
