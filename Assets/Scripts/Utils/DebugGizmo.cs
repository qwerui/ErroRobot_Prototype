using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmo : MonoBehaviour
{
    public Color color;
    public float radius;
    
    private void OnDrawGizmos() 
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
