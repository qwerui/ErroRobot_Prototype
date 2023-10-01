using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    Rigidbody rigid;
    public GameObject particle;
    float damage;
    public float speed;
    float instancedTime;
    const float destoryTime = 10.0f;
    CrowdControl crowdControl;

    private void Awake() 
    {
        rigid = GetComponent<Rigidbody>();  
        instancedTime = 0;  
    }

    private void Update() 
    {
        instancedTime += Time.deltaTime;
        if(instancedTime >= destoryTime)
        {
            Destroy(gameObject);
        }
    }

    public void InitProjectile(GameObject target, float newDamage, CrowdControl crowdControl = null)
    {
        Vector3 dir = target.transform.position - transform.position;
        damage = newDamage;
        this.crowdControl = crowdControl;
        rigid.AddForce(dir * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) 
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if(enemy != null)
        {
            enemy.Damaged(damage, crowdControl);
            //폭발 이펙트가 2초
            Destroy(Instantiate(particle, transform.position, Quaternion.identity), 2.0f);
            Destroy(gameObject);
        }
    }
}
