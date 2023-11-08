using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Singleton Monobehavior 생성기
public static class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        PlayerController.Init();
        SoundQueue.Init();
        SerialPortManager.Init();
        DOTween.KillAll();
    }
}
