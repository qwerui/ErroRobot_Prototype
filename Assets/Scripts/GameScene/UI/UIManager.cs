using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PhaseManager phaseManager;
    public PlayerStatus playerStatus;

    [Header("StatusUI")]
    public TMP_Text shieldText;
    public TMP_Text hpText;
    public TMP_Text coreText;
    public Image hpBar;
    public Image shieldBar;

    [Header("PhaseUI")]
    public GameObject buildUI;
    public Pointer pointer;
    public GameObject aim;
    public GameObject gameoverPanel;
    public TMP_Text waveText;

    [Header("Slots")]
    public TowerSlotList towerSlotList;

    [Header("HitScreen")]
    public HitScreenManager hitScreenManager;
    
    private void Awake() 
    {
        phaseManager.OnWaveStart += OnWaveStart;
        phaseManager.OnWaveEnd += OnWaveEnd;
        playerStatus.OnValueChanged += UpdateStatus;
        playerStatus.OnDamaged += hitScreenManager.ShowHitScreen;
    }

    public void OnWaveStart()
    {
        pointer.isBuild = false;
        buildUI.SetActive(false);
        aim.SetActive(true);
    }

    public void OnWaveEnd()
    {
        buildUI.SetActive(true);
        aim.SetActive(false);
        pointer.isBuild = true;
        waveText.SetText($"Wave {phaseManager.wave}");
    }

    public void UpdateStatus(PlayerStatus status)
    {
        shieldText.SetText($"{status.currentShield:0}/{status.maxShield:0}");
        hpText.SetText($"{status.currentHp:0}/{status.maxHp:0}");
        coreText.SetText($"{status.core}");
        shieldBar.fillAmount = status.currentShield / status.maxShield;
        hpBar.fillAmount = status.currentHp / status.maxHp;
    }

    public void OnGameover()
    {
        gameoverPanel.SetActive(true);
    }
}
