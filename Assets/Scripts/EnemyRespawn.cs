using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Script responsável pelo respawn do humano
public class EnemyRespawn : MonoBehaviour
{
    public AudioSource audioHuman;

    public GameObject humano;
    //private Enemy humanEnemy;

    public Transform escondido;

    private static float timeCounter = 0;

    public float publicTimeCounter;

    public float TempoGerarHumano = 5f;

    public static int humanCount = 0;

    public static float porcentagemHuman = 100f;

    public int spawnCount = 1;

    private void Start()
    {
        timeCounter = TempoGerarHumano;

        //humanEnemy = GetComponent<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        publicTimeCounter = timeCounter;

        AtualizaBarra();

        SpawnHuman();

        //Esconder();
    }

    private void AtualizaBarra()
    {
        porcentagemHuman = (timeCounter * 100) / TempoGerarHumano;
    }

    public void SpawnHuman()
    {
        if (humanCount == 0)
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                humano.GetComponent<Enemy>().maxChaseTime += 5f;
                humano.GetComponent<Enemy>().chaseTime = humano.GetComponent<Enemy>().maxChaseTime;
                audioHuman.pitch = 1.5f;
                //Instantiate(humano, transform.position, transform.rotation);
                humano.transform.position = this.transform.position;
                //humano.GetComponent<NavMeshAgent>().enabled = true;
                humano.SetActive(true);
                humano.GetComponent<Enemy>().agente.speed = 10f * (spawnCount / 2f);
                StartCoroutine(humano.GetComponent<Enemy>().Patrulha());
                //if(humanEnemy != null)
                //{
                //    humanEnemy.cansou = false;
                //}
                humano.GetComponent<Enemy>().cansou = false;

                humanCount++;
                spawnCount++;
                TempoGerarHumano *= 1f / spawnCount;
                timeCounter = TempoGerarHumano;
            }
        }
    }

    //void OnDestroy()
    //{
    //    Debug.Log("Destroyed spawner");
    //}

    public void Esconder()
    {
        humano.GetComponent<Enemy>().AtualizaRaio(spawnCount);
        humano.GetComponent<Enemy>().AtualizaAcel(spawnCount);
        //Debug.Log("dentro do esconder");
        humano.GetComponent<Enemy>().humanAnimator.speed = 1f;
        audioHuman.pitch = 1f;
        humano.GetComponent<Enemy>().pauseChaseTime = false;
        humano.SetActive(false);
        humano.transform.position = escondido.position;
        humanCount--;
    }
}
