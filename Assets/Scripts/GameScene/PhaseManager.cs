using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{

    public bool isDefense = false;
    public int remainEnemy = 0;


    [SerializeField] private GameObject go_defenseUI;

    void Start()
    {
        // 시작은 방어 모드
        isDefense = false;
    }

    void Update()
    {
        /*Debug.Log("현재 상태 : " + isDefense);
        Debug.Log("적 상태 : " + remainEnemy);*/
        if (isDefense)
        {
            if(remainEnemy == 0)
            {
                SetBuild();
                Debug.Log("방어 종료");
            }
        }
        else
        {
            // TODO : 조이스틱으로 조절하게 커서로 옮기기
            if (Input.GetKey(KeyCode.E))
            {
                SetDefense();
                Debug.Log("건설 종료");
            }
        }
    }


    // 웨이브 시작
    void SetDefense()
    {
        GetComponent<EnemyManager>().OnWaveStart();

        // 건설 모드 전용 UI 비활성화
        go_defenseUI.SetActive(false);
    }

    // 건설 모드
    void SetBuild()
    {
        GetComponent<EnemyManager>().OnWaveEnd();

        // 건설 모드 전용 UI 활성화
        go_defenseUI.SetActive(true);

        // TODO : UI 커서 조정
    }
}
