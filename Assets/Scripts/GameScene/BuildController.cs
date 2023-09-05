using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour, IControllerBase
{
    public Pointer pointer;
    public PauseController pauseController;

    void Activate() => gameObject.SetActive(true);
    void Deactivate() => gameObject.SetActive(false);

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
        pointer.SetDirection(direction, inputEvent);
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            pointer.Click();
        }
    }

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            pauseController.Activate(true);
        }
    }
}
