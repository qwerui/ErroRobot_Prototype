using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    public Text dialogueText;

    string[] dialogues; //튜토리얼 대사
    public delegate void OnTutorialEndDelegate();
    public OnTutorialEndDelegate onTutorialEnd; //onEndWave 호출

    int index;

    private void Awake() 
    {
        if(PlayerController.instance.CheckKeyboardMode())
        {
            dialogues = JSONParser.ReadJSON<DialogueContainer>($"{Application.streamingAssetsPath}/KeyboardTutorial.json")?.dialogues;
        }
        else
        {
            dialogues = JSONParser.ReadJSON<DialogueContainer>($"{Application.streamingAssetsPath}/ControllerTutorial.json")?.dialogues;
        }
    }

    private void Start() 
    {
        index = 0;
        Next();
    }
    
    public void Skip()
    {
        onTutorialEnd.Invoke();
        gameObject.SetActive(false);
    }

    public void Next()
    {
        if(index > dialogues.Length)
        {
            Skip();
        }
        else
        {
            dialogueText.text = "";
            dialogueText.DOText(dialogues[index++], 3);
        }
    }
}
