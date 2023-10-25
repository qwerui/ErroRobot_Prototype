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

    const string notAchievedString = "???";
    int index;

    private void Start() 
    {
        var achievementList = JSONParser.ReadJSON<AchievementList>($"{Application.streamingAssetsPath}/Achievement.json");
        var achievements = achievementList.achievements;

        foreach(Achievement achievement in achievements)
        {
            achievement.image = Resources.Load<Sprite>($"AchievementSprite/{achievement.imagePath}");
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
        if(achievement.isAchieved)
        {
            achievementTitle.SetText(achievement.title);
            achievementDescription.SetText(achievement.description);
            achievementReward.SetText(achievement.reward);
            achievementStory.SetText(achievement.story);
        }
        else
        {
            achievementTitle.SetText(notAchievedString);
            achievementDescription.SetText(achievement.description);
            achievementReward.SetText(notAchievedString);
            achievementStory.SetText(notAchievedString);
        }
        
    }
}
