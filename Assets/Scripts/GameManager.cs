using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
//using System.Linq;
using UnityEngine.InputSystem.Users;
using System;
using UnityEngine.SceneManagement;
//using UnityEngine.AddressableAssets;
//using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("Players prefabs")]
    private List<GameObject> playerPrefabs;

    [SerializeField]
    private List<GameObject> ActivePlayersVisible = new();
    public static List<GameObject> ActivePlayers = new();

    [SerializeField, Header("Pontos de spawn")]
    private GameObject[] spawnPoints;

    [SerializeField, Header("Barras de vida")]
    private GameObject[] hpBars;

    [SerializeField, Header("Player numbers")]
    private GameObject[] playerNumbers;

    public GameObject spawnPointsParent;

    public Slider humanSlider;

    public static string vitorioso = "siames";

    public EnemyRespawn humanRespawn;

    [SerializeField, Header("Expulso")]
    private string expulso;
    public GameObject outPanel;
    public GameObject telaOutPreto;
    public GameObject telaOutBranco;
    public GameObject telaOutLaranja;
    public GameObject telaOutSiames;

    // Start is called before the first frame update
    void Start()
    {
        SetupPlayers();
    }

    private void PlayerNumControl(int playerIndex, bool remove)
    {
        if (!remove)
        {
            playerNumbers[playerIndex].SetActive(true);
            playerNumbers[playerIndex].transform.position = Vector3.MoveTowards(playerNumbers[playerIndex].transform.position, new Vector3(ActivePlayers[playerIndex].transform.position.x, ActivePlayers[playerIndex].transform.position.y + 5f, ActivePlayers[playerIndex].transform.position.z), 5f);
        }
        else
        {
            //playerNumbers[ActivePlayers[playerIndex].GetComponent<PlayerInput>().user.id].SetActive(false);

            for (int i = 0; i < ActivePlayers.Count; i++)
            {
                playerNumbers[i].SetActive(false);
            }
        }
    }

    private void SetupPlayers()
    {
        foreach (GameObject player in playerPrefabs)
        {
            if ((player.GetComponent<PlayerInput>().user.controlScheme == null) || ActivePlayers.Count == MenuController.playerCount)
            {
                //player.GetComponent<Character>().hpBar.SetActive(false);
                player.SetActive(false);
            }
            else
            {
                if (ActivePlayers.Count < MenuController.playerCount)
                {
                    ActivePlayers.Add(player);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ActivePlayersVisible = ActivePlayers;

        FimDeJogo();

        SetupHumanSlider();

        SetupHPBars(false);

        for (int i = 0; i < ActivePlayers.Count; i++)
        {
            PlayerNumControl(i, false);
        }
    }

    private void SetupHumanSlider()
    {
        if (EnemyRespawn.humanCount > 0)
        {
            humanSlider.gameObject.SetActive(false);
        }
        else
        {
            humanSlider.gameObject.SetActive(true);
            humanSlider.value = EnemyRespawn.porcentagemHuman;
        }
    }

    //public void EndGame()
    //{
    //    foreach (PlayerInput playerInput in playerInputs)
    //    {
    //        // Disable the actions for the playerInput
    //        playerInput.actions.Disable();
    //    }

    //    playerInputs.Clear();
    //}


    void SetupHPBars(bool control)
    {
        if (control)
        {
            foreach (GameObject hp in hpBars)
            {
                hp.SetActive(false);
            }
        }

        for (int i = 0; i < ActivePlayers.Count; i++)
        {
            Character player = ActivePlayers[i].GetComponent<Character>();

            player.hpBar = hpBars[i];
            player.hpBar.SetActive(true);

            if (player.maxStamina <= 75 && player.maxStamina > 50)
            {
                //player.currentHpBg = FindChildWithTag(player.hpBar, "bg75").GetComponent<SpriteRenderer>();
                FindChildWithTag(player.hpBar, "100hp").SetActive(false);
                FindChildWithTag(player.hpBar, "75hp").SetActive(true);
                FindChildWithTag(player.hpBar, "50hp").SetActive(false);
                FindChildWithTag(player.hpBar, "25hp").SetActive(false);

            }
            else if (player.maxStamina > 25 && player.maxStamina <= 50)
            {
                //player.currentHpBg = FindChildWithTag(player.hpBar, "bg50").GetComponent<SpriteRenderer>();
                FindChildWithTag(player.hpBar, "100hp").SetActive(false);
                FindChildWithTag(player.hpBar, "75hp").SetActive(false);
                FindChildWithTag(player.hpBar, "50hp").SetActive(true);
                FindChildWithTag(player.hpBar, "25hp").SetActive(false);

            }
            else if (player.maxStamina <= 25)
            {
                //player.currentHpBg = FindChildWithTag(player.hpBar, "bg25").GetComponent<SpriteRenderer>();
                FindChildWithTag(player.hpBar, "100hp").SetActive(false);
                FindChildWithTag(player.hpBar, "75hp").SetActive(false);
                FindChildWithTag(player.hpBar, "50hp").SetActive(false);
                FindChildWithTag(player.hpBar, "25hp").SetActive(true);
            }
            else if (player.maxStamina > 75)
            {
                //player.currentHpBg = FindChildWithTag(player.hpBar, "bg100").GetComponent<SpriteRenderer>();
                FindChildWithTag(player.hpBar, "100hp").SetActive(true);
                FindChildWithTag(player.hpBar, "75hp").SetActive(false);
                FindChildWithTag(player.hpBar, "50hp").SetActive(false);
                FindChildWithTag(player.hpBar, "25hp").SetActive(false);
            }
        }
    }

    private void FimDeJogo()
    {
        if (ActivePlayers.Count == 1)
        {
            vitorioso = ActivePlayers[0].GetComponent<Character>().cor;
            //Time.timeScale = 0;
            SceneManager.LoadScene("FimDeJogo");
        }
    }

    //public void RemovePlayer(GameObject player)
    //{
    //    PlayerNumControl((int)player.GetComponent<PlayerInput>().user.id-1, true);
    //    //player.GetComponent<Character>().hpBar.SetActive(false);
    //    GameManager.ActivePlayers.Remove(player);
    //    SetupHPBars(true);
    //    player.SetActive(false);

    //}

    public IEnumerator RemovePlayer(GameObject player)
    {
        foreach (GameObject GO in ActivePlayers)
        {
            GO.GetComponent<Character>().canMove = false;
            GO.GetComponent<Character>().canHide = false;
        }
        player.GetComponent<Rigidbody>().isKinematic = true;
        outPanel.SetActive(true);
        //humanRespawn.humano.GetComponent<Enemy>().agente.speed = 0;
        //player.GetComponent<Character>().hpBar.SetActive(false);

        //Debug.Log("antes do wait");

        //yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(RemovePlayer2(player));
        yield return null;
    }

    public IEnumerator RemovePlayer2(GameObject player)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        outPanel.SetActive(true);
        if (player.GetComponent<Character>().cor == "preta")
        {
            telaOutPreto.SetActive(true);
            telaOutBranco.SetActive(false);
            telaOutLaranja.SetActive(false);
            telaOutSiames.SetActive(false);
        }
        else if (player.GetComponent<Character>().cor == "branca")
        {
            telaOutPreto.SetActive(false);
            telaOutBranco.SetActive(true);
            telaOutLaranja.SetActive(false);
            telaOutSiames.SetActive(false);
        }
        else if (player.GetComponent<Character>().cor == "laranja")
        {
            telaOutPreto.SetActive(false);
            telaOutBranco.SetActive(false);
            telaOutLaranja.SetActive(true);
            telaOutSiames.SetActive(false);
        }
        else if (player.GetComponent<Character>().cor == "siames")
        {
            telaOutPreto.SetActive(false);
            telaOutBranco.SetActive(false);
            telaOutLaranja.SetActive(false);
            telaOutSiames.SetActive(true);
        }
        yield return new WaitForSecondsRealtime(2f);


        //Debug.Log("depois do wait");

        foreach (GameObject GO in ActivePlayers)
        {
            GO.GetComponent<Character>().canMove = true;
            GO.GetComponent<Character>().canHide = true;
        }

        GameManager.ActivePlayers.Remove(player);
        player.SetActive(false);
        //Debug.Log("depois de remover o player");
        SetupHPBars(true);
        PlayerNumControl((int)player.GetComponent<PlayerInput>().user.id - 1, true);
        //Debug.Log("antes do esconder");
        humanRespawn.Esconder();
        //Debug.Log("depois do esconder");
        outPanel.SetActive(false);
        yield return null;
    }

    //void OnDestroy()
    //{
    //    Debug.Log("Destroyed game manager");
    //}

    private GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            //Debug.Log(transform.gameObject.name);

            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                //Debug.Log(child.name+" SETADO AQUI");
                break;
            }
            else
            {
                //Debug.Log(child.name);
                child = FindChildWithTag(transform.gameObject, tag);
            }
        }

        return child;
    }


}
