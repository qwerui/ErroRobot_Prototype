using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceController : MonoBehaviour, IControllerBase
{
    public WeaponController weaponController;
    public CameraController cameraController;
    public PauseController pauseController;

    void Activate() => gameObject.SetActive(true);
    void Deactivate() => gameObject.SetActive(false);
    
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

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            pauseController.Activate(false);
        }
    }
}
