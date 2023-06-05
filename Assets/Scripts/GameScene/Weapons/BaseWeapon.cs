using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{

    // ��� ������� �θ� Ŭ������ �˴ϴ�.

    public string name; // �̸�
    public float accuracy; // ��Ȯ��, ���� ���� ��Ȯ��
    public float bulletSpeed; // ź��, ���� ���� ����
    public float fireDelay; // ���� �ӵ�, ���� ���� ����
    public float reloadDelay; // ���� �ð�

    public int damage; // �����
    public int maxBulletCount; // ���� �� �Ѿ� ����
    public int nowBulletCount; // ���� �Ѿ� ����

    public float reboundHorizontalAmount; // �¿�� Ƣ�� �ݵ�
    public float reboundVerticalAmount; // ���� Ƣ�� �ݵ�

    public ParticleSystem fireParticle; // �� ��ƼŬ
    public AudioClip fireSound; // �� �ݹ� �Ҹ�

    public BaseBullet bullet; // �Ѿ� Ŭ����

    // TODO : ���� �� �ִϸ��̼���?


    // �⺻���� �߻� ����. ��κ��� ���Ⱑ �� ������ ���
    // ������ �ִ� ��... �� Ư���� ����� �ִ� ����� �ش� ������ ������� ���� ����
    public void Shoot(Transform firePos)
    {
        GameObject bullet = Instantiate(this.bullet.bulletPrefab, firePos.transform.position, firePos.transform.rotation);
        bullet.GetComponent<BaseBullet>().parent = this;
        bullet.GetComponent<Rigidbody>().AddForce(firePos.transform.forward * bulletSpeed, ForceMode.Impulse);
    }


    // �⺻���� �Ѿ� �ǰ� ����. ��κ��� ���Ⱑ �� ������ ���
    // ����� ���� ���߸� ����Ű�� �Ѿ�... �� Ư���� ����� �ִ� ����� �ش� ������ ������� ���� ����
    public void OnHit(GameObject target)
    {

        // �� ü�� ����
        if(target.tag == "Enemy")
        {
            target.GetComponent<EnemyBase>().Damaged(damage);
        }
    }
}
