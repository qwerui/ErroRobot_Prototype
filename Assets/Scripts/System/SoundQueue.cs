using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
사용법
PlaySFX에 clip을 매개변수로 넘김
*/
public class SoundQueue : MonoBehaviour
{
    private static SoundQueue _instance = null;
    public static SoundQueue instance
    {
        get
        {
            if(_instance == null)
            {
                return null;
            }
            return _instance;
        }
    }

    Queue<AudioClip> playQueue = new();
    HashSet<string> alreadyEnqueueSet = new();
    
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;

    public static void Init()
    {
        if(_instance == null)
        {
            var soundPrefab = Resources.Load<SoundQueue>("System/SoundQueue");
            _instance = Instantiate<SoundQueue>(soundPrefab);
        }
    }

    private void Awake() 
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StopBGM()
    {
        if (bgm != null)
        {
            bgm.Stop();
        }
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgm != null)
        {
            bgm.Stop();

            bgm.clip = bgmClip;
            bgm.Play();
            
        }

    }

    public void PlaySFX(AudioClip clip)
    {
        if(!alreadyEnqueueSet.Contains(clip.name))
        {
            alreadyEnqueueSet.Add(clip.name);
            playQueue.Enqueue(clip);
        }
    }

    private void Update() 
    {
        while(playQueue.Count > 0)
        {
            AudioClip currentSound = playQueue.Dequeue();
            if(currentSound != null)
            {
                sfx?.PlayOneShot(currentSound);
            }
        }
        alreadyEnqueueSet.Clear();
    }
}