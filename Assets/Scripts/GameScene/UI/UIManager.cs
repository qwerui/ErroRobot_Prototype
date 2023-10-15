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
    
    public GameObject aim;
    public GameoverPanel gameoverPanel;
    public TMP_Text waveText;

    [Header("Pointer")]
    public Pointer pointer;

    [Header("HitScreen")]
    public HitScreenManager hitScreenManager;

    [Header("Effect")]
    public WaveEffect waveEffect;
    
    private void Awake() 
    {
        phaseManager.OnWaveStart += OnWaveStart;
        phaseManager.OnWaveEnd += OnWaveEnd;
        playerStatus.OnValueChanged += UpdateStatus;
        playerStatus.OnDamaged += hitScreenManager.ShowHitScreen;
    }

    public void OnWaveStart()
    {
        buildUI.SetActive(false);
        pointer.SetPointer(PointerIcon.Rifle);

        waveEffect.WaveStartEffect();
    }

    public void OnWaveEnd()
    {
        buildUI.SetActive(true);
        pointer.SetPointer(PointerIcon.Build);
        waveText.SetText($"Wave {phaseManager.wave}");
    }

    public void UpdateStatus(PlayerStatus status)
    {
        shieldText.SetText($"{status.CurrentShield:0}/{status.MaxShield:0}");
        hpText.SetText($"{status.CurrentHp:0}/{status.MaxHp:0}");
        coreText.SetText($"{status.Core}");
        shieldBar.fillAmount = status.CurrentShield / status.MaxShield;
        hpBar.fillAmount = status.CurrentHp / status.MaxHp;
    }

    public void OnGameover()
    {
        gameoverPanel.Gameover();
    }

    public void OnGameClear()
    {
        gameoverPanel.GameClear();
    }
}
