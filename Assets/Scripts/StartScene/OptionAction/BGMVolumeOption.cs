using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace StartMenu
{
    public class BGMVolumeOption : OptionsContent
    {
        public Slider bgmSlider;
        public TMP_Text bgmText;

        const string bgmPref = "BGM";
        int bgmVolume;

        public override void Init()
        {
            bgmVolume = PlayerPrefs.GetInt(bgmPref, 50);
            bgmText.SetText(bgmVolume.ToString());
            bgmSlider.value = bgmVolume;
        }

        public override void Execute(float direction)
        {
            //Slider 움직임 및 bgm 볼륨 조절
            bgmVolume += direction > 0 ? 1 : -1;
            bgmVolume = Mathf.Clamp(bgmVolume, 0, 100);
            PlayerPrefs.SetInt(bgmPref, bgmVolume);
            bgmText.SetText(bgmVolume.ToString());
            bgmSlider.value = bgmVolume;
        }
    }

}
