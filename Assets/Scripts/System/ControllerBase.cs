using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerBase
{
    void OnSubmit(InputEvent inputEvent);
    void OnNavigate(Vector2 direction, InputEvent inputEvent);
}

public enum InputEvent
{
    Pressed,
    Released
}
