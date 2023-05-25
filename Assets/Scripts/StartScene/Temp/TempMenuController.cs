using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class TempMenuController : MonoBehaviour
    {
        StartMenuManager manager;

        void Start()
        {
            manager = GameObject.FindObjectOfType<StartMenuManager>();
        }

        void Update() 
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                manager?.SelectOption(-1);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                manager?.SelectOption(1);
            }

            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                manager?.Execute();
            }
        }
    }

}
