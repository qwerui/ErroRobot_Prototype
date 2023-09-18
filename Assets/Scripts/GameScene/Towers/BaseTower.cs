using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class BaseTower : MonoBehaviour
{

    /*
     * TODO : 포탑의 성질을 재정의할 필요가 있음
     * 타워에 무기가 장착되있는 형식이고, 그 무기를 자동 발사하는 형태라면
     * => 굳이 BaseTower / BaseWeapon 구분 필요 없이 BaseWeapon 하나로 퉁칠 수 있지 않나?
     * 
     * 타워 서칭 메커니즘 :
     * 현재 설정된 타겟이 없으면, 가장 가까운 적을 타겟으로 함
     * 타겟이 죽을 때까지 새로운 타겟 찾지 않음
     */

    [SerializeField] public string towerName; // 이름
    [SerializeField] public float range; // 포탑 사정거리

    [SerializeField] public LayerMask layerMask; // 타겟 지정 레이어 마스크 (적만 지정)

    [SerializeField] public BaseWeapon weapon;


    private float nowFireDelay; // 격발 계산용 현재 격발 후 딜레이(0되면 발사)

    private GameObject nowTarget; // 현재 설정된 타겟의 트랜스폼
    private bool isFindTarget = false; // 타겟 발견 시 True로 됨
    

    void Start()
    {

    }

    void FixedUpdate()
    {
        SearchEnemy();
        // LookTarget();
        Attack();
    }



    private void SearchEnemy()
    {

        if (isFindTarget)
            return;

        Collider[] _target = Physics.OverlapSphere(this.transform.position, range, layerMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Collider target = _target[i];

            if (target.gameObject.tag == "Enemy")
            {
                isFindTarget = true;
                this.nowTarget = target.gameObject;

            }
        }
    }


    /*private void LookTarget()
    {
        if (isFindTarget)
        {
            Vector3 _direction = (nowTarget.transform.position - tf_TopGun.position).normalized;
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            Quaternion _rotation = Quaternion.Lerp(tf_TopGun.rotation, _lookRotation, 0.2f);
            tf_TopGun.rotation = _rotation;
        }
    }*/

    private void Attack()
    {

        nowFireDelay += Time.deltaTime;

        if (nowTarget == null || !nowTarget.activeSelf)
        {
            // 타겟이 없으면 리셋
            nowTarget = null;
            isFindTarget = false;
            return;
        }

        
        if (nowFireDelay >= weapon.fireDelay)
        {
            nowFireDelay = 0;
            Vector3 _direction = (nowTarget.transform.position - this.transform.position).normalized;
            weapon.Shoot(this.transform.position, _direction);

        }
    }

}