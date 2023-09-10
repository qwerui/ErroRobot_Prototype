using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour, IControllerBase
{
    public GameObject pauseMenu;
    public CameraController cameraController;
    public GameObject nextWavePanel;

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
        //pointer.SetDirection(direction, inputEvent);
        cameraController.controlWithKey(direction, inputEvent);
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            RaycastHit hit = cameraController.RaycastCheck();

            if(hit.collider != null)
            {
                if(hit.transform.CompareTag("Ground"))
                {
                    //next wave
                    nextWavePanel.SetActive(true);
                }
                else
                {
                    hit.transform.GetComponent<IRaycastInteractable>()?.Execute();
                }
            }
        }
    }

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            pauseMenu.SetActive(true);
        }
    }
}
