using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class OptionMenuController : MonoBehaviour, IControllerBase
    {
        public OptionMenuManager optionMenuManager;

        private void OnEnable() => PlayerController.instance.AddController(this);

        private void OnDisable()
        {
            PlayerController.instance?.DeleteController(this);
        }

        public void OnNavigate(Vector2 direction, InputEvent inputEvent)
        {
            if (inputEvent == InputEvent.Pressed)
            {
                optionMenuManager.SelectOption(direction.y);
                optionMenuManager.ChangeOptionValue(direction.x);
            }
        }

        public void OnSubmit(InputEvent inputEvent)
        {
            if(inputEvent == InputEvent.Pressed)
            {
                optionMenuManager.Submit();
            }
        }

        public void OnCancel(InputEvent inputEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}

