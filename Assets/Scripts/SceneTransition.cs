using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public GameObject transition;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Transition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Transition()
    {
        //Time.timeScale = 0f;



        yield return new WaitForSeconds(2f);

        transition.SetActive(false);

        //Time.timeScale = 1f;

        yield return null;
    }
}
