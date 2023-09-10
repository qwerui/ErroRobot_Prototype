using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementNotifier : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] Image image;
    Animator anim;

    Queue<Achievement> achievementQueue; 

    private void Awake() 
    {
        if(GameManager.instance.achievementManager.notifier != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        TryGetComponent<Animator>(out anim);
        GameManager.instance.achievementManager.notifier = this;
        achievementQueue = new Queue<Achievement>();
    }

    public static void Init()
    {
        var notifierPrefab = Resources.Load<AchievementNotifier>("System/AchievementNotifier");
        Instantiate(notifierPrefab);
    }

    public void ShowNotifier(Achievement achievement)
    {
        achievementQueue.Enqueue(achievement);
        OnNotifiyEnd();
    }

    public void OnNotifiyEnd()
    {
        if(achievementQueue.Count > 0)
        {
            var achievement = achievementQueue.Dequeue();
            title.SetText(achievement.title);
            image.sprite = achievement.image;
            anim.SetTrigger("Succeed");
        }
    }
}
