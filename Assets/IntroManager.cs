using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class IntroManager : MonoBehaviour
{
    public PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.actions["Interact"].IsPressed())
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
