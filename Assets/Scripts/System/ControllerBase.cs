using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerBase
{
    void OnSubmit(InputEvent inputEvent);
    void OnNavigate(Vector2 direction, InputEvent inputEvent);
    void OnCancel(InputEvent inputEvent);
}

public interface IDialControl
{
    //void OnDial(Vector2 direction);
    void OnDial(InputEvent inputEvent);
}

public enum InputEvent
{
    Pressed,
    Released
}
