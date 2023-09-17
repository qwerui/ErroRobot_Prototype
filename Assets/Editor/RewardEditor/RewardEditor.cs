using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public class RewardEditor : EditorWindow
{
    IntegerField id;
    Button findButton;
    new TextField title;
    TextField description;
    TextField icon;
    SliderInt rarity;
    IntegerField pickableCount;
    Toggle initialReward;
    EnumField rewardType;

    VisualElement rewardStatus;
    EnumField statusType;
    FloatField statusValue;
    
    VisualElement rewardWeapon;
    IntegerField weaponId;

    VisualElement rewardTower;
    IntegerField towerId;

    VisualElement rewardEnhance;
    IntegerField targetId;
    EnumField enhanceType;
    FloatField enhanceValue;

    Button confirmButton;
    Button deleteButton;
    
    [MenuItem("Window/Editors/RewardEditor")]
    public static void ShowExample()
    {
        RewardEditor wnd = GetWindow<RewardEditor>();
        wnd.titleContent = new GUIContent("RewardEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/RewardEditor/RewardEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        id = root.Q<IntegerField>("reward-id");
        findButton = root.Q<Button>("reward-find");
        title = root.Q<TextField>("reward-title");
        description = root.Q<TextField>("reward-description");
        icon = root.Q<TextField>("reward-sprite");
        rarity = root.Q<SliderInt>("reward-rarity");
        pickableCount = root.Q<IntegerField>("reward-count");
        initialReward = root.Q<Toggle>("reward-initial");
        rewardType = root.Q<EnumField>("reward-type");

        rewardStatus = root.Q<VisualElement>("reward-status");
        statusType = root.Q<EnumField>("reward-status-type");
        statusValue = root.Q<FloatField>("reward-status-value");

        rewardWeapon = root.Q<VisualElement>("reward-weapon");
        weaponId = root.Q<IntegerField>("reward-weapon-id");

        rewardTower = root.Q<VisualElement>("reward-tower");
        towerId = root.Q<IntegerField>("reward-tower-id");

        rewardEnhance = root.Q<VisualElement>("reward-enhance");
        targetId = root.Q<IntegerField>("reward-enhance-target");
        enhanceType = root.Q<EnumField>("reward-enhance-type");
        enhanceValue = root.Q<FloatField>("reward-enhance-value");

        confirmButton = root.Q<Button>("reward-confirm");
        deleteButton = root.Q<Button>("reward-delete");

        findButton.clicked += OnClick_Find;
        confirmButton.clicked += OnClick_Confirm;
        deleteButton.clicked += OnClick_Delete;

        rewardType.RegisterValueChangedCallback((evt)=>{OnChange_Type(evt.newValue);});

    }

    private void OnEnable() 
    {
        
    }

    private void OnDisable() 
    {
        findButton.clicked -= OnClick_Find;
        confirmButton.clicked -= OnClick_Confirm;
        deleteButton.clicked -= OnClick_Delete;
    }

    void OnClick_Find()
    {

    }

    void OnClick_Confirm()
    {

    }

    void OnClick_Delete()
    {

    }

    void OnChange_Type(System.Enum type)
    {
        rewardStatus.style.display = DisplayStyle.None;
        rewardTower.style.display = DisplayStyle.None;
        rewardWeapon.style.display = DisplayStyle.None;
        rewardEnhance.style.display = DisplayStyle.None;

        switch((RewardType)type)
        {
            case RewardType.Status:
                rewardStatus.style.display = DisplayStyle.Flex;
            break;
            case RewardType.Tower:
                rewardTower.style.display = DisplayStyle.Flex;
            break;
            case RewardType.Weapon:
                rewardWeapon.style.display = DisplayStyle.Flex;
            break;
            case RewardType.Enhance:
                rewardEnhance.style.display = DisplayStyle.Flex;
            break;
        }
    }
}