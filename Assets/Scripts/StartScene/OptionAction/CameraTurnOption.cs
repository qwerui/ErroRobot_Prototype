using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace StartMenu
{
    public class CameraTurnOption : OptionsContent
    {
        public TMP_Text cameraSpeedText;
        public Slider cameraSpeedSlider;

        const string cameraSpeedPref = "CameraSpeed";

        int cameraSpeed;

        public override void Init()
        {
            cameraSpeed = PlayerPrefs.GetInt(cameraSpeedPref, 50);
            cameraSpeedText.SetText(cameraSpeed.ToString());
            cameraSpeedSlider.value = cameraSpeed;
        }

        public override void Execute(float direction)
        {
            //Slider 이동 및 카메라 회전 속도 조절
            cameraSpeed += direction > 0 ? 1 : -1;
            cameraSpeed = Mathf.Clamp(cameraSpeed, 0, 100);
            PlayerPrefs.SetInt(cameraSpeedPref, cameraSpeed);
            cameraSpeedText.SetText(cameraSpeed.ToString());
            cameraSpeedSlider.value = cameraSpeed;
        }
    }

}
