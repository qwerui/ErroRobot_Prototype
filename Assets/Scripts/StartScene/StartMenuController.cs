using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : IControllerBase
{
    public StartMenu.StartMenuManager startMenuManager;

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        startMenuManager?.SelectOption(direction.y);
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        startMenuManager?.Execute();
    }
}
