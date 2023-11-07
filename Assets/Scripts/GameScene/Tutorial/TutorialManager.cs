using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Enemy;
using OpenCover.Framework.Model;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance = null;
    public static TutorialManager instance
    {
        get
        {
            if(_instance == null)
            {
                return null;
            }
            return _instance;
        }
    }
    
    public GameObject mainPanelBase;
    public GameObject upperPanelBase;
    
    public Text mainDialogueText;
    public Text upperDialogueText;
    public Image imageFrame;
    public Image imageObject;

    public TutorialController tutorialController;

    DialogueContainer[] dialogueContainers; //튜토리얼 대사
    public delegate void OnTutorialEndDelegate();
    public OnTutorialEndDelegate onTutorialEnd; //onEndWave 호출
    public AudioClip clip;

    int index;
    public bool dialogueBlocked = false;
    public bool allowNextPhase = false;

    public EnemyBase tutorialEnemy = null;

    public class Dialogues
    {
        public DialogueContainer[] dialogues;
    }
    
    
    private void Awake()
    {
        _instance = this;
        dialogueContainers = JSONParser.ReadJSON<Dialogues>($"{Application.streamingAssetsPath}/TutorialDialogue.json")?.dialogues;
    }

    private void Start() 
    {
        index = 0;
        Next();
    }
    
    public void Skip()
    {
        allowNextPhase = true;
        SoundQueue.instance.PlaySFX(clip);
        onTutorialEnd.Invoke();
        gameObject.SetActive(false);
    }

    public void CheckEnd()
    {
        if(index >= dialogueContainers.Length-1)
        {
            Skip();
        }
    }

    public void Next()
    {
        dialogueBlocked = false;
        
        SoundQueue.instance.PlaySFX(clip);

        if(index >= dialogueContainers.Length-1)
        {
            Skip();
        }
        else
        {

            while(!CheckCondition(dialogueContainers[index]))
            {
                index++;
            }
            Draw(dialogueContainers[index]);
            WaitUntil(dialogueContainers[index]);
            index++;
        }
    }

    public void Draw(DialogueContainer dialogue)
    {
        DialogueContainer nextDialogue = dialogueContainers[index];

        Text targetText;
        
        // 상단 판넬 사용 시 대화창 위치 변경 및 변경할 Text 오브젝트 지정
        if (dialogue.speaker == "text_panel")
        {
            SwitchPanelToUpper();
            targetText = upperDialogueText;
        }
        else
        {
            SwitchPanelToBase();
            targetText = mainDialogueText;
        }
        
        
        // 독백일 시 이미지 치우고, 텍스트 위치 조정
        if (dialogue.speaker == "self")
        {
            imageFrame.gameObject.SetActive(false);
            RectTransform rectTran = targetText.gameObject.GetComponent<RectTransform>();
            rectTran.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
        }
        else if (dialogue.speaker == "text_panel")
        {
            // Nothing to do
        }
        else
        {
            imageFrame.gameObject.SetActive(true);
            RectTransform rectTran = targetText.gameObject.GetComponent<RectTransform>();
            rectTran.SetLocalPositionAndRotation(new Vector3(50, 0, 0), Quaternion.Euler(0, 0, 0));
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
        }
        
        
        // Text 오브젝트에 글자 적기
        if (targetText == upperDialogueText)
        {
            targetText.text = nextDialogue.text;
        }
        else
        {
            targetText.DOText("", 0);
            targetText.DOText(nextDialogue.text, 1);
        }
        

    }

    private void SwitchPanelToBase()
    {
        mainPanelBase.SetActive(true);
        upperPanelBase.SetActive(false);
    }

    private void SwitchPanelToUpper()
    {
        mainPanelBase.SetActive(false);
        upperPanelBase.SetActive(true);
    }

    public bool CheckCondition(DialogueContainer dialogue)
    {
        if (dialogue.condition == null)
            return true;

        switch (dialogue.condition)
        {
            case "control_keyboard":
                return PlayerController.instance.CheckKeyboardMode();
            case "control_controller":
                return !PlayerController.instance.CheckKeyboardMode();
        }

        return true;
    }

    public void WaitUntil(DialogueContainer dialogue)
    {
        if (dialogue.wait_until == null)
            return;

        IWaitConditionBase conditionBase;

        switch (dialogue.wait_until)
        {
            case "MoveCamera":
                conditionBase = new WaitUntilMoveCamera();
                break;
            case "RobotSpawn":
                conditionBase = new RobotSpawn();
                
                EnemyInfo[] enemyInfoList = Resources.LoadAll<EnemyInfo>("Enemy");
                foreach (EnemyInfo ei in enemyInfoList)
                {
                    if (ei.id == 999)
                    {
                        var spawned = Instantiate<TutorialEnemy>(ei.prefab.GetComponent<TutorialEnemy>(), new Vector3(334.6f, 27.8f, 339.5f), Quaternion.Euler(30f, -125f, 0f));
                        tutorialEnemy = spawned;
                        break;
                    }
                }
                break;
            case "RemoveRobot":
                conditionBase = new WaitUntilRobotRemoved();
                break;
            case "PlaceTurret":
                conditionBase = new WaitUntilTurretPlaced();
                break;
            case "ChangeWeapon":
                conditionBase = new WaitUntilWeaponChange();
                break;
            default:
                conditionBase = null;
                break;
        }
        
        conditionBase?.Init();
        if(conditionBase != null)
        {
            StartCoroutine(conditionBase.ConditionCheck());
        }
        
        dialogueBlocked = true;
    }


}
