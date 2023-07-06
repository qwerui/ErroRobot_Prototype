using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace StartMenu
{
    public class SFXVolumeOption : OptionsContent
    {

        public Slider sfxSlider;
        public TMP_Text sfxText;

        const string sfxPref = "SFX";
        int sfxVolume;

        public override void Init()
        {
            sfxVolume = PlayerPrefs.GetInt(sfxPref, 50);
            sfxText.SetText(sfxVolume.ToString());
            sfxSlider.value = sfxVolume;
        }

        public override void Execute(float direction)
        {
            //Slider 움직임 및 sfx 볼륨 조절
            sfxVolume += direction > 0 ? 1 : -1;
            sfxVolume = Mathf.Clamp(sfxVolume, 0, 100);
            PlayerPrefs.SetInt(sfxPref, sfxVolume);
            sfxText.SetText(sfxVolume.ToString());
            sfxSlider.value = sfxVolume;
        }
    }

}
