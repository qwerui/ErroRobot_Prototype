using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager
{
    AchievementList achievementList;
    Dictionary<AchievementEvent, AchievementChecker> checkers = new Dictionary<AchievementEvent, AchievementChecker>();

    public AchievementNotifier notifier;

    public List<Achievement> GetAchievement() => achievementList.achievements;

    public AchievementManager()
    {
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
            checkers[achievement.eventType].Add(achievement);
        }
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
            }
        }
    }
}