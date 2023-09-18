using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class Bazooka : BaseWeapon
{

    [SerializeField] private float ExplodeDistance;
    
    public override void OnHit(GameObject target)
    {
        // 뭐든 일단 닿으면 폭발
        Collider[] colls = Physics.OverlapSphere(transform.position, ExplodeDistance);
        foreach(Collider c in colls)
        {
            if(c.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, c.transform.position);
                float damageMultiplier = distance / ExplodeDistance;
                c.GetComponent<EnemyBase>().Damaged(damage * damageMultiplier);
            }
        }
        
    }
}
