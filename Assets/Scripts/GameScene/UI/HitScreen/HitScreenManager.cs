using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 피격 시 피격 방향을 보여주는 이펙트 출력
/// </summary>
public class HitScreenManager : MonoBehaviour
{
    Camera mainCamera;
    HitScreen[] hitScreens;

    void Start()
    {
        mainCamera = Camera.main;
        hitScreens = transform.GetComponentsInChildren<HitScreen>();
        System.Array.Sort(hitScreens, (HitScreen a, HitScreen b)=>{
            return a.transform.GetSiblingIndex() - b.transform.GetSiblingIndex();
        });
    }

    public void ShowHitScreen(GameObject source)
    {
        Vector3 playerForward = mainCamera.transform.forward;
        Vector3 sourceVector = source.transform.position - mainCamera.transform.position;
        Vector3 v = sourceVector - playerForward;

        float angle = Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;

        if((angle > 0 ? angle : -angle) <= 45)
        {
            //정면
            hitScreens[1].Show();
        }
        else if(angle > 0)
        {
            //좌측
            hitScreens[0].Show();
        }
        else
        {
            //우측
            hitScreens[2].Show();
        }

    }
}
