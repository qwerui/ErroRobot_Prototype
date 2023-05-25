using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using AI.BehaviorTree;
using System.Linq;

public class BehaviorTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits>{}

    public System.Action<NodeView> OnNodeSelected;
    BehaviorTree tree;

    public BehaviorTreeView()
    {
        Insert(0, new GridBackground()); //그리드 형태 배경

        this.AddManipulator(new ContentZoomer()); //확대 축소
        this.AddManipulator(new ContentDragger()); //드래그로 위치 이동 (휠 버튼 사용)
        this.AddManipulator(new SelectionDragger()); //드래그로 선택한 오브젝트 이동
        this.AddManipulator(new RectangleSelector()); //사각형 영역 내 전부 선택

        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/AI/View/BehaviorTreeEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    //되돌리기
    void OnUndoRedo()
    {
        PopulateView(tree);
        AssetDatabase.SaveAssets();
    }

    NodeView FindNodeView(AI.BehaviorTree.Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    ///<summary>
    ///트리 화면 갱신
    ///</summary>
    internal void PopulateView(BehaviorTree tree)
    {
        this.tree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if(tree.rootNode == null) //루트 노드는 필수다. 없으면 만듦
        {
            tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(tree); //변경 사항 저장을 위한 더티 설정. Unity는 기본적으로 더티 상태가 아닌 오브젝트를 디스크에 저장하지 않는다.
            AssetDatabase.SaveAssets();
        }

        //노드 뷰 생성
        tree.nodes.ForEach(n => CreateNodeView(n));

        //노드 연결선 생성
        tree.nodes.ForEach(n =>  {
            var children = tree.GetChildren(n);
            children.ForEach(c => {
                NodeView parentView = FindNodeView(n);
                NodeView childView = FindNodeView(c);

                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        });
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        //노드 연결 조건 체크, 지우면 노드가 연결이 안된다.
        //포트와 노드가 서로 다르면 연결 가능
        return ports.ToList().Where(endPort => 
        endPort.direction != startPort.direction &&
        endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        //노드 제거
        if(graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>{
                NodeView nodeView = elem as NodeView;
                if(nodeView != null)
                {
                    tree.DeleteNode(nodeView.node);
                }

                Edge edge = elem as Edge;

                if(edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.RemoveChild(parentView.node, childView.node);
                }
            });
        }

        //노드 연결선 추가
        if(graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge => {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                tree.AddChild(parentView.node, childView.node);
            });
        }

        //노드 이동
        if(graphViewChange.movedElements != null)
        {
            nodes.ForEach((n) => {
                NodeView view = n as NodeView;
                view.SortChildren(); //Composite 노드 정렬
            });
        }

        return graphViewChange;
    }
    
    //우클릭 시 나오는 드롭다운에 노드 목록 표시
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        
        {
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach(var type in types)
            {
                evt.menu.AppendAction($"Action/[{type.BaseType.Name}] {type.Name}", (a)=>CreateNode(type));
            }
        }

        {
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach(var type in types)
            {
                evt.menu.AppendAction($"Composite/[{type.BaseType.Name}] {type.Name}", (a)=>CreateNode(type));
            }
        }

        {
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach(var type in types)
            {
                evt.menu.AppendAction($"Decorator/[{type.BaseType.Name}] {type.Name}", (a)=>CreateNode(type));
            }
        }
    }

    ///<summary>
    ///노드 생성
    ///</summary>
    void CreateNode(System.Type type)
    {
        //널 체크, 없으면 오류 발생 가능성 있음.
        UnityEngine.Assertions.Assert.IsNotNull(tree, "Null tree!!");
        AI.BehaviorTree.Node node = tree.CreateNode(type);
        CreateNodeView(node);
    }

    ///<summary>
    ///노드 뷰 생성
    ///</summary>
    void CreateNodeView(AI.BehaviorTree.Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    public void UpdateNodeState()
    {
        nodes.ForEach(n=>{
            NodeView view = n as NodeView;
            view.UpdateState();
        });
    }
}
