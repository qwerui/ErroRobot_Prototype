using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance = null;
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    public AchievementManager achievementManager;
    public bool isLoadedGame;

    private GameManager()
    {
        achievementManager = new AchievementManager();
    }
}
