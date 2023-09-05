using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOption : PauseMenuContent
{
    public override void Execute()
    {
        Time.timeScale = 1.0f;
        LoadingSceneManager.LoadScene("StartScene");
    } 
}
