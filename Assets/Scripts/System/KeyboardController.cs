using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : IControllerPlatform
{
    Dictionary<KeyCode, Vector2> navigateDict = new Dictionary<KeyCode, Vector2>();
    Vector2 NaviVector = Vector2.zero;
    
    public KeyboardController()
    {
        navigateDict.Add(KeyCode.UpArrow, Vector2.up);
        navigateDict.Add(KeyCode.DownArrow, Vector2.down);
        navigateDict.Add(KeyCode.RightArrow, Vector2.right);
        navigateDict.Add(KeyCode.LeftArrow, Vector2.left);
    }

    public void Execute(IControllerBase controller)
    {
#region OnPressed
        bool isNavigate = false;
        foreach(KeyCode key in navigateDict.Keys)
        {    
            if(Input.GetKeyDown(key))
            {
                isNavigate = true;
                NaviVector += navigateDict[key];
            }
        }
        if(isNavigate)
        {
            controller.OnNavigate(NaviVector, InputEvent.Pressed);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            controller.OnSubmit(InputEvent.Pressed);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            controller.OnCancel(InputEvent.Pressed);
        }

#endregion
#region OnReleased

        isNavigate = false;
        foreach(KeyCode key in navigateDict.Keys)
        {    
            if(Input.GetKeyUp(key))
            {
                isNavigate = true;
                NaviVector -= navigateDict[key];
            }
        }
        if(isNavigate)
        {
            controller.OnNavigate(NaviVector, InputEvent.Released);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            controller.OnSubmit(InputEvent.Released);
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            controller.OnCancel(InputEvent.Pressed);
        }
#endregion
    }
}
