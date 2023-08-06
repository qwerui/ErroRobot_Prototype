using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    Dictionary<int, List<Reward>> rewards = new Dictionary<int, List<Reward>>();

    const int MAXRAREITY = 5;
    public int[] rarityWeight;
    int allRaritySum;

    public RewardPanel panel;

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
            rewards[reward.rarity].Add(reward);
        }

        panel.onPickReward = GetReward;
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

                if(pickedRarity <= raritySum)
                {
                    int pickedRewardIndex = Random.Range(0, rewards[rarity].Count);
                    panel.Init(rewards[rarity][pickedRewardIndex], i);
                    break; //다음 선택지로
                }
            }
            
        }
    }

    void GetReward(Reward reward)
    {
        //보상 적용 코드

        panel.gameObject.SetActive(false);
    }

    private void OnValidate() 
    {
        if(rarityWeight.Length > MAXRAREITY)
        {
            //최대 레어도에 의한 길이 한계
            System.Array.Resize(ref rarityWeight, MAXRAREITY);
        }
    }
}
