using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pointer", menuName = "Infos/PointerAsset", order = 0)]
public class PointerAsset : ScriptableObject 
{
    public PointerIcon type;
    public PointerObject obj;
}