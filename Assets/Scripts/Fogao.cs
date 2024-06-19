using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Fogao : MonoBehaviour
{
    private Animator animFogao;

    public GameObject bocas;

    [SerializeField]
    private Transform refPoint;
    [SerializeField]
    private Transform exitPoint;
    [SerializeField]
    private GameObject vfxFogao;

    private bool engolindo = false;

    // Start is called before the first frame update
    void Start()
    {
        animFogao = this.GetComponent<Animator>();
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
            engolindo = true;
            StartCoroutine(Engolir(col));
        }

    }

    IEnumerator Engolir(Collider obj)
    {
        bocas.SetActive(false);
        //GameObject congelado;
        //GameObject descongelado;

        vfxFogao.SetActive(false);
        //obj.gameObject.transform.position = refPoint.position;
        obj.gameObject.SetActive(false);

        //obj.gameObject.GetComponent<Character>().canMove = false;



        animFogao.SetTrigger("Engolir");

        StartCoroutine(Cuspir(obj));

        yield return null;

    }

    IEnumerator Cuspir(Collider obj)
    {
        GameObject fogo;
        fogo = FindChildWithTag(obj.gameObject, "Fogo");

        //obj.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(2f);

        animFogao.ResetTrigger("Engolir");
        animFogao.SetTrigger("Cuspir");
                
        yield return new WaitForSeconds(1.4f);

        //Arremessa o gato pra fora

        obj.gameObject.SetActive(true);
        fogo.SetActive(true);
        obj.gameObject.GetComponent<Character>().isOnFire = true;

        obj.gameObject.transform.position = exitPoint.position;

        obj.attachedRigidbody.AddForce(new Vector3(180, 0, 0), ForceMode.Impulse);

        obj.GetComponent<Character>().TomarDano(10);

        yield return new WaitForSeconds(3f);

        //Descongela o gato
        //congelado.SetActive(false);
        //descongelado.SetActive(true);
        obj.GetComponent<Character>().TomarDano(10);
        fogo.SetActive(false);
        obj.gameObject.GetComponent<Character>().isOnFire = false;
        obj.gameObject.GetComponent<Character>().canMove = true;

        yield return new WaitForSeconds(2f);

        vfxFogao.SetActive(true);
        bocas.SetActive(true);
        engolindo = false;

    }

}
