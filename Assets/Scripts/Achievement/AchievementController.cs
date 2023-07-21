using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour, IControllerBase
{
    public AchievementViewer viewer;

    private void OnEnable() 
    {
        PlayerController.instance.AddController(this);
    }

    private void OnDisable() 
    {
        PlayerController.instance?.DeleteController(this);
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        
    }

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            viewer.ChangeContent(direction.y);
        }
    }

    public void OnCancel(InputEvent inputEvent)
    {
        LoadingSceneManager.LoadScene("StartScene");
    }
}