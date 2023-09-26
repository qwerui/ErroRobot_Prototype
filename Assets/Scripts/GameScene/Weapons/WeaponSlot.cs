using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public TMP_Text bulletText;
    public Image itemIcon;
    public Image bulletPercentage;
    public Image activateCover;
    [HideInInspector]
    public int weaponId;
    BaseWeapon weapon;

    public void Activate()
    {
        activateCover.enabled = false;
    }

    public void Deactivate()
    {
        activateCover.enabled = true;
    }

    public bool IsBlank() => weapon == null;
    public BaseWeapon GetWeapon() => weapon;

    public void SetWeapon(WeaponMapper weapon)
    {
        itemIcon.sprite = weapon.icon;
        this.weapon = Instantiate(weapon.weapon);
        weaponId = weapon.id;
        this.weapon.nowBulletCount = this.weapon.maxBulletCount;

        this.weapon.OnFire += UpdateBullet;

        bulletText.enabled = true;
        itemIcon.enabled = true;
        bulletPercentage.enabled = true;

        UpdateBullet();
    }

    public void UpdateBullet()
    {
        bulletText.SetText(weapon.nowBulletCount.ToString());
        bulletPercentage.fillAmount = weapon.nowBulletCount / (float)weapon.maxBulletCount;
    }
}
