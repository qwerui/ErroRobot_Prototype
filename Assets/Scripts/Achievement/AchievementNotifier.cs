using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementNotifier : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] Image image;
    [SerializeField] AudioClip clip;
    Animator anim;

    Queue<Achievement> achievementQueue; 

    private void Awake() 
    {
        TryGetComponent<Animator>(out anim);
        achievementQueue = new Queue<Achievement>();
    }

    public void ShowNotifier(Achievement achievement)
    {
        achievementQueue.Enqueue(achievement);
        OnNotifyEnd();
    }

    public void OnNotifyEnd()
    {
        if(achievementQueue.Count > 0)
        {
            var achievement = achievementQueue.Dequeue();
            title.SetText(achievement.title);
            image.sprite = achievement.image;
            SoundQueue.instance.PlaySFX(clip);
            anim.SetTrigger("Succeed");
        }
    }
}
