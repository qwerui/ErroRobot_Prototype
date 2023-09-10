using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public BaseWeapon parent;

    public GameObject bulletPrefab;

    // rigidBody �浹 ��
    private void OnTriggerEnter(Collider other)
    {
        parent.OnHit(other.gameObject);
        Destroy(gameObject);
    }

}
