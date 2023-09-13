using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour, IGameUI
{
    Tower tower;
    GameUIController gameUIController;
    public TowerMoveController towerMoveController;
    TowerDetailContent[] towerDetailContents;
    Outline[] outlines;
    int index = 0;

    private void OnEnable() 
    {
        if(gameUIController == null)
        {
            gameUIController = GetComponent<GameUIController>();
            towerDetailContents = GetComponentsInChildren<TowerDetailContent>();
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
        UpdateTowerDetail();
    }

    void UpdateTowerDetail()
    {
        //타워의 정보를 가져와야할 필요가 있음
        //타워의 정보를 업데이트
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
                tower.ReadyToPut();
                towerMoveController.SetTower(tower);
            break;
            case 1: //업그레이드
                tower.Upgrade();
                UpdateTowerDetail();
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
