using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe feita para desenhar a esfera para debug
public class DebugDrawSphere : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);
    }
}
