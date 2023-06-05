using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class BaseTower : MonoBehaviour
{

    /*
     * TODO : 포탑의 성질을 재정의할 필요가 있음
     * 무기 자동발사라면 => 굳이 BaseTower / BaseWeapon 구분 필요 없이 BaseWeapon 하나로 퉁칠 수 있지 않나?
     */

    [SerializeField] public string towerName; // 이름
    [SerializeField] public float range; // 포탑 사정거리

    [SerializeField] public int damage; // 대미지
    [SerializeField] public float accuracy; // 정확도
    [SerializeField] public float fireDelay; // 연사속도

    [SerializeField] public LayerMask layerMask; // 타겟 지정 레이어 마스크 (적만 지정)


    private float nowFireDelay; // 격발 계산용 현재 격발 후 딜레이(0되면 발사)
    private RaycastHit hitInfo; // 맞은 대상 정보

    private bool isFindTarget = false; // 타겟 발견 시 True로 됨
    private bool isAttack = false; // 공격 중...

    [SerializeField] private Transform tf_TopGun; // 포신
    private Transform tf_Target; // 현재 설정된 타겟의 트랜스폼

    [SerializeField] private float viewAngle; // 시야각

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        SearchEnemy();
        LookTarget();
        Attack();
    }



    private void SearchEnemy()
    {
        Collider[] _target = Physics.OverlapSphere(tf_TopGun.position, range, layerMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Collider target = _target[i];

            if (target.gameObject.tag == "Enemy")
            {
                Vector3 _direction = (target.transform.position - tf_TopGun.position).normalized;
                float _angle = Vector3.Angle(_direction, tf_TopGun.forward);

                // TODO : transform에 설정된 시야각과 이게 무슨 연관인지 알아볼것
                if (_angle < viewAngle * 0.5f)
                {
                    tf_Target = target.transform;
                    isFindTarget = true;

                    if (_angle < 5f) // 각도 차이 안나면
                        isAttack = true; // 공격 시작
                    else
                        isAttack = false;

                    return;
                }
            }
        }
        
        // 적 못찾으면 리셋
        tf_Target = null;
        isAttack = false;
        isFindTarget = false;
    }


    private void LookTarget()
    {
        if (isFindTarget)
        {
            Vector3 _direction = (tf_Target.position - tf_TopGun.position).normalized;
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            Quaternion _rotation = Quaternion.Lerp(tf_TopGun.rotation, _lookRotation, 0.2f);
            tf_TopGun.rotation = _rotation;
        }
    }

    private void Attack()
    {
        if (isAttack)
        {
            nowFireDelay += Time.deltaTime;
            if (nowFireDelay >= fireDelay)
            {
                nowFireDelay = 0;

                if (Physics.Raycast(tf_TopGun.position,
                                    tf_TopGun.forward + new Vector3(Random.Range(-1, 1f) * accuracy, Random.Range(-1, 1f) * accuracy, 0f),
                                    out hitInfo,
                                    range,
                                    layerMask))
                {

                    if (hitInfo.transform.tag == "Enemy")
                    {
                        Debug.Log("맞음! " + hitInfo.transform.name);
                        hitInfo.transform.GetComponent<EnemyBase>().Damaged(damage);
                    }
                }
            }
        }
    }

}
