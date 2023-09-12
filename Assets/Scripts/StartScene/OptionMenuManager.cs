using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class OptionMenuManager : MonoBehaviour
    {
        List<OptionsContent> options;
        OptionMenuController optionController;

        int menuIndex = 0;

        void Start()
        {
            options = new List<OptionsContent>(GameObject.FindObjectsOfType<OptionsContent>());
            options.Sort((OptionsContent a, OptionsContent b) => a.transform.GetSiblingIndex() - b.transform.GetSiblingIndex());
            options[menuIndex].Activate();
            optionController = GameObject.FindObjectOfType<OptionMenuController>(true);
        }

        public void OpenOption()
        {
            optionController.gameObject.SetActive(true);
            options[menuIndex].Deactivate();
            menuIndex = 0;
            options[menuIndex].Activate();
            foreach(OptionsContent content in options)
            {
                content.Init();
            }
        }

        public void SelectOption(float direction)
        {
            if(direction < Mathf.Epsilon && direction > -Mathf.Epsilon)
                return;
            options[menuIndex].Deactivate();
            menuIndex += direction < 0 ? 1 : -1;
            menuIndex = Mathf.Clamp(menuIndex, 0, options.Count-1);
            options[menuIndex].Activate();
        }

        public void ChangeOptionValue(float direction)
        {
            if(direction < Mathf.Epsilon && direction > -Mathf.Epsilon)
            {
                return;
            }
            options[menuIndex].Execute(direction);
        }

        public void Submit()
        {
            options[menuIndex].Execute();
        }
    }

}
