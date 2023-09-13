using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 업적 체크하는 클래스
/// Manager에서 Dictionary로 AchievementEvent와 묶여있기 때문에 모든 업적은 이 클래스로 통일
/// </summary>
public class AchievementChecker
{
    List<Achievement> achievements = new List<Achievement>();

    public void Add(Achievement achievement)
    {
        achievements.Add(achievement);
    }

    public Achievement Check(float value)
    {
        foreach(Achievement achievement in achievements)
        {
            if(value >= achievement.requireValue && !achievement.isAchieved)
            {
                return achievement;
            }
        }

        return null;
    }
}
