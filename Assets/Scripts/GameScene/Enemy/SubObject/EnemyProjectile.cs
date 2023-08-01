using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rigid;
    public float moveSpeed;

    public void Init(GameObject target)
    {
        transform.LookAt(target.transform);
        TryGetComponent<Rigidbody>(out rigid);
        rigid?.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);    
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("데미지 구현 필요");
            Destroy(gameObject);
        }
    }
}
