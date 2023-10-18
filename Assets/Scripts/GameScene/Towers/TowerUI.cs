using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUI : MonoBehaviour, IGameUI
{
    Tower tower;
    GameUIController gameUIController;
    public PlayerStatus playerStatus;
    public TMP_Text upgradeText;
    public TowerManager towerManager;
    public AudioClip upgradeClip;
    TowerDetailContent[] towerDetailContents;
    Outline[] outlines;
    int index = 0;

    private void OnEnable() 
    {
        if(gameUIController == null)
        {
            gameUIController = GetComponent<GameUIController>();
            towerDetailContents = GetComponentsInChildren<TowerDetailContent>(true);
            System.Array.Sort(towerDetailContents, (TowerDetailContent a, TowerDetailContent b)=>{
                return a.transform.GetSiblingIndex() - b.transform.GetSiblingIndex();
            });
            outlines = GetComponentsInChildren<Outline>(true);
            System.Array.Sort(outlines, (Outline a, Outline b)=>{
                return a.transform.GetSiblingIndex() - b.transform.GetSiblingIndex();
            });
        }
        gameUIController.enabled = true;
        index = 0;
        outlines[0].enabled = true;

        UpdateTowerDetail();
    }

    private void OnDisable() 
    {
        gameUIController.enabled = false;
        foreach(TowerDetailContent towerDetailContent in towerDetailContents)
        {
            towerDetailContent.gameObject.SetActive(false);
        }
        foreach(Outline outline in outlines)
        {
            outline.enabled = false;
        }
    }

    public void SetTower(Tower newTower)
    {
        tower = newTower;
    }

    void UpdateTowerDetail()
    {
        if (tower.IsMaxLevel()) //최대 레벨
        {
            upgradeText.color = Color.black;
            upgradeText.SetText("-");

            //타워 정보 업데이트
            var towerDetails = tower.GetTowerDetail(tower.Level);

            if (towerDetails != null)
            {
                int detailIndex = 0;
                foreach (KeyValuePair<string, string> detail in towerDetails)
                {
                    towerDetailContents[detailIndex].SetText(detail.Key, detail.Value);
                    towerDetailContents[detailIndex].gameObject.SetActive(true);
                    detailIndex++;
                }
            }
        }
        else
        {
            if (playerStatus.Core < tower.UpgradeCore)
            {
                upgradeText.color = Color.red;
            }
            else
            {
                upgradeText.color = Color.black;
            }
            upgradeText.SetText(tower.UpgradeCore.ToString());

            //타워 정보 업데이트
            var towerDetails = tower.GetTowerDetail(tower.Level);
            var nextLevelDetail = tower.GetTowerDetail(tower.Level+1);

            if (towerDetails != null)
            {
                int detailIndex = 0;
                foreach (KeyValuePair<string, string> detail in towerDetails)
                {   
                    float towerValue = float.Parse(nextLevelDetail[detail.Key]) - float.Parse(detail.Value);
                    string valueString = $"{detail.Value} ({(towerValue > 0 ? "+" : "-")}{Mathf.Abs(towerValue)})";
                    towerDetailContents[detailIndex].SetText(detail.Key, valueString);
                    towerDetailContents[detailIndex].gameObject.SetActive(true);
                    detailIndex++;
                }
            }
        }
    }


    public void OnCancel()
    {
        gameObject.SetActive(false);
    }

    public void OnNavigate(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) < Mathf.Epsilon)
        {
            return;
        }
        
        outlines[index].enabled = false;
        index += direction.x > 0 ? 1 : -1;
        index = Mathf.Clamp(index, 0, 2);
        outlines[index].enabled = true;
    }

    public void OnSubmit()
    {
        switch(index)
        {
            case 0: //이동
                OnCancel();
                towerManager.MoveTower(tower);
            break;
            case 1: //업그레이드
                if(playerStatus.Core >= tower.UpgradeCore && !tower.IsMaxLevel())
                {
                    playerStatus.Core -= tower.UpgradeCore;
                    SoundQueue.instance.PlaySFX(upgradeClip);
                    tower.Upgrade();
                    UpdateTowerDetail();
                }
            break;
            case 2: //닫기
                OnCancel();
            break;
            default:
            break;
        }
    }

    public void OnSubmit_Mouse(int index)
    {
        this.index = index;
        OnSubmit();
    }
}
