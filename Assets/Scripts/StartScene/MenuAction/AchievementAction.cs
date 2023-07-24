using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class AchievementAction : StartMenuListOption
    {
        public override void Execute()
        {
            LoadingSceneManager.LoadScene("AchievementScene");
        }
    }
}

