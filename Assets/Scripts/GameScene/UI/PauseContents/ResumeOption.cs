using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeOption : PauseMenuContent
{
    public PauseController pauseController;

    public override void Execute()
    {
        Time.timeScale = 1.0f;
        pauseController.Deactivate();
    }
}
