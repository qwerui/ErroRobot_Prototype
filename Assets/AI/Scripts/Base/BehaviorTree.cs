using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "BehaivorTree", menuName = "AI/BehaivorTree", order = 0)]
    public class BehaviorTree : ScriptableObject
    {
        public Node rootNode;
        public Node.State treeState = Node.State.Running;
        public bool repeat = false;
        public List<Node> nodes = new List<Node>();
        public Blackboard blackboard;
        [HideInInspector] public GameObject self;

        public Node.State Update()
        {
            if (rootNode.state == Node.State.Running)
            {
                treeState = rootNode.Update();
            }
            else if(repeat)
            {
                treeState = Node.State.Running;
            }
            return treeState;
        }

#if UNITY_EDITOR
        public Node CreateNode(System.Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();

            Undo.RecordObject(this, "Behavior Tree (Create Node)");

            nodes.Add(node);

            if(!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            Undo.RegisterCreatedObjectUndo(node, "Behavior Tree (Create Node)");
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "Behavior Tree (Delete Node)");
            nodes.Remove(node);
            //AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            DecoratorNode decorator =  parent as DecoratorNode;

            if(decorator)
            {
                Undo.RecordObject(decorator, "Behavior Tree (Add Child)");
                decorator.child = child;
                EditorUtility.SetDirty(decorator);
            }

            RootNode rootNode = parent as RootNode;

            if(rootNode)
            {
                Undo.RecordObject(rootNode, "Behavior Tree (Add Child)");
                rootNode.child = child;
                EditorUtility.SetDirty(rootNode);
            }

            CompositeNode composite = parent as CompositeNode;

            if(composite)
            {
                Undo.RecordObject(composite, "Behavior Tree (Add Child)");
                composite.children.Add(child);
                EditorUtility.SetDirty(composite);
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            DecoratorNode decorator =  parent as DecoratorNode;

            if(decorator)
            {
                Undo.RecordObject(decorator, "Behavior Tree (Remove Child)");
                decorator.child = null;
                EditorUtility.SetDirty(decorator);
            }

            RootNode rootNode = parent as RootNode;

            if(rootNode)
            {
                Undo.RecordObject(rootNode, "Behavior Tree (Remove Child)");
                rootNode.child = null;
                EditorUtility.SetDirty(rootNode);
            }

            CompositeNode composite = parent as CompositeNode;

            if(composite)
            {
                Undo.RecordObject(composite, "Behavior Tree (Remove Child)");
                composite.children.Remove(child);
                EditorUtility.SetDirty(composite);
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            DecoratorNode decorator = parent as DecoratorNode;

            if(decorator && decorator.child != null)
            {
                children.Add(decorator.child);
            }

            RootNode rootNode = parent as RootNode;

            if(rootNode && rootNode.child != null)
            {
                children.Add(rootNode.child);
            }

            CompositeNode composite = parent as CompositeNode;

            if(composite)
            {
                return composite.children;
            }

            return children;
        }
#endif

        public void Traverse(Node node, System.Action<Node> visiter)
        {
            if(node)
            {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach(c=>Traverse(c, visiter));
            }
        }

        ///<summary>
        ///복사본을 생성한다. 원본 훼손 방지를 위해 호출할 것
        ///</summary>
        public BehaviorTree Clone()
        {
            BehaviorTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            tree.nodes = new List<Node>();
            tree.blackboard = blackboard.Clone();
            Traverse(tree.rootNode, n => tree.nodes.Add(n));
            return tree;
        }

        public void Bind(GameObject self)
        {
            Traverse(rootNode, node => {
                node.blackboard = blackboard;
                node.self = self;
            });
        }
    }
}
