using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public PlayerInput playerInput;

    public GameObject telaSiames;
    public GameObject telaLaranja;
    public GameObject telaPreta;
    public GameObject telaBranca;

    //float t;
    //Vector3 startPosition = new(-68,0,0);
    //Vector3 target = new(68, 0, 0);
    //float timeToReachTarget = 2f;

    // Start is called before the first frame update
    void Start()
    {
        ShowVictoryScreen();
        //MoveScreen(telaSiames);
        //MoveScreen(telaBranca);
        //MoveScreen(telaPreta);
        //MoveScreen(telaLaranja);
    }

    //private void MoveScreen(GameObject tela)
    //{
    //    //startPosition = target = transform.position;
    //    t += Time.deltaTime / timeToReachTarget;
    //    tela.transform.position = Vector3.Lerp(startPosition, target, t);
    //    //telaSiames.transform.position

    //}

    // Update is called once per frame
    void Update()
    {
        if (playerInput.actions["Interact"].IsPressed())
        {
            Voltar();
        }
    }

    public void ShowVictoryScreen()
    {
        if(GameManager.vitorioso == "siames")
        {
            telaSiames.SetActive(true);
        }
        else if (GameManager.vitorioso == "laranja")
        {
            telaLaranja.SetActive(true);
        }
        else if (GameManager.vitorioso == "preta")
        {
            telaPreta.SetActive(true);
        }
        else if (GameManager.vitorioso == "branca")
        {
            telaBranca.SetActive(true);
        }
    }
    
    public void Voltar()
    {
        //if (playerInput.actions["Interact"].IsPressed())
        //{
        //    SceneManager.LoadScene("Menu");
        //}
        SceneManager.LoadScene("Menu");
    }

    //public void SetDestination(Vector3 destination, float time)
    //{
    //    t = 0;
    //    startPosition = transform.position;
    //    timeToReachTarget = time;
    //    target = destination;
    //}
}
