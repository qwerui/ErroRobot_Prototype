using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : IControllerPlatform
{
    Dictionary<KeyCode, Vector2> navigateDict = new Dictionary<KeyCode, Vector2>();
    Dictionary<char, Vector2> navigateDict_ctrl = new Dictionary<char, Vector2>();
    Vector2 NaviVector = Vector2.zero;
    
    public KeyboardController()
    {
        navigateDict.Add(KeyCode.UpArrow, Vector2.up);
        navigateDict.Add(KeyCode.DownArrow, Vector2.down);
        navigateDict.Add(KeyCode.RightArrow, Vector2.right);
        navigateDict.Add(KeyCode.LeftArrow, Vector2.left);

        navigateDict_ctrl.Add('W', Vector2.up);
        navigateDict_ctrl.Add('S', Vector2.down);
        navigateDict_ctrl.Add('D', Vector2.right);
        navigateDict_ctrl.Add('A', Vector2.left);

        navigateDict_ctrl.Add('w', Vector2.up);
        navigateDict_ctrl.Add('s', Vector2.down);
        navigateDict_ctrl.Add('d', Vector2.right);
        navigateDict_ctrl.Add('a', Vector2.left);
    }

    public void Reset()
    {
        NaviVector = Vector2.zero;
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

        if( SerialPortManager.Instance.sp.IsOpen )
        {
            switch( SerialPortManager.Instance._key )
            {
                case 'W':
                    if( NaviVector.y <= 0.0 )
                    {
                        isNavigate = true;
                        NaviVector += navigateDict_ctrl['w'];
                    }
                break;

                case 'S':
                    if( NaviVector.y >= 0.0 )
                    {
                        isNavigate = true;
                        NaviVector += navigateDict_ctrl['s'];
                    }
                break;

                case 'D':
                    if( NaviVector.x <= 0.0 )
                    {
                        isNavigate = true;
                        NaviVector += navigateDict_ctrl['d'];
                    }
                break;
            
                case 'A':
                    if( NaviVector.x >= 0.0 )
                    {
                        isNavigate = true;
                        NaviVector += navigateDict_ctrl['a'];
                    }
                break;

                case 'J':
                    controller.OnSubmit(InputEvent.Pressed);
                break;

                case 'K':
                    controller.OnCancel(InputEvent.Pressed);
                break;
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

        if( SerialPortManager.Instance.sp.IsOpen )
        {
            switch( SerialPortManager.Instance._key )
            {
                case 'w':
                    if( NaviVector.y > 0.0 )
                    {
                        isNavigate = true;
                        NaviVector -= navigateDict_ctrl['w'];
                    }
                break;

                case 's':
                    if( NaviVector.y < 0.0 )
                    {
                        isNavigate = true;
                        NaviVector -= navigateDict_ctrl['s'];
                    }
                break;

                case 'd':
                    if( NaviVector.x > 0.0 )
                    {
                        isNavigate = true;
                        NaviVector -= navigateDict_ctrl['d'];
                    }
                break;
            
                case 'a':
                    if( NaviVector.x < 0.0 )
                    {
                        isNavigate = true;
                        NaviVector -= navigateDict_ctrl['a'];
                    }
                break;

                case 'j':
                    controller.OnSubmit(InputEvent.Released);
                break;

                case 'k':
                    controller.OnCancel(InputEvent.Released);
                break;
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
            controller.OnCancel(InputEvent.Released);
        }
#endregion
#region Dial
        if(controller is IDialControl)
        {
            IDialControl tempController = controller as IDialControl;

            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                tempController.OnDial(InputEvent.Pressed);
            }

            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                tempController.OnDial(InputEvent.Released);
            }
        }
#endregion

    }
}
