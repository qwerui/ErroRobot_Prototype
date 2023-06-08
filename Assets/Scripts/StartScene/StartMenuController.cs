using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour, IControllerBase
{
    public StartMenu.StartMenuManager startMenuManager;

    private void OnEnable() 
    {
        PlayerController.instance.AddController(this);    
    }
    
    private void OnDisable() 
    {
        PlayerController.instance?.DeleteController(this);
    }

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        startMenuManager?.SelectOption(direction.y);
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        startMenuManager?.Execute();
    }
}
