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
    public List<int> enhanceIdList = new(); // 적용된 강화 ID 목록

    public System.Action OnFire; // 발사 이벤트, 무기 슬롯 업데이트용
    public System.Action OnReload; // 장전 이벤트

    // TODO : 적용 시 애니매이션은?


    // 기본적인 발사 로직. 대부분의 무기가 이 로직을 사용
    // 궤적이 휘는 총... 등 특수한 기믹이 있는 무기는 해당 로직을 사용하지 않을 예정
    public virtual void Shoot(Vector3 firePos, Vector3 _direction)
    {
        GameObject bullet = Instantiate(this.bullet.bulletPrefab, firePos, Quaternion.Euler(new Vector3(0, 0, 0)));
        bullet.GetComponent<BaseBullet>().SetParent(this);
        // 명중률 보정
        Vector3 acc_value = new Vector3(Random.Range(-1f, 1f) * accuracy, Random.Range(-1, 1f) * accuracy, 0f);
        bullet.GetComponent<Rigidbody>().AddForce((_direction + acc_value) * bulletSpeed, ForceMode.Impulse);

        if(fireSound != null)
        {
            SoundQueue.instance.PlaySFX(fireSound);
        }
        OnFire?.Invoke();
    }


    // 기본적인 총알 피격 로직. 대부분의 무기가 이 로직을 사용
    // 대미지 없이 폭발만 일으키는 총알... 등 특수한 기믹이 있는 무기는 해당 로직을 사용하지 않을 예정
    public virtual void OnHit(GameObject target)
    {
        // 적 체력 감소
        if(target.CompareTag("Enemy"))
        {
            target.GetComponent<EnemyBase>().Damaged(damage);
        }
    }

    /// <summary>
    /// 강화 메소드, 타입은 WeaponEnhanceReward에 정의되어있음, 각 무기마다 오버라이딩해야함
    /// switch문으로 타입에 따라 원하는 것만 필터링해 강화할 것
    /// </summary>
    /// <param name="type">강화할 종류</param>
    /// <param name="value">강화 값</param>
    public virtual void Enhance(EnhanceType type, float value) {}
}