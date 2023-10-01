using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, IGameUI
{
    public WeaponSlot slotPrefab;
    public Transform slotTarget;
    public WeaponController weaponController;
    List<WeaponSlot> slotList = new List<WeaponSlot>();
    GameUIController gameUIController;
    WeaponMapper tempForAlter;
    public RewardManager rewardManager;
    public TextNotifier textNotifier;

    int index = 0;

    public int GetSlotCount() => slotList.Count;

    private void Awake() 
    {
        gameUIController = GetComponent<GameUIController>();
    }

    public void CreateSlot()
    {
        slotList.Add(Instantiate(slotPrefab, slotTarget));
    }

    public void CreateSlot(int amount)
    {
        for(int i=slotTarget.childCount; i<amount; i++)
        {
            slotList.Add(Instantiate(slotPrefab, slotTarget));
        }
    }

    public void SetWeapon(WeaponMapper weapon)
    {
        for(int i = 0; i < slotList.Count; i++)
        {
            var slot = slotList[i];
            if(slot.IsBlank())
            {
                slotList[index].Deactivate();
                slot.SetWeapon(weapon);
                index = i;
                weaponController.CurrentWeapon = slotList[index].GetWeapon();
                slotList[index].Activate();
                return;
            }
        }

        textNotifier.Activate("변경할 무기를 선택해주세요");
        tempForAlter = weapon;
        gameUIController.enabled = true;
    }

    public void ChangeWeapon(Vector2 direction)
    {
        OnNavigate(direction);
        weaponController.CurrentWeapon = slotList[index].GetWeapon();
    }

    public void LoadWeapon(List<SerializedWeapon> weapons)
    {
        int loadIndex = 0;

        foreach(SerializedWeapon weapon in weapons)
        {
            var mapper = Resources.Load<WeaponMapper>($"Reward/Weapon/{weapon.id}");
            slotList[loadIndex].SetWeapon(mapper);
            var instantiated = slotList[loadIndex].GetWeapon();
            foreach(int enhanceId in weapon.enhance)
            {
                var reward = JSONParser.ReadJSON<WeaponEnhanceReward>($"{Application.streamingAssetsPath}/Rewards/{enhanceId}.json");
                instantiated.Enhance(reward.enhanceType, reward.enhanceValue);
            }
            loadIndex++;
        }

        slotList[0].Activate();
        weaponController.CurrentWeapon = slotList[0].GetWeapon();
    }

    public List<SerializedWeapon> GetSerializedWeapons()
    {
        var weapons = new List<SerializedWeapon>();
        foreach (WeaponSlot slot in slotList)
        {
            if (!slot.IsBlank())
            {
                SerializedWeapon temp = new SerializedWeapon
                {
                    id = slot.weaponId,
                    enhance = slot.GetWeapon().enhanceIdList
                };
                weapons.Add(temp);
            }
        }
        return weapons;
    }

    public void EnhanceWeapon(WeaponEnhanceReward reward)
    {
        foreach(WeaponSlot slot in slotList)
        {
            if(slot.weaponId == reward.targetId)
            {
                //강화 코드
                var weapon = slot.GetWeapon();
                weapon.enhanceIdList.Add(reward.id);
                weapon.Enhance(reward.enhanceType, reward.enhanceValue);
                break;
            }
        }
    }

#region AlterWeapon

    public void OnNavigate(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) < Mathf.Epsilon)
        {
            return;
        }

        slotList[index].Deactivate();
        index += direction.x > 0 ? 1 : -1;
        index = Mathf.Clamp(index, 0, slotList.Count-1);
        slotList[index].Activate();
    }

    public void OnSubmit()
    {
        rewardManager.RemoveEnhance(slotList[index].weaponId);
        Destroy(slotList[index].GetWeapon().gameObject);
        slotList[index].SetWeapon(tempForAlter);
        weaponController.CurrentWeapon = slotList[index].GetWeapon();
        textNotifier.Deactivate();
        tempForAlter = null;
        gameUIController.enabled = false;
    }

    public void OnCancel()
    {
        //반드시 무기를 교체해야 함
    }

#endregion

}
