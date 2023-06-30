using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StairReverse
{
    None = 0,
    ReverseX = 2,
    ReverseY = 4,
    ReverseAll = 6
}

///<summary>
///계단형태로 자식을 배치하는 레이아웃
///</summary>
[AddComponentMenu("Layout/Stair Layout Group", 150)]
public class StairLayoutGroup : LayoutGroup
{
    [SerializeField] protected Vector2 m_Spacing = Vector2.zero;
    public Vector2 Spacing 
    { 
        get {return m_Spacing;} 
        set {SetProperty(ref m_Spacing, value);}   
    }

    public StairReverse reverse = StairReverse.ReverseAll;

    public override void CalculateLayoutInputVertical()
    {
        
    }

    //가로 정렬
    public override void SetLayoutHorizontal()
    {
        float xOffset = (reverse & StairReverse.ReverseX) > 0 ? -Spacing.x : Spacing.x;
        for(int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform childRect = rectChildren[i];
            SetChildAlongAxis(childRect, 0, i * xOffset);
        }
    }

    //세로 정렬
    public override void SetLayoutVertical()
    {
        for(int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform childRect = rectChildren[i];
            if((reverse & StairReverse.ReverseY) > 0)
            {
                SetChildAlongAxis(childRect, 1, -i*(childRect.sizeDelta.y+Spacing.y));
            }
            else
            {
                SetChildAlongAxis(childRect, 1, i*(childRect.sizeDelta.y+Spacing.y));
            }
        }
    }
}