using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceController : MonoBehaviour, IControllerBase, IDialControl
{
    public WeaponController weaponController;
    public CameraController cameraController;
    public GameObject pauseMenu;
    public WeaponManager weaponManager;

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
        cameraController.controlWithKey(direction);
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            weaponController.PressButton();
        }

        else if (inputEvent == InputEvent.Released)
        {
            weaponController.ReleaseButton();
        }
    }

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            cameraController.DisableRotation();
            pauseMenu.SetActive(true);
        }
    }

    public void OnDial(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) < Mathf.Epsilon)
        {
            return;
        }
        weaponManager.ChangeWeapon(direction);
    }
}
