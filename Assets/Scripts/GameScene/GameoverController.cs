using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverController : MonoBehaviour, IControllerBase
{
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

    }

    public void OnSubmit(InputEvent inputEvent)
    {
        Time.timeScale = 1.0f;
        LoadingSceneManager.LoadScene("StartScene");
    }
}
