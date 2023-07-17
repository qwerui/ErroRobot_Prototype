using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager
{
    Achievement[] achievements;

    AchievementNotifier notifier;

    public Achievement[] GetAchievement() => achievements;

    public AchievementManager()
    {
        achievements = Resources.LoadAll<Achievement>("Achievement");
        notifier = Resources.Load<AchievementNotifier>("System/AchievementNotifier");
        
        if(achievements.Length > 0)
        {
            System.Array.Sort(achievements,
            (Achievement a, Achievement b) =>
            a.id - b.id);
        }
    }
}
