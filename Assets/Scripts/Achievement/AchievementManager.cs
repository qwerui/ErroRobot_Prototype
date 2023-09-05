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
        achievementList = JSONParser.ReadJSON<AchievementList>($"{Application.streamingAssetsPath}/Achievement.json");
        var achievements = achievementList.achievements;
        if(achievements.Count > 0)
        {
            achievements.Sort((Achievement a, Achievement b) => a.id - b.id);
        }

        InitChecker();

        foreach(Achievement achievement in achievements)
        {
            checkers[achievement.eventType].Add(achievement);
        }
    }

    //이 부분은 하드 코딩
    void InitChecker()
    {
        checkers.Add(AchievementEvent.PlayCount, new PlayCountChecker());
    }

    public void CheckAchievement(AchievementEvent occuredEvent, int value)
    {
        IAchievementChecker checker = checkers[occuredEvent];
        
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

    public void CheckAchievement(AchievementEvent occuredEvent, float value)
    {
        IAchievementChecker checker = checkers[occuredEvent];
        
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

    public void CheckAchievement(AchievementEvent occuredEvent, System.Enum value)
    {
        IAchievementChecker checker = checkers[occuredEvent];
        
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

///<summary>
///업적 체크하는 인터페이스<br/>
///각 조건에 따른 달성 체크는 하드 코딩<br/>
///업적 생성할 때 id는 먼저 달성 되는 순서로 지정<br/>
///ex) 50, 100, 150 처치 -> id : 1, 2, 3
///</summary>
public interface IAchievementChecker
{
    public Achievement Check(int value);
    public Achievement Check(float value);
    public Achievement Check(System.Enum value);
    public void Add(Achievement achievement);
}