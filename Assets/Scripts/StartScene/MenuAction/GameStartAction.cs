using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class GameStartAction : StartMenuListOption
    {
        public override void Execute()
        {
            //게임 시작
            LoadingSceneManager.LoadScene("GameScene");
        }
    }
}

