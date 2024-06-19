using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Espuma : MonoBehaviour
{

    private Vector3 refPoint;

    [SerializeField]
    private Transform exitPoint;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player1"))
        {
            refPoint = col.transform.position;
            StartCoroutine(Flutuar(col));
        }

    }

    IEnumerator Flutuar(Collider obj)
    {

        GameObject bolha;
        //GameObject descongelado;

        Rigidbody objRb = obj.GetComponent<Rigidbody>();

        obj.gameObject.GetComponent<Character>().canMove = false;

        bolha = FindChildWithTag(obj.gameObject, "Bolha");
        //descongelado = FindChildWithTag(obj.gameObject, "Descongelado");

        bolha.SetActive(true);
        //descongelado.SetActive(false);
        obj.gameObject.GetComponent<Character>().catMesh.enabled = false;
        obj.gameObject.GetComponent<Character>().catFaceMesh.enabled = false;
        obj.gameObject.GetComponent<Character>().weaponsObj.SetActive(false);

        //obj.transform.position = new Vector3(exitPoint.position.x,exitPoint.position.y, refPoint.z);

        objRb.isKinematic = true;

        objRb.MovePosition(refPoint + (2f * Time.deltaTime * exitPoint.position));

        while (objRb.position.y < exitPoint.position.y)
        {
            objRb.MovePosition(objRb.position + 0.1f * Time.deltaTime * exitPoint.position);

            yield return new WaitForFixedUpdate();
        }

        objRb.position = exitPoint.position;

        //obj.transform.position + 8f * Time.deltaTime * new Vector3(exitPoint.position.x, exitPoint.position.y, refPoint.z)

        //yield return new WaitForSeconds(5f);

        obj.GetComponent<Character>().TomarDano(15);
        obj.GetComponent<Rigidbody>().isKinematic = false;
        bolha.SetActive(false);
        //descongelado.SetActive(true);
        obj.gameObject.GetComponent<Character>().catMesh.enabled = true;
        obj.gameObject.GetComponent<Character>().catFaceMesh.enabled = true;
        obj.gameObject.GetComponent<Character>().weaponsObj.SetActive(true);

        obj.gameObject.GetComponent<Character>().canMove = true;
    }

    //IEnumerator EsperaBreak(bool b)
    //    {
    //        Debug.Log("entrou no esperabreak");
    //        yield return new WaitForSeconds(3f);
    //        b = true;
    //    }

    private GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }

}
