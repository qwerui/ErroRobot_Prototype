using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour, IControllerBase
{
    public AchievementViewer viewer;
    public AudioClip navigateClip;
    public AudioClip selectClip;

    private void OnEnable() 
    {
        PlayerController.instance.AddController(this);
    }

    private void OnDisable() 
    {
        PlayerController.instance?.DeleteController(this);
    }

    public void OnSubmit(InputEvent inputEvent)
    {
        //notifier 테스트 코드
        // if(inputEvent == InputEvent.Pressed)
        // {
        //     GameManager.instance.achievementManager.CheckAchievement(AchievementEvent.PlayCount, 10);
        // }
    }

    public void OnNavigate(Vector2 direction, InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Pressed)
        {
            viewer.ChangeContent(direction.y);
            SoundQueue.instance.PlaySFX(navigateClip);
        }
    }

    public void OnCancel(InputEvent inputEvent)
    {
        if(inputEvent == InputEvent.Released)
        {
            SoundQueue.instance.PlaySFX(selectClip);
            LoadingSceneManager.LoadScene("StartScene");
        }
    }
}
