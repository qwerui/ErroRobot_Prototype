using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public BuildController buildController;
    public DefenceController defenceController;
    public GameoverController gameoverController;

    public UIManager UI;

    void Start()
    {
        buildController.Init();
        defenceController.Init();
        buildController.gameObject.SetActive(true); 
    }

    public void Gameover()
    {
        Time.timeScale = 0.0f;
        gameoverController.gameObject.SetActive(true);
        UI.OnGameover();
    }
}
