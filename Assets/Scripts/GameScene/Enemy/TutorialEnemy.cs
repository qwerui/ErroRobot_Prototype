
using Enemy;
using UnityEngine;

public class TutorialEnemy : EnemyBase
{
    
    protected override void Awake() 
    {
        isDead = false;
    }

    protected override void Start()
    {
        
    }
    protected override void Update() 
    {
        
    }
    
    public override void OnDead()
    {
        isDead = true;
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 2.0f);
        Destroy(gameObject);
    }

}