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
    Dictionary<int, AudioClip> soundList = new Dictionary<int, AudioClip>();
    
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

            SoundInfo[] sounds = Resources.LoadAll<SoundInfo>("Audio");
            foreach(SoundInfo soundInfo in sounds)
            {
                soundList[soundInfo.id] = soundInfo.clip;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(int id)
    {
        if (bgm != null)
        {
            bgm.Stop();
            AudioClip bgmClip = soundList[id];
            if(bgmClip != null)
            {
                bgm.clip = bgmClip;
                bgm.Play();
            }
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