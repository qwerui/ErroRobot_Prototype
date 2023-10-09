using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerDetailContent : MonoBehaviour
{
    public TMP_Text towerDetailTitle;
    public TMP_Text towerDetailValue;

    public void SetText(string towerDetailTitle, string towerDetailValue)
    {
        this.towerDetailTitle.SetText(towerDetailTitle);
        this.towerDetailValue.SetText(towerDetailValue);
    }
}
