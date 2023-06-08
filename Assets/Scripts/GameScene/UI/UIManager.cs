using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PhaseManager phaseManager;

    public GameObject BuildUI;
    public GameObject Aim;
    public GameObject Reward;

    private void Start() 
    {
        phaseManager.OnWaveStart += OnWaveStart;
        phaseManager.OnWaveEnd += OnWaveEnd;    
    }

    public void OnWaveStart()
    {
        BuildUI.SetActive(false);
        Aim.SetActive(true);
    }

    public void OnWaveEnd()
    {
        BuildUI.SetActive(true);
        Aim.SetActive(false);
        Reward.SetActive(true);
    }
}
