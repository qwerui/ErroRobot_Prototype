using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCountChecker : IAchievementChecker
{
    int[] condition = {
        10,
        50,
        100
    };

    List<Achievement> achievements = new List<Achievement>();

    public void Add(Achievement achievement)
    {
        achievements.Add(achievement);
    }

    public Achievement Check(int value)
    {
        if(condition.Length != achievements.Count)
        {
            Debug.LogAssertion($"{this.GetType()} do not have same condition and achievements!!");
            return null;
        }

        for(int i=0;i<condition.Length;i++)
        {
            if(value >= condition[i] && !achievements[i].isAchieved)
            {
                return achievements[i];
            }
        }
        return null;
    }

    public Achievement Check(float value)
    {
        throw new NotImplementedException();
    }

    public Achievement Check(Enum value)
    {
        throw new NotImplementedException();
    }
}
