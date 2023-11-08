using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    AchievementList achievementList;
    Dictionary<AchievementEvent, AchievementChecker> checkers = new Dictionary<AchievementEvent, AchievementChecker>();

    AchievementNotifier notifier;

    public List<Achievement> GetAchievement() => achievementList.achievements;

    private void Awake()
    {
        var notifierPrefab = Resources.Load<AchievementNotifier>("System/AchievementNotifier");
        notifier = Instantiate(notifierPrefab);

        achievementList = JSONParser.ReadJSON<AchievementList>($"{Application.streamingAssetsPath}/Achievement.json");
        var achievements = achievementList.achievements;
        if(achievements.Count > 0)
        {
            achievements.Sort((Achievement a, Achievement b) => a.id - b.id);
        }

        //업적 체크 클래스를 각 이벤트에 할당
        foreach(string eventString in System.Enum.GetNames(typeof(AchievementEvent)))
        {
            AchievementEvent converted = System.Enum.Parse<AchievementEvent>(eventString);
            checkers[converted] = new AchievementChecker();
        }

        //업적 체크 클래스에 체크할 업적 할당
        foreach(Achievement achievement in achievements)
        {
            achievement.image = Resources.Load<Sprite>($"AchievementSprite/{achievement.imagePath}");
            checkers[achievement.eventType].Add(achievement);
        }
    }

    void Start()
    {
        var status = FindFirstObjectByType<PlayerStatus>();
        status.OnChange_ClearCount += (value) => CheckAchievement(AchievementEvent.ClearCount, value);
        status.OnChange_KillCount += (value) => CheckAchievement(AchievementEvent.KillCount, value);
        status.OnChange_PlayCount += (value) => CheckAchievement(AchievementEvent.PlayCount, value);
        status.OnChange_WaveCount += (value) => CheckAchievement(AchievementEvent.WaveCount, value);
    }

    public void CheckAchievement(AchievementEvent occuredEvent, float value = 1)
    {
        CheckAchievement_Internal(occuredEvent, value);
    }

    public void CheckAchievement(AchievementEvent occuredEvent, int value)
    {
        CheckAchievement_Internal(occuredEvent, (float)value);
    }

    void CheckAchievement_Internal(AchievementEvent occuredEvent, float value)
    {
        AchievementChecker checker = checkers[occuredEvent];
        
        if(checker != null)
        {
            Achievement succeed = checker.Check(value);
            
            if(succeed != null)
            {
                succeed.isAchieved = true;
                JSONParser.SaveJSON<AchievementList>($"{Application.streamingAssetsPath}/Achievement.json", achievementList);
                notifier.ShowNotifier(succeed);

                switch(succeed.rewardType)
                {
                    case AchievementRewardType.Enhance:
                    var startStatus = JSONParser.ReadJSON<StartStatus>($"{Application.streamingAssetsPath}/StartStatus.json");
                    
                    switch(succeed.statusType)
                    {
                        case StatusType.MaxHP:
                            startStatus.maxHp += succeed.rewardValue;
                        break;
                        case StatusType.MaxShield:
                            startStatus.maxShield += succeed.rewardValue;
                        break;
                        case StatusType.CoreGain:
                            startStatus.coreGainPercent += succeed.rewardValue;
                        break;
                        case StatusType.ShieldRecover:
                            startStatus.shieldRecovery += succeed.rewardValue;
                        break;
                        case StatusType.WeaponSlot:
                            startStatus.weaponSlot += 1;
                        break;
                    }

                    JSONParser.SaveJSON<StartStatus>($"{Application.streamingAssetsPath}/StartStatus.json", startStatus);
                    break;
                    case AchievementRewardType.Unlock:
                    var unlock = JSONParser.ReadJSONString($"{Application.streamingAssetsPath}/Rewards/{(int)succeed.rewardValue}.json");
                    unlock.Replace("\"isUnlocked\":false", "\"isUnlocked\":true");
                    JSONParser.SaveJSONString($"{Application.streamingAssetsPath}/Rewards/{(int)succeed.rewardValue}.json", unlock);
                    break;
                }
            }
        }
    }
}