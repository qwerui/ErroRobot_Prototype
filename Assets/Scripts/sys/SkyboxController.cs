using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public float speedFactor = 0.01f;
    private float rotSpeed = 0.0f;

    void Update()
    {
        if(rotSpeed > 360)
        {
            rotSpeed = 0.0f;
        }
        else
        {
            rotSpeed += speedFactor;
        }

        RenderSettings.skybox.SetFloat("_Rotation", rotSpeed);
    }
}
