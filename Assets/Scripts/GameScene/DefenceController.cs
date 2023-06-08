using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceController : MonoBehaviour, IControllerBase
{
    public WeaponController weaponController;
    public CameraController cameraController;

    void Activate() => gameObject.SetActive(true);
    void Deactivate() => gameObject.SetActive(false);

    PhaseManager phaseManager;

    public void Init() 
    {
        phaseManager = GameObject.FindObjectOfType<PhaseManager>();
        phaseManager.OnWaveStart += Activate;
        phaseManager.OnWaveEnd += Deactivate;
    }

    private void OnEnable() 
    {
        PlayerController.instance.AddController(this);
    }

    private void OnDisable() 
    {
        PlayerController.instance?.DeleteController(this);
    }

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        cameraController.controlWithKey(direction, inputEvent);
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        weaponController.checkFire();
    }
}
