using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private BaseWeapon parent;

    public GameObject bulletPrefab;

    public void SetParent(BaseWeapon parent)
    {
        this.parent = parent;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet"))
        {
            parent.OnHit(other.gameObject);
            Destroy(gameObject);
        }
        
    }

    

}
