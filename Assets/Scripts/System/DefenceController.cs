using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceController : IControllerBase
{
    public Weapon weapon;

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {

    }

    public void OnSubmit(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            weapon?.OnFire();
        }
    }
}
