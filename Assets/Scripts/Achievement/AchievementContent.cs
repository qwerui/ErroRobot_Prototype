using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementContent : MonoBehaviour
{
    public Achievement achievement;

    public TMP_Text title;
    public Image image;
    public Outline outline;

    const string notAchievedTitle = "Locked";

    public void Activate() => outline.enabled = true;
    public void Deactivate() => outline.enabled = false;

    private void Awake() 
    {
        TryGetComponent<Outline>(out outline);    
    }

    public void Init(Achievement newAchievement)
    {
        achievement = newAchievement;
        if(newAchievement.isAchieved)
        {
            title.SetText(achievement.title);
        }
        else
        {
            title.SetText(notAchievedTitle);
        }
        image.sprite = achievement.image;
    }
}
