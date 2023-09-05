using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StairReverse))]
public class StairReverseDrawer: PropertyDrawer 
{
    //property 매개변수 수정 시 원본도 같이 변경된다
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
    {
        Rect xRect = new Rect(position.x + 155, position.y, 30, position.height);
        Rect yRect = new Rect(position.x + 190, position.y, 30, position.height);
        
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.LabelField(position, "Reverse");
        
        EditorGUI.BeginChangeCheck();
        bool isOnX = EditorGUI.ToggleLeft(xRect, "X Reverse", (property.enumValueFlag & (int)StairReverse.ReverseX)>0);
        if(EditorGUI.EndChangeCheck())
        {
            if(isOnX)
            {
                property.enumValueFlag = property.enumValueFlag + (int)StairReverse.ReverseX;
            }
            else
            {
                property.enumValueFlag = property.enumValueFlag - (int)StairReverse.ReverseX;
            }
        }

        EditorGUI.BeginChangeCheck();
        bool isOnY = EditorGUI.ToggleLeft(yRect, "Y Reverse", (property.enumValueFlag & (int)StairReverse.ReverseY)>0);
        if(EditorGUI.EndChangeCheck())
        {
            if(isOnY)
            {
                property.enumValueFlag = property.enumValueFlag + (int)StairReverse.ReverseY;
            }
            else
            {
                property.enumValueFlag = property.enumValueFlag - (int)StairReverse.ReverseY;
            }
        }
        EditorGUI.EndProperty();
    }
}