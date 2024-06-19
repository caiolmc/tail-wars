using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [Header("Painéis")]
    public GameObject painelStart;
    public GameObject painelMenu;
    public GameObject painelNumJogadores;
    public GameObject painelCreditos;
    public GameObject painelControles;
    public GameObject painelExit;

    [Header("Elementos UI")]
    public Slider sliderNumJogadores;
    public GameObject logo;
    //public GameObject logoG;
    public TextMeshProUGUI playerCountText;
    public GameObject grid;
    public GameObject selection;
    public GameObject transition;

    [Header("First Selected Options")]
    public GameObject menuFirst;


    [Header("Botoes")]
    //private GameObject selectedButton;
    public Button jogar;
    public Button controles;
    public Button creditos;
    public Button voltar1;
    public Button voltar2;

    public PlayerInput menuInput;

    private GamepadInput controls;

    public static int playerCount = 2;

    public RectTransform[] buttonPositions;

    public Vector2 inputMovement;
    public int buttonUp = 0;

    public int jogarControl = 0;

    public AudioSource audioUI;

    public List<AudioClip> audioClips = new();


    private void Awake()
    {
        controls = new GamepadInput();

        controls.UI.Navigate.performed += ctx => inputMovement = ctx.ReadValue<Vector2>();
        controls.UI.Navigate.performed += ctx => buttonUp++;
        controls.UI.Navigate.canceled += ctx => inputMovement = Vector2.zero;
        controls.UI.Navigate.canceled += ctx => buttonUp = 0;
    }

    private void OnEnable()
    {
        controls.UI.Enable();
    }

    private void OnDisable()
    {
        controls.UI.Disable();
    }


    public void AdvanceMenu()
    {
        if (painelStart.activeSelf)
        {
            audioUI.clip = audioClips[0];
            audioUI.Play();
            painelStart.SetActive(false);
            painelMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(menuFirst);
        }

    }


    private void Update()
    {
        playerCountText.text = sliderNumJogadores.value.ToString();
        playerCount = (int)sliderNumJogadores.value;

        //Navigate();

        //ShowGrid();

        controls.UI.Grid.performed += ctx => ShowGrid();

        StartMenu();

        PlayGame();

        Voltar();

        MoveSelection();

        ExitGame();
        //Debug.Log(EventSystem.current.currentSelectedGameObject);
    }

    private void ExitGame()
    {
        if(painelMenu.activeSelf && !painelNumJogadores.activeSelf && !painelControles.activeSelf && !painelCreditos.activeSelf)
        {
            //menuInput.actions["Voltar"].IsPressed() ||
            if (menuInput.actions["Exit"].IsPressed())
            {
                logo.SetActive(false);
                painelExit.SetActive(true);
            }
        }

        if (painelExit.activeSelf)
        {
            if (menuInput.actions["Interact"].IsPressed())
            {
                Application.Quit();
            }
            else if (menuInput.actions["Voltar"].IsPressed())
            {
                logo.SetActive(true);
                painelExit.SetActive(false);
            }
        }
    }

    private void MoveSelection()
    {
        if (!painelNumJogadores.activeSelf && jogar.gameObject.activeSelf && painelMenu.activeSelf)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                selection.GetComponent<RectTransform>().position = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position;
            }
        }
    }

    private void StartMenu()
    {
        if (painelStart.activeSelf)
        {
            controls.UI.Start.performed += ctx => AdvanceMenu();
        }



    }

    public void CloseAll()
    {
        //Debug.Log("CLOSE ALL");
        jogarControl = 0;
        painelMenu.SetActive(true);
        painelNumJogadores.SetActive(false);
        painelStart.SetActive(false);
        jogar.gameObject.SetActive(true);
        painelControles.SetActive(false);
        painelCreditos.SetActive(false);
        logo.SetActive(true);

        //EventSystem.current.SetSelectedGameObject(menuFirst);
        //MoveSelection();


    }

    //private void Navigate()
    //{
    //    controls.UI.Interact.performed += ctx => Interact(selectedButton);
    //}

    //private void Interact(GameObject selectedButton)
    //{
    //    foreach (RectTransform rt in buttonPositions)
    //    {
    //        if (selectedButton.GetComponent<RectTransform>().position == rt.position)
    //        {
    //            //rt.gameObject.GetComponent<Button>().
    //        }
    //    }
    //}

    private void ShowGrid()
    {
        //if (controls.UI.Grid.IsPressed())
        //{

        //}

        grid.SetActive(!grid.activeSelf);
    }

    public void OnJogarPressed()
    {
        if (painelMenu.activeSelf)
        {
            audioUI.clip = audioClips[0];
            audioUI.Play();
            jogarControl++;
            painelNumJogadores.SetActive(true);
            jogar.gameObject.SetActive(false);
            menuInput.actions["Interact"].Disable();
        }
    }

    public void OnCreditosPressed()
    {
        if (!painelControles.activeSelf && !painelCreditos.activeSelf && !painelNumJogadores.activeSelf)
        {
            audioUI.clip = audioClips[0];
            audioUI.Play();
            painelCreditos.SetActive(true);
            painelMenu.SetActive(false);
            logo.SetActive(false);
            //EventSystem.current.SetSelectedGameObject(voltar2.gameObject);

        }
    }

    public void OnControlesPressed()
    {
        if (!painelControles.activeSelf && !painelCreditos.activeSelf && !painelNumJogadores.activeSelf)
        {
            audioUI.clip = audioClips[0];
            audioUI.Play();
            painelControles.SetActive(true);
            painelMenu.SetActive(false);
            logo.SetActive(false);
            //EventSystem.current.SetSelectedGameObject(voltar1.gameObject);
        }
    }

    public void Voltar()
    {
        if (painelControles.activeSelf || painelCreditos.activeSelf)
        {
            //if (menuInput.actions["Interact"].IsPressed())
            //{
            //    CloseAll();
            //}
            if (menuInput.actions["Voltar"].IsPressed())
            {
                CloseAll();
            }

            //controls.UI.Interact.performed += ctx => CloseAll();
            //controls.UI.Voltar.performed += ctx => CloseAll();

        }
    }

    public void PlayGame()
    {
        //Debug.Log(jogarControl);

        if (painelNumJogadores.activeSelf && !jogar.gameObject.activeSelf)
        {

            ManagePlayerCount();

            if (jogarControl > 0)
            {
                menuInput.actions["Interact"].Enable();
                if (menuInput.actions["Interact"].IsPressed())
                {
                    //SceneManager.LoadScene("Gameplay1");
                    audioUI.clip = audioClips[1];
                    audioUI.Play();
                    StartCoroutine(Transition());
                }
            }


            controls.UI.Voltar.performed += ctx => CloseAll();
        }
    }

    IEnumerator Transition()
    {

        transition.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Gameplay1");

        yield return null;
    }

    private void ManagePlayerCount()
    {
        if (buttonUp == 1)
        {
            buttonUp++;
            if (inputMovement == new Vector2(1, 0))
            {
                if (sliderNumJogadores.value == 2)
                {
                    sliderNumJogadores.value = 3;
                }
                else if (sliderNumJogadores.value == 3)
                {
                    sliderNumJogadores.value = 4;
                }
            }
            else if (inputMovement == new Vector2(-1, 0))
            {
                if (sliderNumJogadores.value == 4)
                {
                    sliderNumJogadores.value = 3;
                }
                else if (sliderNumJogadores.value == 3)
                {
                    sliderNumJogadores.value = 2;
                }
            }
        }

        //controls.UI.Navigate.ReadValue<>() += ctx => CloseAll();
    }
}
