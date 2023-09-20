using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardManager : MonoBehaviour
{
    Dictionary<int, List<Reward>> rewards = new Dictionary<int, List<Reward>>();
    Dictionary<int, List<WeaponEnhanceReward>> enhanceCache = new Dictionary<int, List<WeaponEnhanceReward>>();

    const int MAXRAREITY = 5;
    public int[] rarityWeight;
    int allRaritySum;

    //보상 획득 창
    public RewardPanel rewardPanel;
    public PhaseManager gameplayManager;
    public PlayerStatus playerStatus;
    
    public SaveManager saveManager;

    [Header("Tower")]
    public TowerManager towerManager;

    private void Awake() 
    {
        for(int i=0;i<5;i++)
        {
            rewards[i] = new List<Reward>();
            allRaritySum += rarityWeight[i];
        }

        Dictionary<string, Sprite> iconCache = new Dictionary<string, Sprite>();

        foreach(string filePath in Directory.GetFiles($"{Application.streamingAssetsPath}/Rewards", "*json"))
        {
            string rewardJson = JSONParser.ReadJSONString(filePath);
            Reward reward = JsonUtility.FromJson<Reward>(rewardJson);
            switch(reward.type)
            {
                case RewardType.Status:
                    reward = JsonUtility.FromJson<StatusReward>(rewardJson);
                break;
                case RewardType.Weapon:
                    WeaponReward weapon = JsonUtility.FromJson<WeaponReward>(rewardJson);
                    //여기에 무기 오브젝트 할당 코드
                    reward = weapon;
                break;
                case RewardType.Tower:
                    TowerReward tower = JsonUtility.FromJson<TowerReward>(rewardJson);
                    tower.towerPrefab = Resources.Load<TowerMapper>($"Reward/Tower/{tower.towerId}").tower;
                    reward = tower;
                break;
                case RewardType.Enhance:
                    WeaponEnhanceReward enhance = JsonUtility.FromJson<WeaponEnhanceReward>(rewardJson);
                    
                    //무기 강화 보상은 무기 획득 시 메인에 추가
                    if(!enhanceCache.ContainsKey(enhance.targetId))
                    {
                        enhanceCache[enhance.targetId] = new List<WeaponEnhanceReward>();
                    }
                    enhanceCache[enhance.targetId].Add(enhance);
                    reward = enhance; 
                break;
            }

            reward.image = Resources.Load<Sprite>($"Reward/Sprite/{reward.imagePath}");

            //새 게임이면 획득 가능한 보상 수 초기화
            if(!GameManager.instance.isLoadedGame)
            {
                reward.currentPickable = reward.pickableCount;
            }

            
            if(reward.isUnlocked && reward.type != RewardType.Enhance)
            {
                rewards[reward.rarity].Add(reward);
            }
        }
    }

    private void Start() 
    {
        /*
        BuildController 활성화보다 늦게 호출해야함
        BuildController는 Awake에서 할당 => (PhaseManager 클래스 참조)
        */
        if(GameManager.instance.isLoadedGame)
        {
            gameplayManager.OnWaveEnd += InitReward_LoadedGame;
        }
        else
        {
            gameplayManager.OnWaveEnd += InitReward;
        }
    }

    public void InitReward_LoadedGame()
    {
        gameplayManager.OnWaveEnd += InitReward;
        gameplayManager.OnWaveEnd -= InitReward_LoadedGame;
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

        reward.pickableCount--;

        if(reward.pickableCount <= 0)
        {
            rewards[reward.rarity].Remove(reward);
        }

        //보상 적용 코드
        switch(reward.type)
        {
            case RewardType.Status:
                var statusReward = reward as StatusReward;
                playerStatus.Enhance(statusReward.statusType, statusReward.value);
                JSONParser.SaveJSON<StatusReward>($"{Application.streamingAssetsPath}/Rewards/{statusReward.id}.json", statusReward);
            break;
            case RewardType.Weapon:
                var weaponReward = reward as WeaponReward;
                JSONParser.SaveJSON<WeaponReward>($"{Application.streamingAssetsPath}/Rewards/{weaponReward.id}.json", weaponReward);
            break;
            case RewardType.Tower:
                var towerReward = reward as TowerReward;
                towerManager.CreateTower(towerReward.towerPrefab);
                JSONParser.SaveJSON<TowerReward>($"{Application.streamingAssetsPath}/Rewards/{towerReward.id}.json", towerReward);
            break;
            case RewardType.Enhance:
                var enhanceReward = reward as WeaponEnhanceReward;
                JSONParser.SaveJSON<WeaponEnhanceReward>($"{Application.streamingAssetsPath}/Rewards/{enhanceReward.id}.json", enhanceReward);
            break;
            default:
                Debug.LogWarning("Invalid Reward");
            break;
        }

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
