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
        // ������ ��� ���
        isDefense = false;
    }

    void Update()
    {
        /*Debug.Log("���� ���� : " + isDefense);
        Debug.Log("�� ���� : " + remainEnemy);*/
        if (isDefense)
        {
            if(remainEnemy == 0)
            {
                SetBuild();
                Debug.Log("��� ����");
            }
        }
        else
        {
            // TODO : ���̽�ƽ���� �����ϰ� Ŀ���� �ű��
            if (Input.GetKey(KeyCode.E))
            {
                SetDefense();
                Debug.Log("�Ǽ� ����");
            }
        }
    }


    // ���̺� ����
    void SetDefense()
    {
        GetComponent<EnemyManager>().OnWaveStart();

        // �Ǽ� ��� ���� UI ��Ȱ��ȭ
        go_defenseUI.SetActive(false);
    }

    // �Ǽ� ���
    void SetBuild()
    {
        GetComponent<EnemyManager>().OnWaveEnd();

        // �Ǽ� ��� ���� UI Ȱ��ȭ
        go_defenseUI.SetActive(true);

        // TODO : UI Ŀ�� ����
    }
}
