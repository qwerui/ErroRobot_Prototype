using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour, IControllerBase
{
    IGameUI gameUI;
    Pointer pointer;
    public AudioClip navigateClip;
    public AudioClip selectClip;

    private void OnEnable() 
    {
        if(pointer == null)
        {
            pointer = GameObject.FindObjectOfType<Pointer>();
        }

        gameUI = GetComponent<IGameUI>();
        pointer.Deactivate();
        PlayerController.instance.AddController(this);
    }

    private void OnDisable() 
    {
        if(pointer != null)
        {
            pointer.Activate();
        }
        PlayerController.instance?.DeleteController(this);
    }

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            gameUI?.OnCancel();
            SoundQueue.instance.PlaySFX(selectClip);
        }
    }

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            gameUI?.OnNavigate(direction);
            SoundQueue.instance.PlaySFX(navigateClip);
        }
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            gameUI?.OnSubmit();
            SoundQueue.instance.PlaySFX(selectClip);
        }
    }

    public void SetUI(IGameUI gameUI)
    {
        this.gameUI = gameUI;
    }
}

public interface IGameUI
{
    void OnNavigate(Vector2 direction);
    void OnSubmit();
    void OnCancel();
}
