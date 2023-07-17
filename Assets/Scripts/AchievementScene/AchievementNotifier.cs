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
        DontDestroyOnLoad(gameObject);
        TryGetComponent<Animator>(out anim);
    }

    public void ShowNotifier(Achievement achievement)
    {
        title.SetText(achievement.title);
        image.sprite = achievement.image;
        anim.SetTrigger("Succeed");
    }
}
