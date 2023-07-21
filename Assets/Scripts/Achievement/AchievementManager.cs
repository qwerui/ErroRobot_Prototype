using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager
{
    AchievementList achievementList;
    Dictionary<AchievementEvent, IAchievementChecker> checkers = new Dictionary<AchievementEvent, IAchievementChecker>();

    public AchievementNotifier notifier;

    public List<Achievement> GetAchievement() => achievementList.achievements;

    public AchievementManager()
    {
        achievementList = JSONParser.ReadJSON<AchievementList>($"{Application.dataPath}/Infos/Achievement.json");
        var achievements = achievementList.achievements;
        if(achievements.Count > 0)
        {
            achievements.Sort((Achievement a, Achievement b) => a.id - b.id);
        }
    }

    public void CheckAchievement(AchievementEvent occuredEvent, int value)
    {
        IAchievementChecker checker = checkers[occuredEvent];
        
        if(checker != null)
        {
            Achievement succeed = checker.Check(value);
            
            if(succeed != null)
            {
                notifier.ShowNotifier(succeed);
            }
        }
    }

    public void CheckAchievement(AchievementEvent occuredEvent, float value)
    {
        IAchievementChecker checker = checkers[occuredEvent];
        
        if(checker != null)
        {
            Achievement succeed = checker.Check(value);
            
            if(succeed != null)
            {
                notifier.ShowNotifier(succeed);
            }
        }
    }

    public void CheckAchievement(AchievementEvent occuredEvent, System.Enum value)
    {
        IAchievementChecker checker = checkers[occuredEvent];
        
        if(checker != null)
        {
            Achievement succeed = checker.Check(value);
            
            if(succeed != null)
            {
                notifier.ShowNotifier(succeed);
            }
        }
    }
}

///<summary>
///업적 체크하는 클래스
///</summary>
public interface IAchievementChecker
{
    public Achievement Check(int value);
    public Achievement Check(float value);
    public Achievement Check(System.Enum value);
}