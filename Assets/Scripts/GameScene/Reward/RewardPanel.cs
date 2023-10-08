using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : MonoBehaviour, IGameUI
{
    GameUIController gameUIController;
    public RewardManager rewardManager;
    public RewardOption[] rewardOptions = new RewardOption[3];
    public AudioClip enableClip;

    int index;

    private void OnEnable() 
    {
        if(gameUIController == null)
        {
            gameUIController = GetComponent<GameUIController>();
        }

        SoundQueue.instance.PlaySFX(enableClip);
        gameUIController.enabled = true;
        index = 0;
        rewardOptions[index].Activate();
    }

    private void OnDisable() 
    {
        foreach(RewardOption ro in rewardOptions)
        {
            ro.Deactivate();
        }
        gameUIController.enabled = false;
    }

    public void SetRewardOption(int index, Reward reward)
    {
        rewardOptions[index].Init(reward);
    }

    public void OnCancel()
    {
        //보상은 반드시 획득 = 아무 코드 없음
    }

    public void OnNavigate(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) < Mathf.Epsilon)
        {
            return;
        }

        rewardOptions[index].Deactivate();
        index += direction.x > 0 ? 1 : -1;
        index = Mathf.Clamp(index, 0, 2);
        rewardOptions[index].Activate();
    }

    public void OnSubmit()
    {
        rewardManager.GetReward(rewardOptions[index].GetReward());
    }

    public void OnSubmit_Mouse(int index)
    {
        rewardManager.GetReward(rewardOptions[index].GetReward());
    }
}
