using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class AchievementEditor : EditorWindow
{
    Button findButton;
    Button deleteButton;
    Button commitButton;

    IntegerField idField;
    TextField titleField;
    TextField descriptionField;
    TextField rewardField;
    TextField storyField;
    EnumField eventField;
    TextField imageField;
    FloatField requireField;
    EnumField rewardTypeField;
    FloatField rewardValueField;
    EnumField statusField;

    AchievementList achievementList;

    [MenuItem("Window/Editors/AchievementEditor")]
    public static void OpenWindow()
    {
        AchievementEditor wnd = GetWindow<AchievementEditor>();
        wnd.titleContent = new GUIContent("AchievementEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/AchievementEditor/AchievementEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/AchievementEditor/AchievementEditor.uss");

        findButton = root.Q<Button>("find-button");
        commitButton = root.Q<Button>("commit-button");
        deleteButton = root.Q<Button>("delete-button");

        idField = root.Q<IntegerField>();
        titleField = root.Q<TextField>("achievement-title");
        descriptionField = root.Q<TextField>("achievement-description");
        rewardField = root.Q<TextField>("achievement-reward");
        storyField = root.Q<TextField>("achievement-story");
        eventField = root.Q<EnumField>("achievement-event");
        imageField = root.Q<TextField>("achievement-image");
        requireField = root.Q<FloatField>("achievement-require");
        rewardTypeField = root.Q<EnumField>("achievement-reward-type");
        rewardValueField = root.Q<FloatField>("achievement-reward-value");
        statusField = root.Q<EnumField>("achievement-reward-status");

        findButton.clicked += OnClickFind;
        commitButton.clicked += OnClickCommit;
        deleteButton.clicked += OnClickDelete;

        rewardTypeField.RegisterValueChangedCallback((evt)=>{
            if((AchievementRewardType)evt.newValue == AchievementRewardType.Enhance)
            {
                statusField.style.display = DisplayStyle.Flex;
            }
            else
            {
                statusField.style.display = DisplayStyle.None;
            }
        });
    }

    private void OnEnable() 
    {
        if(achievementList == null)
        {
            achievementList = JSONParser.ReadJSON<AchievementList>($"{Application.streamingAssetsPath}/Achievement.json");
        }
    }

    private void OnDisable() 
    {
        findButton.clicked -= OnClickFind;
        commitButton.clicked -= OnClickCommit;
        deleteButton.clicked -= OnClickDelete;
    }

    void OnClickFind()
    {
        var achievements = achievementList.achievements;
        var foundAchievement = achievements.Find((Achievement achievement) => {
            return idField.value == achievement.id;
        });
        
        if(foundAchievement != null)
        {
            //ID기반으로 찾은 업적을 에디터에 복사
            titleField.value = foundAchievement.title;
            descriptionField.value = foundAchievement.description;
            rewardField.value = foundAchievement.reward;
            storyField.value = foundAchievement.story;
            eventField.value = foundAchievement.eventType;
            imageField.value = foundAchievement.imagePath;
            requireField.value = foundAchievement.requireValue;
            rewardTypeField.value = foundAchievement.rewardType;
            rewardValueField.value = foundAchievement.rewardValue;
            statusField.value = foundAchievement.statusType;
        }
        else
        {
            //ID가 존재하지 않으면 출력(입력 가능하다는 의미)
            Debug.LogWarning($"ID: {idField.value} do not exist");
        }
    }

    void OnClickCommit()
    {
        var achievements = achievementList.achievements;
    
        if(achievements.FindIndex((Achievement achievement)=>{return idField.value == achievement.id;}) >= 0)
        {
            //이미 ID가 존재하는 경우 입력 불가능
            Debug.LogAssertion($"ID: {idField.value} already exist");
        }
        else
        {
            //업적 초기화
            var saveTarget = new Achievement();
            saveTarget.id = idField.value;
            saveTarget.title = titleField.value;
            saveTarget.description = descriptionField.value;
            saveTarget.reward = rewardField.value;
            saveTarget.story = storyField.value;
            saveTarget.eventType = (AchievementEvent)eventField.value;
            saveTarget.imagePath = imageField.value.Length == 0 ? idField.value.ToString() : imageField.value;
            saveTarget.requireValue = requireField.value;
            saveTarget.rewardType = (AchievementRewardType)rewardTypeField.value;
            saveTarget.rewardValue = rewardValueField.value;
            saveTarget.statusType = (StatusType)statusField.value;

            //업적 저장
            achievements.Add(saveTarget);
            achievements.Sort((Achievement a, Achievement b)=>{return a.id - b.id;});
            JSONParser.SaveJSON<AchievementList>($"{Application.streamingAssetsPath}/Achievement.json", achievementList);

            Debug.Log("Save Success!!");
        }
    }

    void OnClickDelete()
    {
        //ID를 기반으로 제거
        int deleted = achievementList.achievements.RemoveAll((Achievement achievement)=>{
            return achievement.id == idField.value;
        });
        if(deleted > 0)
        {
            Debug.LogWarning($"ID: {idField.value} is deleted");
            JSONParser.SaveJSON<AchievementList>($"{Application.streamingAssetsPath}/Achievement.json", achievementList);
        }
        else
        {
            Debug.Log($"ID: {idField.value} already not exist");
        }
    }
}