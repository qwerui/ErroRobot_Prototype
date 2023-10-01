using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rigid;
    public float moveSpeed;
    float damage;

    public void Init(GameObject target, float damage)
    {
        this.damage = damage;
        transform.LookAt(target.transform);
        TryGetComponent<Rigidbody>(out rigid);
        rigid?.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);    
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().Damaged(damage, gameObject);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Tower"))
        {
            other.GetComponent<Tower>().OnDamaged(damage);
            Destroy(gameObject);
        }
    }
}
