using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardManager : MonoBehaviour
{
    Dictionary<int, List<Reward>> rewards = new Dictionary<int, List<Reward>>();

    const int MAXRAREITY = 5;
    public int[] rarityWeight;
    int allRaritySum;

    //보상 획득 창
    public GameObject rewardPanel;
    public PhaseManager gameplayManager;
    public RewardOption[] rewardOptions = new RewardOption[3];

    public SaveManager saveManager;

    private void Awake() 
    {
        for(int i=0;i<5;i++)
        {
            rewards[i] = new List<Reward>();
            allRaritySum += rarityWeight[i];
        }
        var loadedReward = Resources.LoadAll<Reward>("Reward");
        
        foreach(Reward reward in loadedReward)
        {
            if(reward.isPickable)
            {
                rewards[reward.rarity].Add(reward);
            }
        }

        gameplayManager.OnWaveEnd += InitReward;
    }

    public void InitReward()
    {
        //가중치에 따라 선택
        for(int i=0;i<3;i++) //3개 선택지
        {
            int pickedRarity = Random.Range(0, allRaritySum);
            int raritySum = 0;

            for(int rarity = 0; rarity < rarityWeight.Length; rarity++)
            {
                raritySum += rarityWeight[rarity];

                if(pickedRarity <= raritySum && rewards[rarity].Count > 0)
                {
                    int pickedRewardIndex = Random.Range(0, rewards[rarity].Count);
                    rewardOptions[i].Init(rewards[rarity][pickedRewardIndex]);
                    break; //다음 선택지로
                }
            }
        }

        //보상 획득창 활성화
        rewardPanel.gameObject.SetActive(true);
    }

    public void PickLeftOption(BaseEventData eventData)
    {
        GetReward(rewardOptions[0].reward);
    }

    public void PickCenterOption(BaseEventData eventData)
    {
        GetReward(rewardOptions[1].reward);
    }

    public void PickRightOption(BaseEventData eventData)
    {
        GetReward(rewardOptions[2].reward);
    }

    void GetReward(Reward reward)
    {
        //보상 적용 코드
        rewardPanel.SetActive(false);

        //보상 결정 시 세이브
        saveManager.SaveGame();
    }

    private void OnValidate() 
    {
        if(rarityWeight.Length > MAXRAREITY)
        {
            //최대 레어도에 의한 길이 한계
            System.Array.Resize(ref rarityWeight, MAXRAREITY);
        }
        
        if(rewardOptions.Length != 3)
        {
            //3선택지에 의한 길이 고정
            rewardOptions = new RewardOption[3];
        }
    }
}
