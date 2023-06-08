using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public BuildController buildController;
    public DefenceController defenceController;

    void Start()
    {
        buildController.Init();
        defenceController.Init();
        buildController.gameObject.SetActive(true); 
    }
}
