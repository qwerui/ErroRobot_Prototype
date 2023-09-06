using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

//추후에 GameplayManager로 명칭 변경 예정
public class PhaseManager : MonoBehaviour
{
    public int wave = 1;

    public bool isDefense = false;
    public int remainEnemy = 0;

    public delegate void WaveStartDelegate();
    public delegate void WaveEndDelegate();
    public delegate void GameEndDelegate();

    public event WaveStartDelegate OnWaveStart;
    public event WaveEndDelegate OnWaveEnd;
    public event GameEndDelegate OnGameEnd;

    [SerializeField] private GameObject go_defenseUI;
    [Header("Controller")]
    public BuildController buildController;
    public DefenceController defenceController;
    public GameoverController gameoverController;

    public UIManager UI;
    public PlayerStatus playerStatus;
    public SaveManager saveManager;

    private void Awake() 
    {
        //컨트롤러 이벤트 초기화
        OnWaveStart += () => buildController.gameObject.SetActive(false);
        OnWaveStart += () => defenceController.gameObject.SetActive(true);
        OnWaveEnd += () => buildController.gameObject.SetActive(true);
        OnWaveEnd += () => defenceController.gameObject.SetActive(false);

        //게임 종료 이벤트 초기화
        playerStatus.onDead += Gameover;
        playerStatus.onDead += defenceController.cameraController.DisableRotation;
        OnGameEnd += GameClear;
        OnGameEnd += defenceController.cameraController.DisableRotation;
    }

    void Start()
    {
        //게임 로드 체크
        if(GameManager.instance.isLoadedGame)
        {
            saveManager.LoadGame();
        }
        else
        {
            //새 게임
            var startStatus = JSONParser.ReadJSON<StartStatus>($"{Application.streamingAssetsPath}/StartStatus.json") ?? StartStatus.Create();
            playerStatus.Init(startStatus);
            UI.towerSlotList.CreateSlot(startStatus.towerSlot);
        }

        //튜토리얼 체크
        if(PlayerPrefs.GetInt("IsFirst", 0) == 0) //0 : 첫 시작, 1 : 두 번째 게임 이후
        {
            var tutorial = Resources.Load<TutorialManager>("System/Tutorial");
            tutorial = Instantiate<TutorialManager>(tutorial);
            tutorial.onTutorialEnd = () => OnWaveEnd.Invoke();
            PlayerPrefs.SetInt("IsFirst", 1);
        }
        else
        {
            OnWaveEnd.Invoke();
        }

        // 시작은 건설 모드
        isDefense = false;
    }

    public void Gameover()
    {
        Time.timeScale = 0.0f;
        gameoverController.gameObject.SetActive(true);
        UI.OnGameover();
    }

    public void GameClear()
    {
        //임시 코드 추후에 게임 클리어 화면 출력
        Time.timeScale = 0.0f;
        gameoverController.gameObject.SetActive(true);
        UI.OnGameover();
    }

    public void UpdateRemainEnemy()
    {
        remainEnemy--;
        if(remainEnemy <= 0)
        {
            if(wave > 5)
            {
                OnGameEnd.Invoke();
            }
            else
            {
                wave++;
                OnWaveEnd.Invoke();
            }
        }
    }

    public void InvokeNextWave()
    {
        OnWaveStart.Invoke();
    }

    // void Update()
    // {
    //     /*Debug.Log("현재 상태 : " + isDefense);
    //     Debug.Log("적 상태 : " + remainEnemy);*/
    //     if (isDefense)
    //     {
    //         if(remainEnemy == 0)
    //         {
    //             SetBuild();
    //             Debug.Log("방어 종료");
    //         }
    //     }
    //     else
    //     {
    //         // TODO : 조이스틱으로 조절하게 커서로 옮기기
    //         if (Input.GetKey(KeyCode.E))
    //         {
    //             SetDefense();
    //             Debug.Log("건설 종료");
    //         }
    //     }
    // }


    // 웨이브 시작
    void SetDefense()
    {
        //GetComponent<EnemyManager>().OnWaveStart();

        // 건설 모드 전용 UI 비활성화
        //go_defenseUI.SetActive(false);
    }

    // 건1설 모드
    void SetBuild()
    {
        //GetComponent<EnemyManager>().OnWaveEnd();

        // 건설 모드 전용 UI 활성화
        //go_defenseUI.SetActive(true);

        // TODO : UI 커서 조정
    }
}