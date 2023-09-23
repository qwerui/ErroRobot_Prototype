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

    bool isMaterialInstanced;
    bool isBuildPhase;
    bool isMoving;
    bool isCanPut;

#region TowerInfo

    protected Dictionary<string, string> towerString = new Dictionary<string, string>();

    /// <summary>
    /// 타워 정보 변수, 자식 클래스에서는 다운 캐스팅 프로퍼티 필요
    /// </summary>
    protected TowerInfo towerInfo;
    protected int level = 0;
    public int Level {get{return level;}}
    public float MaxHp {get{return towerInfo.maxHp[level];}}
    protected float currentHp;
    public float CurrentHp {set {currentHp = Mathf.Clamp(value, 0, MaxHp);} get {return currentHp;}}
    public int UpgradeCore {get{return towerInfo.upgradeCore[level];}}

    public Sprite icon;
    public ParticleSystem upgradeEffect;

#endregion

    IEnumerator towerLoop;
    protected PhaseManager gameplayManager;
    TowerUI towerUI;

    Vector3 beforeMovePostion;

    public delegate void OnValueChangeDelegate();
    public OnValueChangeDelegate OnValueChange;
    public delegate void OnPutDelegate();
    public OnPutDelegate OnPut;

    public bool IsMaxLevel() => level == towerInfo.maxLevel - 1;

    public virtual Dictionary<string, string> GetTowerDetail(int level)
    {
        return towerInfo.GetTowerInfoString(level);
    }

    protected virtual void Start() 
    {
        gameplayManager = GameObject.FindObjectOfType<PhaseManager>();
        towerUI = GameObject.FindObjectOfType<TowerUI>(true);
        towerLoop = ActivateTowerLoop();
        isBuildPhase = true;
        
        gameplayManager.OnWaveStart += StartLoop;
        gameplayManager.OnWaveStart += () => isBuildPhase = false;
        gameplayManager.OnWaveEnd += StopLoop;
        gameplayManager.OnWaveEnd += () => isBuildPhase = true;
    }

    protected virtual void OnDestroy() 
    {
        gameplayManager.OnWaveStart -= StartLoop;
        gameplayManager.OnWaveEnd -= StopLoop;
    }

#region Actions

    void StartLoop() => StartCoroutine(towerLoop);
    void StopLoop()
    {
        if(towerLoop != null)
        {
            StopCoroutine(towerLoop);
        }
    }
    
    /// <summary>
    /// 방어 페이즈에 실행할 동작
    /// </summary>
    protected virtual void Execute(){}

    public virtual void Upgrade()
    {
        level++;
        upgradeEffect.Play();
    }

    void OnDamaged(float damage)
    {
        CurrentHp -= damage;
        OnValueChange.Invoke();

        if(CurrentHp <= 0)
        {
            Destroy(gameObject);
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

    void IRaycastInteractable.Execute()
    {
        if(isBuildPhase)
        {
            if(isMoving)
            {
                if(isCanPut)
                {
                    Put();
                }
            }
            else
            {
                towerUI.SetTower(this);
                towerUI.gameObject.SetActive(true);
            }
        }
    }
#endregion

#region SetTower

    /// <summary>
    /// 설치 준비
    /// </summary>
    public void ReadyToPut()
    {
        boxCollider ??= GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        isMoving = true;
        beforeMovePostion = transform.position;
        isCanPut = true;

        //반투명화
        foreach(var mesh in GetComponentsInChildren<MeshRenderer>())
        {
            var material = mesh.material;

            if(!isMaterialInstanced)
            {
                material = Instantiate(material);
            }
            
            if(material.shader.renderQueue == 3000) //Transparent = 3000
            {
                material.SetColor("_Color", halfTransparent);
            }
            mesh.material = material;
        }

        isMaterialInstanced = true;
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
        Put();
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

        OnPut?.Invoke();
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
            isCanPut = false;
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
            isCanPut = true;
        }
    }
#endregion
}
