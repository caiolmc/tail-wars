using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe feita para desenhar o raio para debug
public class DebugDrawLine : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 50, Color.blue);
        
    }
}
