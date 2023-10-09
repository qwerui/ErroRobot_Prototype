using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class OptionMenuController : MonoBehaviour, IControllerBase
    {
        public OptionMenuManager optionMenuManager;
        public AudioClip navigateClip;
        public AudioClip selectClip;

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
                SoundQueue.instance.PlaySFX(navigateClip);
            }
        }

        public void OnSubmit(InputEvent inputEvent)
        {
            if(inputEvent == InputEvent.Pressed)
            {
                optionMenuManager.Submit();
                SoundQueue.instance.PlaySFX(selectClip);
            }
        }

        public void OnCancel(InputEvent inputEvent)
        {
            if(inputEvent == InputEvent.Pressed)
            {
                optionMenuManager.ReturnMenu();
                SoundQueue.instance.PlaySFX(selectClip);
            }
        }
    }
}

