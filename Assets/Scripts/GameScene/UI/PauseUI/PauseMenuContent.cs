using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuContent : MonoBehaviour
{
    Image selected;

    private void Awake() 
    {
        selected = transform.GetChild(0).GetComponent<Image>();
    }

    public void Activate()
    {
        selected.enabled = true;
    }

    public void Deactivate()
    {
        selected.enabled = false;
    }

    public virtual void Execute()
    {

    }
}
