using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour, IControllerBase
{
    public PauseMenu pauseMenu;
    public Pointer pointer;

    bool isFromBuild;

    public void Activate(bool isFromBuild)
    {
        this.isFromBuild = isFromBuild;
        if(isFromBuild)
        {
            pointer.HidePointer();
        }
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        if(isFromBuild)
        {
            pointer.ShowPointer();
        }
        gameObject.SetActive(false);
    }

    private void OnEnable() 
    {
        Time.timeScale = 0.0f;
        PlayerController.instance.AddController(this);
        pauseMenu.gameObject.SetActive(true);
    }

    private void OnDisable() 
    {
        PlayerController.instance?.DeleteController(this);
        pauseMenu.gameObject.SetActive(false);
    }

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            pauseMenu.OnCancel();
        }
    }

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            pauseMenu.OnNavigate(direction.y);
        }
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            pauseMenu.OnSubmit();
        }
    }
}
