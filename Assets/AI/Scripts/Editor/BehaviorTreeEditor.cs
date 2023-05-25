using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using AI.BehaviorTree;
using System;
using UnityEditor.Callbacks;

public class BehaviorTreeEditor : EditorWindow
{
    BehaviorTreeView treeView;
    InspectorView inspectorView;
    IMGUIContainer blackboardView;

    SerializedObject treeObject;
    SerializedProperty blackboardProperty;

    

    [MenuItem("Window/AI/BehaviorTree")]
    public static void OpenWindow()
    {
        BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviorTreeEditor");
    }

    //Behavior Tree 열면 에디터 활성화
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if(Selection.activeObject is BehaviorTree)
        {
            OpenWindow();
            return true;
        }
        return false;
    }

    public void CreateGUI()
    {       
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/AI/View/BehaviorTreeEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/AI/View/BehaviorTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        //하위 UI 찾기
        treeView =  root.Q<BehaviorTreeView>();
        inspectorView = root.Q<InspectorView>();
        blackboardView = root.Q<IMGUIContainer>();

        //Blackboard 출력
        if (treeObject != null)
        {
            blackboardView.onGUIHandler = () =>
            {
                treeObject.Update();
                EditorGUILayout.PropertyField(blackboardProperty, true);
                treeObject.ApplyModifiedProperties();
            };
        }
        treeView.OnNodeSelected = OnNodeSelectionChanged;

        OnSelectionChange();
    }

    private void OnEnable() 
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable() 
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange change)
    {
        switch(change)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
            break;
            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
            break;
        }
    }

    //노드 선택 시 호출
    private void OnNodeSelectionChanged(NodeView node)
    {
        inspectorView.UpdateSelection(node);
    }

    private void OnSelectionChange() 
    {
        //에셋 창에서 선택된 Behavior Tree를 표시한다.
        BehaviorTree tree = Selection.activeObject as BehaviorTree;
        if(!tree)
        {
            if(Selection.activeGameObject)
            {
                BehaviorTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviorTreeRunner>();
                if(runner)
                {
                    tree = runner.tree;
                }
            }
        }

        if(Application.isPlaying)
        {
            if(tree) //런타임 업데이트
            {
                treeView.PopulateView(tree);
            }
        }
        else
        {
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                treeView.PopulateView(tree);
            }
        }

        if(tree != null)
        {
            treeObject = new SerializedObject(tree);
            blackboardProperty = treeObject.FindProperty("blackboard");
        }
    }

    private void OnInspectorUpdate() 
    {
        treeView?.UpdateNodeState();
    }
}