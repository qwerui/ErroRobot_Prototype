using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 파일명은 towerId와 일치 시킬 것
/// </summary>
[CreateAssetMenu(fileName = "TowerMapper", menuName = "Infos/Rewards/TowerMapper", order = 0)]
public class TowerMapper : ScriptableObject 
{
    public Tower tower;
}