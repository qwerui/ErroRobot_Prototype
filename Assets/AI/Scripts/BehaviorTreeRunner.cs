using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviorTree tree;

    bool isRunning = false;

    public void Awake()
    {
        tree = tree.Clone();
        tree.Bind(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            tree.Update();
        }
    }

    public void RunTree() => isRunning = true;
    public void StopTree() => isRunning = false;
}
