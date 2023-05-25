using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits>{}

    Editor editor;

    public InspectorView()
    {

    }

    //선택된 노드의 정보를 Inspector창에 출력
    internal void UpdateSelection(NodeView nodeView)
    {
        Clear();
        
        UnityEngine.Object.DestroyImmediate(editor); //기존 노드 삭제
        editor = Editor.CreateEditor(nodeView.node); //새 노드 할당
        IMGUIContainer container = new IMGUIContainer(() => {
            if(editor.target)
            {
                editor.OnInspectorGUI();
            }
            });
        Add(container); //출력
    }
}
