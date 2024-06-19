using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObjects : MonoBehaviour
{
    Material[] mats;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat("_CutoutSize", 0f);
        }
    }
}
