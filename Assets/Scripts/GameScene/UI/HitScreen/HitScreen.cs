using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HitScreen : MonoBehaviour
{
    [SerializeField]
    bool yMode;
    [SerializeField]
    Color color;

    Image image;

    private void Awake() 
    {
        var image = GetComponent<Image>();
        if(image != null)
        {
            this.image = image;
            color.a = 0;
            image.enabled = true;
            image.material = Instantiate(image.material);
            image.material.SetColor("_Color", color);
            image.material.SetFloat("_Y_Mode", yMode?1:0);
        }
    }

    public void Show()
    {
        //페이드 아웃
        color.a = 1;
        image.material.SetColor("_Color", color);
        image.material.DOFade(0, 1);
    }
}
