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
        cameraController.DisableRotation();
    }

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        //pointer.SetDirection(direction, inputEvent);
        cameraController.controlWithKey(direction);
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
                    // 튜토리얼 진행 중일 때는 다음 페이즈 비활성화
                    if (!TutorialManager.instance.allowNextPhase)
                        return;
                    //next wave
                    nextWavePanel.SetActive(true);
                    cameraController.DisableRotation();
                }
                else
                {
                    hit.transform.GetComponent<IRaycastInteractable>()?.Execute();
                    cameraController.DisableRotation();
                }
            }
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
}
