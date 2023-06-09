using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PhaseManager phaseManager;

    public GameObject buildUI;
    public GameObject aim;
    public GameObject reward;
    public GameObject gameoverPanel;

    private void Start() 
    {
        phaseManager.OnWaveStart += OnWaveStart;
        phaseManager.OnWaveEnd += OnWaveEnd;    
    }

    public void OnWaveStart()
    {
        buildUI.SetActive(false);
        aim.SetActive(true);
    }

    public void OnWaveEnd()
    {
        buildUI.SetActive(true);
        aim.SetActive(false);
        reward.SetActive(true);
    }

    public void OnGameover()
    {
        gameoverPanel.SetActive(true);
    }
}
