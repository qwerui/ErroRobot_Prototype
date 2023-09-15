using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameoverPanel : MonoBehaviour
{
    TMP_Text GameEndTitle;

    public void Gameover()
    {
        GameEndTitle ??= GetComponentInChildren<TMP_Text>(true);
        GameEndTitle.SetText("Game Over");
        GameEndTitle.color = new Color(0.7f, 0.0f, 0.0f, 1.0f);
        gameObject.SetActive(true);
    }

    public void GameClear()
    {
        GameEndTitle ??= GetComponentInChildren<TMP_Text>(true);
        GameEndTitle.SetText("Game Clear");
        GameEndTitle.color = new Color(0.0f, 0.7f, 1.0f, 1.0f);
        gameObject.SetActive(true);
    }
}