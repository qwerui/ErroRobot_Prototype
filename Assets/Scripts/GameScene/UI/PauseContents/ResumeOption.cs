using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeOption : PauseMenuContent
{
    public GameObject pauseMenu;

    public override void Execute()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }
}
