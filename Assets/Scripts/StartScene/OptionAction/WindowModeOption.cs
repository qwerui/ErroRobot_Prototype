using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StartMenu
{
    public class WindowModeOption : OptionsContent
    {
        List<KeyValuePair<string, FullScreenMode>> windowMode = new List<KeyValuePair<string, FullScreenMode>>();
        public TMP_Text windowModeText;

        int index = 0;
        const string windowModePref = "ScreenMode";

        protected override void Awake() 
        {
            base.Awake();
            windowMode.Add(new KeyValuePair<string, FullScreenMode>("FullScreen", FullScreenMode.FullScreenWindow));
            windowMode.Add(new KeyValuePair<string, FullScreenMode>("Windowed", FullScreenMode.Windowed));
        }

        public override void Init()
        {
            index = PlayerPrefs.GetInt(windowModePref, 0);
            windowModeText.SetText(windowMode[index].Key);
        }

        public override void Execute()
        {
            Screen.fullScreenMode = windowMode[index].Value;
            PlayerPrefs.SetInt(windowModePref, index);
        }

        public override void Execute(float direction)
        {
            index += direction > 0 ? 1 : -1;
            index = Mathf.Clamp(index, 0, windowMode.Count-1);
            windowModeText.SetText(windowMode[index].Key);
        }
    }

}
