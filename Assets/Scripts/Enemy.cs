using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Script responsável por fazer o humano seguir o gato utilizando NavMesh
public class Enemy : MonoBehaviour
{
    public float radius = 1f;

    public Transform[] waypoints;
    public Transform exitPoint;

    public Animator humanAnimator;

    public NavMeshAgent agente;

    public float distanceToTarget = 0f;
    public Transform target;

    public float chaseTime = 5f;
    public float maxChaseTime = 5f;
    public bool pauseChaseTime = false;

    public bool cansou;

    int index;

    private bool patrulhaRunning = false;

    private bool perseguirRunning = false;

    public GameManager gManager;

    public EnemyRespawn enemyRespawn;

    private void Start()
    {
        chaseTime = maxChaseTime;
        //agente.speed = 10f * (enemyRespawn.spawnCount / 2);
    }

    private void Update()
    {
        ChecaEstado();
    }

    private void ChecaEstado()
    {
        if (!cansou)
        {
            if (FindTarget())
            {
                if (!perseguirRunning)
                {
                    StopAllCoroutines();
                }
                StartCoroutine(Perseguir());
            }
            else
            {
                if (!patrulhaRunning)
                {
                    StopAllCoroutines();
                }
                StartCoroutine(Patrulha());
            }
        }
        else
        {
            StartCoroutine(Desistir());
        }
    }



    private bool FindTarget()
    {
        GameObject caixa;

        LimpaDistancia();

        foreach (GameObject t in GameManager.ActivePlayers)
        {
            if (distanceToTarget == 0)
            {
                distanceToTarget = Vector3.Distance(this.gameObject.transform.position, t.transform.position);
                target = t.transform;
            }
            else
            {
                caixa = FindChildWithTag(t, "Box");
                if (!caixa.activeSelf && (Vector3.Distance(this.gameObject.transform.position, t.transform.position) < distanceToTarget))
                {
                    distanceToTarget = Vector3.Distance(this.gameObject.transform.position, t.transform.position);
                    target = t.transform;
                }
            }
        }

        if (target != null)
        {
            if (distanceToTarget > radius + 5 || FindChildWithTag(target.gameObject, "Box").activeSelf)
            {
                target = null;
            }
        }

        return (target != null);
    }

    private void LimpaDistancia()
    {
        if (target == null)
        {
            humanAnimator.ResetTrigger("Encontrou");
            humanAnimator.SetTrigger("PerdeuAlvo");
            distanceToTarget = 999;
        }
        //humanAnimator.CrossFade("Idle",0.3f);
    }


    public IEnumerator Patrulha()
    {
        patrulhaRunning = true;

        agente.speed = 10f * (enemyRespawn.spawnCount / 2f);

        if (agente.remainingDistance < 1)
        {
            humanAnimator.ResetTrigger("Patrulha");
            humanAnimator.SetTrigger("Espera");
            if (index >= waypoints.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }


            yield return new WaitForSeconds(2f);
            humanAnimator.ResetTrigger("Espera");
            humanAnimator.SetTrigger("Patrulha");
            agente.SetDestination(waypoints[index].position);
        }

        yield return null;
        patrulhaRunning = false;
    }

    IEnumerator Perseguir()
    {
        ///////////////////////////////////////////////////ativar animaçao de surpresa depois trocar pra perseguir quando animacao acabar
        perseguirRunning = true;

        //Debug.Log(target);

        if (target != null && chaseTime > 0 && !pauseChaseTime)
        {
            agente.speed = 10f * (enemyRespawn.spawnCount) *0.6f;
            chaseTime -= Time.deltaTime;
            humanAnimator.ResetTrigger("Espera");
            humanAnimator.SetTrigger("Encontrou");
            agente.isStopped = false;
            agente.SetDestination(target.position);
        }
        else if (chaseTime <= 0)
        {
            agente.speed = 0f;
            humanAnimator.SetTrigger("Cansou");
            cansou = true;
            target = null;

        }
        else if (target == null)
        {
            humanAnimator.ResetTrigger("Encontrou");
            humanAnimator.SetTrigger("PerdeuAlvo");
        }

        yield return null;
        perseguirRunning = false;
    }

    IEnumerator Desistir()
    {
        agente.speed = 5f;
        agente.SetDestination(exitPoint.position);

        humanAnimator.ResetTrigger("Encontrou");
        humanAnimator.ResetTrigger("Espera");

        if (agente.remainingDistance < 1)
        {
            enemyRespawn.Esconder();
        }

        yield return null;
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player1"))
        {
            humanAnimator.speed = 0f;
            pauseChaseTime = true;
            agente.speed = 0f;
            col.gameObject.GetComponent<Character>().VeryAngryMeow();
            //gManager.RemovePlayer(col.gameObject);
            StartCoroutine(gManager.RemovePlayer(col.gameObject));
            //Time.timeScale = 0;
        }
    }
    //void OnDestroy()
    //{
    //    Debug.Log("Destroyed human");
    //}

    public void AtualizaRaio(int spawnCount)
    {
        radius *= ((spawnCount/2f)+1f);
    }

    public void AtualizaAcel(int spawnCount)
    {
        agente.acceleration *= ((spawnCount / 2f) + 1f);
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


}
