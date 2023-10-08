using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class StartMenuManager : MonoBehaviour
    {
        List<StartMenuListOption> menuOptionList;

        int menuIndex;

        private void Awake() 
        {
            menuIndex = 0;
        }

        void Start()
        {
            menuOptionList = new List<StartMenuListOption>(GameObject.FindObjectsOfType<StartMenuListOption>());
            menuOptionList.Sort((StartMenuListOption a, StartMenuListOption b)=>{
                return a.transform.GetSiblingIndex() - b.transform.GetSiblingIndex();
            });
            menuOptionList[menuIndex].Select();
        }

        public void SelectOption(float direction)
        {
            if(direction < Mathf.Epsilon && direction > -Mathf.Epsilon)
                return;
            menuOptionList[menuIndex].Deselect();
            
            menuIndex += direction < 0 ? 1 : -1;

            menuIndex = Mathf.Clamp(menuIndex, 0, menuOptionList.Count-1);
            menuOptionList[menuIndex].Select();
        }

        public void Execute()
        {
            menuOptionList[menuIndex].Execute();
        }

        public void SetExit()
        {
            menuOptionList[menuIndex].Deselect();
            menuIndex = menuOptionList.Count - 1;
            menuOptionList[menuIndex].Select();
        }
    }
}
