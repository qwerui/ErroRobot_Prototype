using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

[CreateAssetMenu(fileName = "TestBlackboard", menuName = "ErroRobot_Prototype/Blackboard/TestBlackboard", order = 0)]
public class TestBlackboard : Blackboard
{
    public int temp1;
    public int temp2;
    public Vector2 temp3;

    public TestBlackboard()
    {
        Set<int>("temp1", temp1);
        Set<int>("temp2", temp2);
        Set<Vector2>("temp3", temp3);
    }
}
