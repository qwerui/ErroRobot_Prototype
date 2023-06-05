using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class BaseTower : MonoBehaviour
{

    /*
     * TODO : ��ž�� ������ �������� �ʿ䰡 ����
     * ���� �ڵ��߻��� => ���� BaseTower / BaseWeapon ���� �ʿ� ���� BaseWeapon �ϳ��� ��ĥ �� ���� �ʳ�?
     */

    [SerializeField] public string towerName; // �̸�
    [SerializeField] public float range; // ��ž �����Ÿ�

    [SerializeField] public int damage; // �����
    [SerializeField] public float accuracy; // ��Ȯ��
    [SerializeField] public float fireDelay; // ����ӵ�

    [SerializeField] public LayerMask layerMask; // Ÿ�� ���� ���̾� ����ũ (���� ����)


    private float nowFireDelay; // �ݹ� ���� ���� �ݹ� �� ������(0�Ǹ� �߻�)
    private RaycastHit hitInfo; // ���� ��� ����

    private bool isFindTarget = false; // Ÿ�� �߰� �� True�� ��
    private bool isAttack = false; // ���� ��...

    [SerializeField] private Transform tf_TopGun; // ����
    private Transform tf_Target; // ���� ������ Ÿ���� Ʈ������

    [SerializeField] private float viewAngle; // �þ߰�

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

                // TODO : transform�� ������ �þ߰��� �̰� ���� �������� �˾ƺ���
                if (_angle < viewAngle * 0.5f)
                {
                    tf_Target = target.transform;
                    isFindTarget = true;

                    if (_angle < 5f) // ���� ���� �ȳ���
                        isAttack = true; // ���� ����
                    else
                        isAttack = false;

                    return;
                }
            }
        }
        
        // �� ��ã���� ����
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
                        Debug.Log("����! " + hitInfo.transform.name);
                        hitInfo.transform.GetComponent<EnemyBase>().Damaged(damage);
                    }
                }
            }
        }
    }

}
