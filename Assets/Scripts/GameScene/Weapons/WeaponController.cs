using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour

{

    // 현재 선택된 무기
    [SerializeField]
    private BaseWeapon currentWeapon;
    public BaseWeapon CurrentWeapon
    {
        set { currentWeapon = value; }
    }

    // 카메라
    [SerializeField]
    private Camera cam;

    // 격발 위치 오브젝트 transform
    public Transform[] fireStartPos;

    // 격발 순서
    private int _fireSeq = 0;

    // 현재 격발 카운트. 0이 되면 격발 가능
    private float currentFireDelay;

    // 장전 상태, true면 현재 장전 중
    private bool isReloading = false;

    // 현재 격발 진행 여부.(True = 버튼이 눌림)
    private bool ButtonPressed = false;



    // 총알 충돌 정보
    private RaycastHit hitInfo;




    // 오디오
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (currentWeapon != null)
        {
            CalculateFireDelay();
            if (ButtonPressed)
            {
                CheckFire();

            }

            checkReload();
        }
    }

    // 총 격발 시간 계산
    private void CalculateFireDelay()
    {
        if (currentFireDelay > 0)
            currentFireDelay -= Time.deltaTime;
    }

    // 격발 버튼 누르는 상태로 전환
    public void PressButton()
    {
        ButtonPressed = true;
    }

    // 격발 버튼 뗀 상태로 전환
    public void ReleaseButton()
    {
        ButtonPressed = false;
    }

    // 격발 버튼 눌렸을 때 처리.
    private void CheckFire()
    {
        if (!isReloading)
        {
            if (currentWeapon.nowBulletCount <= 0)
            {
                StartCoroutine(ReloadCoroutine());
            }
            else if (currentFireDelay <= 0)
            {
                Fire(_fireSeq);
            }
        }
    }


    // 격발 실시!
    private void Fire(int fireSeq)
    {
        currentFireDelay = currentWeapon.fireDelay;
        currentWeapon.nowBulletCount -= 1;


        Vector3[] firePos = new Vector3[2];
        firePos[0] = fireStartPos[0].position;
        firePos[1] = fireStartPos[1].position;
        //Vector3 firePos = cam.transform.position;
        //Vector3 temp = new Vector3(0, 0.5f, 0);

        //Debug.Log("위치 : " + cam.transform.position);
        //Debug.Log("수정 : " + (cam.transform.position - temp));

        // TODO : 총알 발사 (파티클, 사운드)
        // currentWeapon.Shoot(firePos - temp, cam.transform.forward.normalized - temp);
        //currentWeapon.Shoot(firePos - temp, cam.transform.forward.normalized);
        currentWeapon.Shoot(firePos[fireSeq], cam.transform.forward.normalized);

        if (_fireSeq == 0)
            _fireSeq = 1;
        else
            _fireSeq = 0;

        // 총기 반동 코루틴
        /*StopAllCoroutines();
        StartCoroutine(ReboundCoroutine());*/
    }


    // 장전 체크 (R키)
    private void checkReload()
    {
        // R키를 누르고, 장전 중이 아닐 것이며, 현재 총알 수가 최대 장탄 수보다 적을 때
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentWeapon.nowBulletCount < currentWeapon.maxBulletCount)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    // 장전 프로세스
    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        // TODO : 무기 장전 애니매이션 재생

        // TOOD : yield 개념 확실히 잡기
        // 장전 시간 동안 대기
        currentWeapon.OnReload.Invoke();
        yield return new WaitForSeconds(currentWeapon.reloadDelay);

        currentWeapon.nowBulletCount = currentWeapon.maxBulletCount;
        isReloading = false;
        // Debug.Log("장전 완료");
    }


    // 반동 프로세스
    IEnumerator ReboundCoroutine()
    {
        // TODO : 반동 프로세스
        yield return null;
    }


}