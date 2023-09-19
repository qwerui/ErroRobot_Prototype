using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class RewardOption : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public Image image;
    Reward reward;
    Outline outline;

    float titleLength;

    Sequence sequence; 

    public void Activate() => outline.enabled = true;
    public void Deactivate() => outline.enabled = false;

    private void Awake() 
    {
        sequence = DOTween.Sequence();
        sequence.OnStart(()=>{
            title.rectTransform.anchoredPosition = Vector2.zero;
        })
        .AppendInterval(1.0f)
        .Append(title.rectTransform.DOAnchorPosX(150 - titleLength, 2).SetEase(Ease.Linear))
        .AppendInterval(1.0f)
        .SetLoops(-1);

        //최초 실행 시 의도되지 않은 움직임 방지
        if(titleLength > 150)
        {
            sequence.Play();
        }
        else
        {
            sequence.Pause();
            title.rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void OnEnable() 
    {
        if(outline == null)
        {
            outline = GetComponent<Outline>();
        }    
    }

    public void Init(Reward reward)
    {
        this.reward = reward;
        title.SetText(reward.title);
        description.SetText(reward.description);
        image.sprite = reward.image;

        titleLength = title.GetPreferredValues(reward.title).x;

        if(titleLength > 150)
        {
            sequence.Play();
        }
        else
        {
            sequence.Pause();
            title.rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public Reward GetReward()
    {
        return reward;
    }

}
