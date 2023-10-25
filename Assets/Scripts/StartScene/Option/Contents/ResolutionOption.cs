using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StartMenu
{
    public class ResolutionOption : OptionsContent
    {
        List<KeyValuePair<string, Vector2>> resolution = new List<KeyValuePair<string, Vector2>>();
        public TMP_Text resolutionText;

        int index = 0;
        const string resolutionPref = "Resolution";

        protected override void Awake() 
        {
            base.Awake();
            resolution.Add(new KeyValuePair<string, Vector2>("1920x1080", new Vector2(1920, 1080)));
            resolution.Add(new KeyValuePair<string, Vector2>("1280x720", new Vector2(1280, 720)));
            resolution.Add(new KeyValuePair<string, Vector2>("800x600", new Vector2(800, 600)));
        }

        public override void Init()
        {
            index = PlayerPrefs.GetInt(resolutionPref, 0);
            resolutionText.SetText(resolution[index].Key);
        }

        public override void Execute()
        {
            Vector2 resolutionValue = resolution[index].Value;
            Screen.SetResolution((int)resolutionValue.x, (int)resolutionValue.y, Screen.fullScreenMode);
            PlayerPrefs.SetInt(resolutionPref, index);
        }

        public override void Execute(float direction)
        {
            index += direction > 0 ? 1 : -1;
            index = Mathf.Clamp(index, 0, resolution.Count-1);
            resolutionText.SetText(resolution[index].Key);
        }
    }

}
