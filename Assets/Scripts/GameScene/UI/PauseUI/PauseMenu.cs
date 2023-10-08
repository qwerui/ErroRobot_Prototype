using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일시정지 관련
/// </summary>
public class PauseMenu : MonoBehaviour, IGameUI
{
    PauseMenuContent[] pauseMenuContents;
    GameUIController gameUIController;
    int index;

    private void OnEnable() 
    {
        if(pauseMenuContents == null)
        {
            pauseMenuContents = GetComponentsInChildren<PauseMenuContent>();
            System.Array.Sort(pauseMenuContents, (PauseMenuContent a, PauseMenuContent b)=>a.transform.GetSiblingIndex() - b.transform.GetSiblingIndex());
            gameUIController = GetComponent<GameUIController>();
        }
        
        Time.timeScale = 0.0f;
        index = 0;
        pauseMenuContents[index].Activate();
    }

    public void OnNavigate(Vector2 direction)
    {
        pauseMenuContents[index].Deactivate();

        if(Mathf.Abs(direction.y) < Mathf.Epsilon)
            return;

        if(direction.y > 0)
        {
            index--;
        }
        else
        {
            index++;
        }

        index = Mathf.Clamp(index, 0, pauseMenuContents.Length-1);

        pauseMenuContents[index].Activate();
    }

    public void OnSubmit()
    {
        pauseMenuContents[index].Execute();
    }

    public void OnCancel()
    {
        pauseMenuContents[0].Execute();
    }
}