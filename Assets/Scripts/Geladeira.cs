using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Geladeira : MonoBehaviour
{
    private Animator animGela;
    
    [SerializeField]
    private Transform refPoint;
    [SerializeField]
    private Transform exitPoint;
    [SerializeField]
    private GameObject vfxCold;

    private bool engolindo = false;

    // Start is called before the first frame update
    void Start()
    {
        animGela = this. GetComponent<Animator>();
    }

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

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player1") && !engolindo)
        {
            if (!col.gameObject.GetComponent<Character>().isOnFire)
            {
                engolindo = true;
                StartCoroutine(Engolir(col));
            }
        }
    }

    IEnumerator Engolir(Collider obj)
    {
        GameObject congelado;
        //GameObject descongelado;

        vfxCold.SetActive(false);

        //obj.gameObject.SetActive(false);

        obj.gameObject.GetComponent<Character>().canMove = false;

        obj.gameObject.transform.position = refPoint.position;

        congelado = FindChildWithTag(obj.gameObject, "Congelado");
        //descongelado = FindChildWithTag(obj.gameObject, "Descongelado");

        //congelado.SetActive(false);
        //descongelado.SetActive(false);
        obj.gameObject.GetComponent<Character>().catMesh.enabled = false;
        obj.gameObject.GetComponent<Character>().catFaceMesh.enabled = false;
        obj.gameObject.GetComponent<Character>().weaponsObj.SetActive(false);

        animGela.SetTrigger("Engolir");

        StartCoroutine(Cuspir(obj));

        yield return null;

    }

    IEnumerator Cuspir(Collider obj)
    {
        GameObject congelado;
        //GameObject descongelado;

        congelado = FindChildWithTag(obj.gameObject, "Congelado");
        //descongelado = FindChildWithTag(obj.gameObject, "Descongelado");

        obj.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(2f);

        //Congela o gato e ativa animação da geladeira
        congelado.SetActive(true);
        animGela.ResetTrigger("Engolir");
        animGela.SetTrigger("Cuspir");


        yield return new WaitForSeconds(1.4f);



        //Arremessa o gato pra fora
        obj.gameObject.transform.position = exitPoint.position;
        obj.GetComponent<Rigidbody>().isKinematic = false;
        //obj.attachedRigidbody.AddForce(this.transform.up * -800f, ForceMode.Impulse);
        
        obj.attachedRigidbody.AddForce(new Vector3(0,20,-200f), ForceMode.Impulse);

        obj.GetComponent<Character>().TomarDano(15);

        yield return new WaitForSeconds(3f);

        //Descongela o gato
        congelado.SetActive(false);
        //descongelado.SetActive(true);
        obj.gameObject.GetComponent<Character>().catMesh.enabled = true;
        obj.gameObject.GetComponent<Character>().catFaceMesh.enabled = true;
        obj.gameObject.GetComponent<Character>().weaponsObj.SetActive(true);

        obj.gameObject.GetComponent<Character>().canMove = true;

        yield return new WaitForSeconds(2f);

        vfxCold.SetActive(true);
        engolindo = false;
 
    }

}
