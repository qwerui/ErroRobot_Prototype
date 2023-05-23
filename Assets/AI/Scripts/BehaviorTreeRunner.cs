using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviorTree tree;

    void Start()
    {
        tree = tree.Clone();
        tree.Bind();
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
