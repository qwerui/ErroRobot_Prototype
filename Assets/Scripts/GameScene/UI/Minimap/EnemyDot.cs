using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//미니맵의 적 위치를 표시할 점
public class EnemyDot : MonoBehaviour
{
    IObjectPool<EnemyDot> dotPool;
    RectTransform rect;

    private void Awake() 
    {
        TryGetComponent<RectTransform>(out rect);
    }

    public void SetPosition(Vector2 position)
    {
        rect.anchoredPosition = position;
    }

    public void SetPool(IObjectPool<EnemyDot> objectPool)
    {
        dotPool = objectPool;
    }

    public void DestroyDot()
    {
        dotPool.Release(this);
    }
}
