using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
사용법
1. Resources/Audio 폴더에 SoundInfo를 생성(ScriptableObject)
2. 생성한 SoundInfo에 id와 음악 파일을 할당
3. 사용할 스크립트에서 id를 매개변수로 넘긴다.
-PlaySound
-PlayBGM
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

    Queue<int> playQueue = new Queue<int>();
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

    public void PlaySound(int id)
    {
        if(!playQueue.Contains(id))
        {
            playQueue.Enqueue(id);
        }
    }

    private void Update() 
    {
        while(playQueue.Count > 0)
        {
            int currentSoundId = playQueue.Dequeue();
            AudioClip currentSound = soundList[currentSoundId];
            if(currentSound != null)
            {
                sfx?.PlayOneShot(currentSound);
            }
        }
    }
}