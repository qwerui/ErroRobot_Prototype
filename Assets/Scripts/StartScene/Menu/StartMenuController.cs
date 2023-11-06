using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour, IControllerBase
{
    public StartMenu.StartMenuManager startMenuManager;
    public AudioClip navigateClip;
    public AudioClip selectClip;

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
        if(inputEvent == InputEvent.Pressed)
        {
            startMenuManager?.SelectOption(direction.y);
            SoundQueue.instance.PlaySFX(navigateClip);
        }
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            startMenuManager?.Execute();
            SoundQueue.instance.PlaySFX(selectClip);
        }
    }

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            startMenuManager?.SetExit();
            SoundQueue.instance.PlaySFX(selectClip);
        }
    }
}
