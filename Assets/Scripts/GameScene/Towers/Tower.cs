using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

/*
    예상 필요 변수
    HP
    공격력/회복량 등 - 메인 효과 수치
    공격속도/효과 주기
    사거리
*/
public class Tower : MonoBehaviour, IRaycastInteractable
{
    BoxCollider boxCollider;

    readonly Color halfTransparent = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    readonly Color opaque = Color.white;
    readonly Color notAvailable = new Color(1.0f, 0.0f, 0.0f, 0.5f);

    bool isBuildPhase;
    bool isMoving;

    IEnumerator towerLoop;
    PhaseManager gameplayManager;
    TowerUI towerUI;

    Vector3 beforeMovePostion;

    void StartLoop() => StartCoroutine(towerLoop);
    void StopLoop()
    {
        if(towerLoop != null)
        {
            StopCoroutine(towerLoop);
        }
    } 

    private void Start() 
    {
        gameplayManager = GameObject.FindObjectOfType<PhaseManager>();
        towerUI = GameObject.FindObjectOfType<TowerUI>(true);
        towerLoop = ActivateTowerLoop();
        
        gameplayManager.OnWaveStart += StartLoop;
        gameplayManager.OnWaveStart += () => isBuildPhase = false;
        gameplayManager.OnWaveEnd += StopLoop;
        gameplayManager.OnWaveEnd += () => isBuildPhase = true;
    }

    /// <summary>
    /// 설치 준비
    /// </summary>
    public void ReadyToPut()
    {
        boxCollider ??= GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        isMoving = true;
        beforeMovePostion = transform.position;

        //반투명화
        foreach(var mesh in GetComponentsInChildren<MeshRenderer>())
        {
            var material = mesh.material;
            material = Instantiate(material);
            if(material.shader.renderQueue == 3000) //Transparent = 3000
            {
                material.SetColor("_Color", halfTransparent);
            }
            mesh.material = material;
        }
    }

    /// <summary>
    /// 설치 준비 시 위치 이동
    /// </summary>
    /// <param name="target">목적지</param>
    public void Move(Vector3 target)
    {
        transform.SetPositionAndRotation(target, Quaternion.identity);
    }

    /// <summary>
    /// 이동 취소
    /// </summary>
    public void RevertMove()
    {
        transform.position = beforeMovePostion;
    }

    /// <summary>
    /// 타워 설치
    /// </summary>
    public void Put()
    {
        boxCollider.isTrigger = false;
        isMoving = false;

        //불투명화
        foreach(var mesh in GetComponentsInChildren<MeshRenderer>())
        {
            var material = mesh.material;
            material.SetColor("_Color", opaque);
        }
    }

    /// <summary>
    /// 방어 페이즈때만 실행하는 루프
    /// </summary>
    IEnumerator ActivateTowerLoop()
    {
        while(true)
        {
            Execute();
            yield return null;
        }
    }

    private void OnDestroy() 
    {
        gameplayManager.OnWaveStart -= StartLoop;
        gameplayManager.OnWaveEnd -= StopLoop;
    }
    
    /// <summary>
    /// 방어 페이즈에 실행할 동작
    /// </summary>
    protected virtual void Execute(){}
    public virtual void Upgrade(){}

    void IRaycastInteractable.Execute()
    {
        if(isBuildPhase)
        {
            if(isMoving)
            {
                Put();
            }
            else
            {
                towerUI.SetTower(this);
                towerUI.gameObject.SetActive(true);
            }
        }
    }

    /*
    타워는 이동 중일 때만 콜리전이 트리거 모드가 된다.
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tower>() != null)
        {
            //타워가 겹칠 시 붉게 변함
            foreach (var mesh in GetComponentsInChildren<MeshRenderer>())
            {
                var material = mesh.material;
                material.SetColor("_Color", notAvailable);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.GetComponent<Tower>() != null)
        {
            //타워가 겹치지 않으면 원상복귀
            foreach (var mesh in GetComponentsInChildren<MeshRenderer>())
            {
                var material = mesh.material;
                material.SetColor("_Color", halfTransparent);
            }
        }
    }
}
