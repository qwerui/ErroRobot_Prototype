using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMRequester : MonoBehaviour
{
    public AudioClip bgmClip;

    void Start()
    {
        SoundQueue.instance.PlayBGM(bgmClip);    
    }
}
