using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour

{

    // ���� ���õ� ����
    [SerializeField]
    private BaseWeapon currentWeapon;

    // ī�޶�
    [SerializeField]
    private Camera cam;

    // �ݹ� ��ġ
    [SerializeField]
    private Transform firePos;

    // ���� �ݹ� ī��Ʈ. 0�� �Ǹ� �ݹ� ����
    private float currentFireDelay;

    // ���� ����, true�� ���� ���� ��
    private bool isReloading = false;



    // �Ѿ� �浹 ����
    private RaycastHit hitInfo;

    


    // �����
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        calculateFireDelay();
        checkFire();
        checkReload();
    }

    // �� �ݹ� �ð� ���
    private void calculateFireDelay()
    {
        if (currentFireDelay > 0)
            currentFireDelay -= Time.deltaTime;
    }

    // �ݹ� ��ư ������ �� ó��.
    private void checkFire()
    {
        if(Input.GetKey(KeyCode.Q) || Input.GetMouseButton(0))
        {

            if (!isReloading)
            {
                if(currentWeapon.nowBulletCount <= 0)
                {
                    StartCoroutine(ReloadCoroutine());
                } 
                else if(currentFireDelay <= 0)
                {
                    Fire();
                }
            }
        }
    }


    // �ݹ� �ǽ�!
    public void Fire()
    {

        Debug.Log("�ݹ�!");

        currentFireDelay = currentWeapon.fireDelay;
        currentWeapon.nowBulletCount -= 1;

        // TODO : �Ѿ� �߻� (��ƼŬ, ����)
        currentWeapon.Shoot(cam.transform);

        // �ѱ� �ݵ� �ڷ�ƾ
        /*StopAllCoroutines();
        StartCoroutine(ReboundCoroutine());*/
    }


    // ���� üũ (RŰ)
    private void checkReload()
    {
        // RŰ�� ������, ���� ���� �ƴ� ���̸�, ���� �Ѿ� ���� �ִ� ��ź ������ ���� ��
        if(Input.GetKeyDown(KeyCode.R) && !isReloading && currentWeapon.nowBulletCount < currentWeapon.maxBulletCount)
        {
            Debug.Log("���� ����...");
            StartCoroutine(ReloadCoroutine());
        }
    }

    // ���� ���μ���
    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        // TODO : ���� ���� �ִϸ��̼� ���

        // TOOD : yield ���� Ȯ���� ���
        // ���� �ð� ���� ���
        yield return new WaitForSeconds(currentWeapon.reloadDelay);

        currentWeapon.nowBulletCount = currentWeapon.maxBulletCount;
        isReloading = false;
        Debug.Log("���� �Ϸ�");
    }


    // �ݵ� ���μ���
    IEnumerator ReboundCoroutine()
    {
        // TODO : �ݵ� ���μ���
        yield return null;
    }


}
