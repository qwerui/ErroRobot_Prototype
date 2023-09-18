using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private BaseWeapon parent;
    private float age = 0;

    public GameObject bulletPrefab;


    public void Update()
    {
        age += Time.timeScale;
        if (age >= 1000)
        {
            Destroy(gameObject);
        }
    }

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
