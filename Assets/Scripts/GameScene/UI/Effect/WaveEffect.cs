using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WaveEffect : MonoBehaviour
{
    public Image upper;
    public Image lower;
    public Image background;
    public TMP_Text text;
    public AudioClip clip;

    Sequence waveStartSeq;
    Sequence waveClearSeq;

    private void Awake() 
    {
        waveStartSeq = DOTween.Sequence();
        waveClearSeq = DOTween.Sequence();

        waveStartSeq.SetAutoKill(false);
        waveClearSeq.SetAutoKill(false);

        waveStartSeq
        .OnStart(()=>{
            text.SetText("WAVE START");
            text.color = new Color(1, 0, 0.2f, 0);
            upper.color = new Color(1, 0, 0.2f, 1);
            lower.color = new Color(1, 0, 0.2f, 1);
            upper.rectTransform.anchoredPosition = Vector2.zero;
            lower.rectTransform.anchoredPosition = Vector2.zero;
        })
        .Append(upper.rectTransform.DOAnchorPosY(50, 1.0f))
        .Join(lower.rectTransform.DOAnchorPosY(-50, 1.0f))
        .Join(DOTween.To(()=>background.rectTransform.sizeDelta.y, x => background.rectTransform.sizeDelta= new Vector2(background.rectTransform.sizeDelta.x, x),100, 1.0f))
        .Append(text.DOFade(1, 0.5f))
        .Append(text.DOFade(0, 0.25f))
        .Append(upper.rectTransform.DOAnchorPosY(0, 0.5f))
        .Join(lower.rectTransform.DOAnchorPosY(0, 0.5f))
        .Join(DOTween.To(()=>background.rectTransform.sizeDelta.y, x => background.rectTransform.sizeDelta= new Vector2(background.rectTransform.sizeDelta.x, x), 0, 0.5f))
        .OnComplete(()=>{
            upper.color = Color.clear;
            lower.color = Color.clear;
        });

        // waveClearSeq
        // .OnStart(()=>{
        //     text.SetText("WAVE CLEAR");
        //     text.color = new Color(0, 0.4f, 1, 0);
        //     upper.color = new Color(0, 0.4f, 1, 1);
        //     lower.color = new Color(0, 0.4f, 1, 1);
        // })
        // .Append(upper.rectTransform.DOAnchorPosY(50, 1.0f))
        // .Join(lower.rectTransform.DOAnchorPosY(-50, 1.0f))
        // .Join(DOTween.To(()=>background.rectTransform.sizeDelta.y, x => background.rectTransform.sizeDelta= new Vector2(background.rectTransform.sizeDelta.x, x), 100, 1.0f))
        // .Append(text.DOFade(1, 0.5f))
        // .Append(text.DOFade(0, 0.25f))
        // .Append(upper.rectTransform.DOAnchorPosY(0, 0.5f))
        // .Join(lower.rectTransform.DOAnchorPosY(0, 0.5f))
        // .Join(DOTween.To(()=>background.rectTransform.sizeDelta.y, x => background.rectTransform.sizeDelta= new Vector2(background.rectTransform.sizeDelta.x, x), 0, 0.5f))
        // .OnComplete(()=>{
        //     upper.color = Color.clear;
        //     lower.color = Color.clear;
        // });

    }

    public void WaveStartEffect()
    {
        waveStartSeq.Play();
        SoundQueue.instance.PlaySFX(clip);
    }
    // public void WaveClearEffect() => waveClearSeq.Play();
}
