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
    public AudioClip clip;

    int index;

    private void Awake() 
    {
        dialogues = JSONParser.ReadJSON<DialogueContainer>($"{Application.streamingAssetsPath}/KeyboardTutorial.json")?.dialogues;
    }

    private void Start() 
    {
        index = 0;
        Next();
    }
    
    public void Skip()
    {
        SoundQueue.instance.PlaySFX(clip);
        onTutorialEnd.Invoke();
        gameObject.SetActive(false);
    }

    public void Next()
    {
        SoundQueue.instance.PlaySFX(clip);

        if(index >= dialogues.Length)
        {
            Skip();
        }
        else
        {
            dialogueText.DOText("", 0);
            dialogueText.DOText(dialogues[index++], 3);
        }
    }
}
