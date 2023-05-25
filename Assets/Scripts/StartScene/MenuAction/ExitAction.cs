using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class ExitAction : StartMenuListOption
    {
        public override void Execute()
        {
            //게임 종료
            Application.Quit();
        }
    }
}
