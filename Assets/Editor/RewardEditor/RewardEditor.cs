using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using System;
using System.Collections.Generic;


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
    ListView enhanceList;

    VisualElement rewardTower;
    IntegerField towerId;

    VisualElement rewardEnhance;
    IntegerField targetId;
    EnumField enhanceType;
    FloatField enhanceValue;

    Button confirmButton;
    Button deleteButton;

    readonly string rewardPathBase = $"{Application.streamingAssetsPath}/Rewards";
    List<int> enhanceArray = new List<int>(10);

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
        enhanceList = root.Q<ListView>("reward-weapon-enhance");

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

        enhanceList.itemsSource = new int[0];
        enhanceList.makeItem = () => new IntegerField();
        enhanceList.bindItem = (element, i) => {
            if(enhanceArray.Count <= i)
            {
                enhanceArray.Add(0);
            }
            ((IntegerField)element).value = enhanceArray[i];
            ((IntegerField)element).RegisterValueChangedCallback((value)=>{
                enhanceArray[i] = value.newValue;
            });
        };

    }

    private void OnDisable() 
    {
        findButton.clicked -= OnClick_Find;
        confirmButton.clicked -= OnClick_Confirm;
        deleteButton.clicked -= OnClick_Delete;
    }

    void OnClick_Find()
    {
        var found = JSONParser.ReadJSON<Reward>($"{rewardPathBase}/{id.value}.json");
        if(found != null)
        {
            title.value = found.title;
            description.value = found.description;
            icon.value = found.imagePath;
            rarity.value = found.rarity;
            pickableCount.value = found.pickableCount;
            initialReward.value = found.isUnlocked;
            rewardType.value = found.type;

            if(found.type == RewardType.Status)
            {
                var converted = JSONParser.ReadJSON<StatusReward>($"{rewardPathBase}/{id.value}.json");
                statusType.value = converted.statusType;
                statusValue.value = converted.value;
            }
            else if(found.type == RewardType.Tower)
            {
                var converted = JSONParser.ReadJSON<TowerReward>($"{rewardPathBase}/{id.value}.json");
                towerId.value = converted.towerId;
            }
            else if(found.type == RewardType.Weapon)
            {
                var converted = JSONParser.ReadJSON<WeaponReward>($"{rewardPathBase}/{id.value}.json");
                weaponId.value = converted.weaponId;
            }
            else if(found.type == RewardType.Enhance)
            {
                var converted = JSONParser.ReadJSON<WeaponEnhanceReward>($"{rewardPathBase}/{id.value}.json");
                targetId.value = converted.targetId;
                enhanceType.value = converted.enhanceType;
                enhanceValue.value = converted.enhanceValue;
            }
            else
            {
                Debug.LogAssertion("Invalid Reward Type");
            }
        }
    }

    void OnClick_Confirm()
    {
        if(File.Exists($"{rewardPathBase}/{id.value}.json"))
        {
            //이미 ID가 존재하는 경우 입력 불가능
            Debug.LogAssertion($"ID: {id.value} already exist");
        }
        else
        {
            try
            {
                switch(rewardType.value)
                {
                    case RewardType.Status:
                        JSONParser.SaveJSON($"{rewardPathBase}/{id.value}.json", CreateStatusReward());
                    break;
                    case RewardType.Tower:
                        JSONParser.SaveJSON($"{rewardPathBase}/{id.value}.json", CreateTowerReward());
                    break;
                    case RewardType.Weapon:
                        JSONParser.SaveJSON($"{rewardPathBase}/{id.value}.json", CreateWeaponReward());
                    break;
                    case RewardType.Enhance:
                        JSONParser.SaveJSON($"{rewardPathBase}/{id.value}.json", CreateEnhanceReward());           
                    break;
                    default:
                        Debug.LogAssertion("Invalid Reward Type");
                    return;
                }

                Debug.Log("Save Success");
            }
            catch
            {
                Debug.LogAssertion("Save Failed");
            }
        }
    }

    void OnClick_Delete()
    {
        if(File.Exists($"{rewardPathBase}/{id.value}.json"))
        {
            File.Delete($"{rewardPathBase}/{id.value}.json");
            Debug.LogWarning($"ID: {id.value} is deleted");
        }
        else
        {
            Debug.Log($"ID: {id.value} already not exist");
        }
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

    StatusReward CreateStatusReward()
    {
        var reward = new StatusReward
        {
            statusType = (StatusType)statusType.value,
            value = statusValue.value
        };
        InitCommon(reward);
        return reward;
    }

    TowerReward CreateTowerReward()
    {
        var reward = new TowerReward
        {
            towerId = towerId.value
        };
        InitCommon(reward);
        return reward;
    }

    WeaponReward CreateWeaponReward()
    {
        int[] enhanceListId = new int[enhanceList.itemsSource.Count];

        for(int i=0;i<enhanceListId.Length;i++)
        {
            enhanceListId[i] = enhanceArray[i];
        }

        var reward = new WeaponReward
        {
            weaponId = weaponId.value,
            enhanceListId = enhanceListId
        };
        InitCommon(reward);
        return reward;
    }

    WeaponEnhanceReward CreateEnhanceReward()
    {
        var reward = new WeaponEnhanceReward
        {
            targetId = targetId.value,
            enhanceType = (EnhanceType)enhanceType.value,
            enhanceValue = enhanceValue.value
        };
        InitCommon(reward);
        return reward;
    }

    void InitCommon(Reward reward)
    {
        reward.id = id.value;
        reward.title = title.value;
        reward.description = description.value;
        reward.imagePath = icon.value;
        reward.rarity = rarity.value;
        reward.pickableCount = pickableCount.value;
        reward.currentPickable = pickableCount.value;
        reward.isUnlocked = initialReward.value;
    }
}