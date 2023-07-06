using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Enqueue(AudioClip clip)
    {

    }

    private void Update() 
    {
        
    }
}
