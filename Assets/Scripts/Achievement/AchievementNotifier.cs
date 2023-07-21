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

    private void Awake() 
    {
        if(GameManager.instance.achievementManager.notifier != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        TryGetComponent<Animator>(out anim);
        GameManager.instance.achievementManager.notifier = this;
    }

    public static void Init()
    {
        var notifierPrefab = Resources.Load<AchievementNotifier>("System/AchievementNotifier");
        Instantiate(notifierPrefab);
    }

    public void ShowNotifier(Achievement achievement)
    {
        title.SetText(achievement.title);
        image.sprite = achievement.image;
        anim.SetTrigger("Succeed");
    }
}
