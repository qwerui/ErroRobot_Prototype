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
    public RewardPanel rewardPanel;
    public PhaseManager gameplayManager;
    public PlayerStatus playerStatus;
    

    public SaveManager saveManager;

    private void Awake() 
    {
        for(int i=0;i<5;i++)
        {
            rewards[i] = new List<Reward>();
            allRaritySum += rarityWeight[i];
        }
        // var loadedReward = Resources.LoadAll<Reward>("Reward");
        
        // foreach(Reward reward in loadedReward)
        // {
        //     rewards[reward.rarity].Add(reward);
        // }
    }

    private void Start() 
    {
        /*
        BuildController 활성화보다 늦게 호출해야함
        BuildController는 Awake에서 할당 => (PhaseManager 클래스 참조)
        */
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
                    rewardPanel.SetRewardOption(i, rewards[rarity][pickedRewardIndex]);
                    break; //다음 선택지로
                }
            }
        }

        //보상 획득창 활성화
        rewardPanel.gameObject.SetActive(true);
    }

    public void GetReward(Reward reward)
    {
        rewardPanel.gameObject.SetActive(false);

        //보상 적용 코드
        // switch(reward.type)
        // {
        //     case RewardType.Status:
        //         var statusReward = reward as StatusReward;
        //         playerStatus.Enhance(statusReward.statusType, statusReward.value);
        //     break;
        //     case RewardType.Weapon:
        //     break;
        //     case RewardType.Tower:
        //     break;
        //     default:
        //         Debug.LogWarning("Invalid Reward");
        //     break;
        // }

        // reward.pickableCount--;

        // if(reward.pickableCount <= 0)
        // {
        //     rewards[reward.rarity].Remove(reward);
        // }

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
    }
}
