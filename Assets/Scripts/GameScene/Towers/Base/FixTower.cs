using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTower : Tower
{
    protected FixTowerInfo FixTowerInfo
    {
        set{towerInfo = value;}
        get{return towerInfo as FixTowerInfo;}
    }
}
