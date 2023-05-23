using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public AI.BehaviorTree.Node node;
    public Port input;
    public Port output;

    public NodeView(AI.BehaviorTree.Node node) : base("Assets/AI/View/NodeView.uxml")
    {
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.guid;

        style.left = node.postion.x;
        style.top = node.postion.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();

        Label descriptionLabel = this.Q<Label>("description");
        descriptionLabel.bindingPath = "description";
        descriptionLabel.Bind(new SerializedObject(node));
    }

    //UI 출력에 차이를 주기 위해 분류
    void SetupClasses()
    {
        if(node is ActionNode)
        {
            AddToClassList("action");
        }
        else if(node is CompositeNode)
        {
            AddToClassList("composite");
        }
        else if(node is DecoratorNode)
        {
            AddToClassList("decorator");
        }
        else if(node is RootNode)
        {
            AddToClassList("root");
        }
    }

    ///<summary>
    ///노드의 입력 포트 생성
    ///</summary>
    private void CreateInputPorts()
    {
        if(node is ActionNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if(node is CompositeNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if(node is DecoratorNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if(node is RootNode)
        {
            //No Input
        }

        if(input != null)
        {
            input.portName = "";
            input.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(input);
        }
    }

    ///<summary>
    ///노드의 출력 포트 생성
    ///</summary>
    private void CreateOutputPorts()
    {
        if(node is ActionNode)
        {
            //No Output
        }
        else if(node is CompositeNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if(node is DecoratorNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }
        else if(node is RootNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }

        if(output != null)
        {
            output.portName = "";
            output.style.width = 100;
            output.style.flexDirection = FlexDirection.ColumnReverse;
            outputContainer.Add(output);
        }
    }

    ///<summary>
    ///노드의 좌표 기록. 다음에 열었을 때 기존에 있던 위치에 다시 그리기 위함.
    ///</summary>
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(node, "Behavior Tree (Set Position)");
        node.postion.x = newPos.x;
        node.postion.y = newPos.y;
        EditorUtility.SetDirty(node); //변경 사항 저장을 위한 더티 설정. Unity는 기본적으로 더티 상태가 아닌 오브젝트를 디스크에 저장하지 않는다.
    }
    
    public override void OnSelected()
    {
        base.OnSelected();
        
        if(OnNodeSelected != null)
        {
            OnNodeSelected.Invoke(this);
        }
    }

    public void SortChildren()
    {
        CompositeNode composite = node as CompositeNode;
        if(composite)
        {
            composite.children.Sort((AI.BehaviorTree.Node a, AI.BehaviorTree.Node b)=>{
                return a.postion.x < b.postion.x ? -1 : 1;
            });
        }
    }

    //런타임에서 상태 시각화
    public void UpdateState()
    {
        RemoveFromClassList("running");
        RemoveFromClassList("failure");
        RemoveFromClassList("success");

        if (Application.isPlaying)
        {
            switch (node.state)
            {
                case AI.BehaviorTree.Node.State.Running:
                    if(node.started)
                    {
                        AddToClassList("running");
                    }
                    break;
                case AI.BehaviorTree.Node.State.Failure:
                    AddToClassList("failure");
                    break;
                case AI.BehaviorTree.Node.State.Success:
                    AddToClassList("success");
                    break;
            }
        }
    }
}
