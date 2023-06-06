using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : IControllerPlatform
{
    public void Execute(IControllerBase controller)
    {
#region OnPressed

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            controller.OnNavigate(Vector2.up, InputEvent.Pressed);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            controller.OnNavigate(Vector2.down, InputEvent.Pressed);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            controller.OnNavigate(Vector2.left, InputEvent.Pressed);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            controller.OnNavigate(Vector2.right, InputEvent.Pressed);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            controller.OnSubmit(InputEvent.Pressed);
        }

#endregion
#region OnReleased

        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            controller.OnNavigate(Vector2.up, InputEvent.Released);
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            controller.OnNavigate(Vector2.down, InputEvent.Released);
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            controller.OnNavigate(Vector2.left, InputEvent.Released);
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            controller.OnNavigate(Vector2.right, InputEvent.Released);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            controller.OnSubmit(InputEvent.Released);
        }
#endregion
    }
}
