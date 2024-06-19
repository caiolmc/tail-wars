using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutout : MonoBehaviour
{
    public GameObject target;

    public LayerMask wall;

    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = cam.WorldToViewportPoint(target.transform.position);
        targetPos.y /= (Screen.width / Screen.height);
        Vector3 offset = target.transform.position - transform.position;
        //Debug.DrawRay(transform.position, offset, Color.red);
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wall);
        float cutoutSize = 3/offset.magnitude;

        for (int i = 0; i < hitObjects.Length; i++)
        {
            Material[] mats = hitObjects[i].transform.GetComponent<Renderer>().materials;
            for (int j = 0; j < mats.Length; j++)
            {
                mats[j].SetVector("_CutoutPos", targetPos);
                mats[j].SetFloat("_CutoutSize", cutoutSize);
                mats[j].SetFloat("_FallOffSize", cutoutSize / 2);
            }

        }
    }
}
