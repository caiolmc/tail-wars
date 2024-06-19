using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avisotrigger : MonoBehaviour
{
    public RawImage warning;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player1")
        {
            warning.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player1")
        {
            warning.gameObject.SetActive(false);
        }
    }

}
