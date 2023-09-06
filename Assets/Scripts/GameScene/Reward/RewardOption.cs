using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardOption : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public UnityEngine.UI.Image image;
    Reward reward;
    Outline outline;

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
    }

    public Reward GetReward()
    {
        return reward;
    }

}
