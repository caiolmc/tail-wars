using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BocasFogao : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player1"))
        {
            col.attachedRigidbody.AddForce(new Vector3(100, 50, 0), ForceMode.Impulse);
            col.gameObject.GetComponent<Character>().TomarDano(10);
        }
    }
}
