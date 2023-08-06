using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardOption : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public UnityEngine.UI.Image image;
    public Reward reward;

    public void Init(Reward reward)
    {
        this.reward = reward;
        title.SetText(reward.title);
        description.SetText(reward.description);
        image.sprite = reward.image;
    }
}
