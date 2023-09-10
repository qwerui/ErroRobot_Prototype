using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public enum PointerIcon
{
    Build,
    Rifle
}

public class Pointer : MonoBehaviour
{
    Dictionary<PointerIcon, PointerObject> pointerIcons = new Dictionary<PointerIcon, PointerObject>();

    PointerObject currentPointer;

    public void Activate() => gameObject.SetActive(true);   
    public void Deactivate() => gameObject.SetActive(false);
    
    private void Awake() 
    {
        var pointerAssets = Resources.LoadAll<PointerAsset>("PointerIcons");
        
        foreach(PointerAsset pointerAsset in pointerAssets)
        {
            pointerIcons[pointerAsset.type] = pointerAsset.obj;
        }
    }

    public void SetPointer(PointerIcon icon)
    {
        if(!pointerIcons[icon].isInstantiated)
        {
            pointerIcons[icon] = Instantiate(pointerIcons[icon], transform);
            pointerIcons[icon].isInstantiated = true;
        }
        
        if(currentPointer != null)
        {
            currentPointer.gameObject.SetActive(false);
        }
        currentPointer = pointerIcons[icon];
        currentPointer.gameObject.SetActive(true);
    }

    public void DoAction()
    {
        currentPointer.DoAction();
    }
}

