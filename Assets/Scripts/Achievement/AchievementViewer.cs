using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementViewer : MonoBehaviour
{
    List<AchievementContent> achievementContents = new List<AchievementContent>();

    public GameObject contentsParent;
    public AchievementContent achievementContent;

    [Header("Achievement Texts")]
    public TMP_Text achievementTitle;
    public TMP_Text achievementDescription;
    public TMP_Text achievementReward;
    public TMP_Text achievementStory;

    int index;

    private void Start() 
    {
        var achievements = GameManager.instance.achievementManager.GetAchievement();

        foreach(Achievement achievement in achievements)
        {
            var createdContent = Instantiate<AchievementContent>(achievementContent, contentsParent.transform);
            createdContent.Init(achievement);
            achievementContents.Add(createdContent);
        }

        index = 0;

        if(achievementContents.Count > 0)
        {
            achievementContents[index].Activate();
            ShowAchievementInfo(achievementContents[index].achievement);
        }
    }

    public void ChangeContent(float direction)
    {
        if(Mathf.Abs(direction) < Mathf.Epsilon || achievementContents.Count <= 0)
        {
            return;
        }

        achievementContents[index].Deactivate();

        index += direction > 0 ? -1 : 1;
        index = Mathf.Clamp(index, 0, achievementContents.Count - 1);

        achievementContents[index].Activate();
        ShowAchievementInfo(achievementContents[index].achievement);
    }

    void ShowAchievementInfo(Achievement achievement)
    {
        achievementTitle.SetText(achievement.title);
        achievementDescription.SetText(achievement.description);
        achievementReward.SetText(achievement.reward);
        achievementStory.SetText(achievement.story);
    }
}
