using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardPanel : MonoBehaviour
{
    public PlayerStatus status;
    public RewardOption[] options = new RewardOption[3];

    public delegate void OnPickRewardDelegate(Reward reward);

    public OnPickRewardDelegate onPickReward;

    public void Init(Reward reward, int index)
    {
        options[index].Init(reward);
    }

    public void PickLeftOption(BaseEventData eventData)
    {
        onPickReward.Invoke(options[0].reward);
    }

    public void PickCenterOption(BaseEventData eventData)
    {
        onPickReward.Invoke(options[1].reward);
    }

    public void PickRightOption(BaseEventData eventData)
    {
        onPickReward.Invoke(options[2].reward);
    }

    private void OnValidate() 
    {
        if(options.Length > 3)
        {
            System.Array.Resize(ref options, 3);
        }    
    }
}
