using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{

    // 모든 무기들의 부모 클래스가 됩니다.

    public string name; // 이름
    public float accuracy; // 정확도, 낮을 수록 정확함
    public float bulletSpeed; // 탄속, 높을 수록 빠름
    public float fireDelay; // 연사 속도, 낮을 수록 빠름
    public float reloadDelay; // 장전 시간

    public int damage; // 대미지
    public int maxBulletCount; // 장전 시 총알 개수
    public int nowBulletCount; // 현재 총알 개수

    public float reboundHorizontalAmount; // 좌우로 튀는 반동
    public float reboundVerticalAmount; // 위로 튀는 반동

    public ParticleSystem fireParticle; // 총 파티클
    public AudioClip fireSound; // 총 격발 소리

    public BaseBullet bullet; // 총알 클래스

    // TODO : 적용 시 애니매이션은?


    // 기본적인 발사 로직. 대부분의 무기가 이 로직을 사용
    // 궤적이 휘는 총... 등 특수한 기믹이 있는 무기는 해당 로직을 사용하지 않을 예정
    public void Shoot(Vector3 firePos, Vector3 _direction)
    {
        
        GameObject bullet = Instantiate(this.bullet.bulletPrefab, firePos, Quaternion.Euler(new Vector3(0, 0, 0)));
        bullet.GetComponent<BaseBullet>().parent = this;
        // 명중률 보정
        Vector3 acc_value = new Vector3(Random.Range(-1f, 1f) * accuracy, Random.Range(-1, 1f) * accuracy, 0f);
        bullet.GetComponent<Rigidbody>().AddForce((_direction + acc_value) * bulletSpeed, ForceMode.Impulse);
    }


    // 기본적인 총알 피격 로직. 대부분의 무기가 이 로직을 사용
    // 대미지 없이 폭발만 일으키는 총알... 등 특수한 기믹이 있는 무기는 해당 로직을 사용하지 않을 예정
    public void OnHit(GameObject target)
    {

        // 적 체력 감소
        if(target.tag == "Enemy")
        {
            target.GetComponent<EnemyBase>().Damaged(damage);
        }
    }
}