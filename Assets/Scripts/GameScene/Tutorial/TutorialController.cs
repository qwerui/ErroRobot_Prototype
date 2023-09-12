using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour, IControllerBase
{
    public TutorialManager tutorialManager;

    private void OnEnable() 
    {
        PlayerController.instance.AddController(this);    
    }

    private void OnDisable() 
    {
        PlayerController.instance?.DeleteController(this);    
    }

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            tutorialManager.Skip();
        }
    }

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        //동작 없음
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            tutorialManager.Next();
        }       
    }
}
