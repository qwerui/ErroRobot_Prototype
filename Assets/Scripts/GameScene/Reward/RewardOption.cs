using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

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

        title.rectTransform.anchoredPosition = Vector2.zero;
        titleLength = title.GetPreferredValues(reward.title).x;

        if(titleLength > 150)
        {
            sequence.Kill();
            sequence = DOTween.Sequence();
            sequence
            .AppendInterval(1.0f)
            .Append(title.rectTransform.DOAnchorPosX(150 - titleLength, 2).SetEase(Ease.Linear))
            .AppendInterval(1.0f)
            .SetLoops(-1);
            sequence.Play();
        }
        else
        {
            sequence.Kill();
        }
    }

    public Reward GetReward()
    {
        return reward;
    }

}
