using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextNotifier : MonoBehaviour
{
    public TMP_Text text;
    
    public void Activate(string text)
    {
        this.text.SetText(text);
        gameObject.SetActive(true);
    }

    public void Deactivate() => gameObject.SetActive(false);
}
