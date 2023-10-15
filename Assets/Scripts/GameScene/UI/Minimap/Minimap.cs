using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Minimap : MonoBehaviour
{
    public IObjectPool<EnemyDot> dotPool;
    public EnemyDot enemyDotPrefab;
    public Transform createTarget;

    private void Awake() 
    {
        dotPool = new ObjectPool<EnemyDot>(CreateDot, GetDot, ReleaseDot, DestroyDot, maxSize:20);    
    }

    EnemyDot CreateDot()
    {
        EnemyDot dot = Instantiate<EnemyDot>(enemyDotPrefab, createTarget);
        dot.SetPool(dotPool);
        return dot;
    }

    void GetDot(EnemyDot dot)
    {
        dot.gameObject.SetActive(true);
    }

    void ReleaseDot(EnemyDot dot)
    {
        dot.gameObject.SetActive(false);
    }

    void DestroyDot(EnemyDot dot)
    {
        Destroy(dot.gameObject);
    }
}
