using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class Character : MonoBehaviour
{
    [Header("Animacoes")]
    [SerializeField]
    private Animator catAnimator;

    private float idleTime;

    //private GamepadInput controls;
    public bool canMove = true;

    [Header("Movement")]
    [SerializeField]
    float movespeed = 15f;
    public bool isOnFire = false;

    [SerializeField]
    private float jumpForce = 17f;

    //Vector3 forward, right;
    [SerializeField]
    //bool isGrounded = true;
    bool isBoosted = false;

    [SerializeField]
    private GameObject boostedVFX;

    //CharacterController cc;
    Rigidbody rb;

    //Vector2 move;

    //Vector3 forward, right;
    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina = 100f;
    public SpriteRenderer currentHpBg;


    [Header("Hide")]
    //LEMBRAR DE CONTINUAR ISSO AQUI (cooldown para poder se esconder)///////////////////////////////////////////////////
    public bool canHide = true;

    public bool hidden = false;

    [Header("Invincibility")]
    public bool isInvincible = false;
    public float invincibilityTime;
    public float invincibilityDeltaTime;

    [Header("Models")]
    public GameObject cat;
    public SkinnedMeshRenderer catMesh;
    public MeshRenderer catFaceMesh;

    public GameObject box;
    //public GameObject model;
    //private SkinnedMeshRenderer meshRenderer;
    public Color normalColor;
    public Color damageColor = Color.white;
    public string cor;

    [Header("Weapons")]
    public string equippedWeapon = "none";
    public GameObject weaponsObj;
    public List<GameObject> weapons;
    public List<bool> wpsStatus;
    //public Dictionary<bool, GameObject> wps = new Dictionary<bool, GameObject>();

    private PlayerInput playerInput;
    private Vector2 movement = new Vector2(0f, 0f);

    [Header("Cauda")]
    private Rabo rabo;
    public Transform target;
    public Transform pontoZero;
    public float radius = 12f;

    [Header("HP Bar")]
    public GameObject hpBar;

    float distToGround;

    [Header("Audio")]
    public AudioSource audioCatHigh;
    public AudioSource audioCatLow;
    public List<AudioClip> audioClipsCat = new();

    private void OnValidate()
    {
        //playerInput = GetComponent<PlayerInput>();
        //Debug.Log("salve");
    }


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        rabo = gameObject.AddComponent<Rabo>();
        rabo.SetupRabo(target, pontoZero, playerInput);
        //controls = new GamepadInput();
    }

    //private void OnEnable()
    //{
    //    controls.Gameplay.Enable();
    //    controls.Gameplay.Hide.performed += ctx => Hide();

    //    controls.Gameplay.Boost.started += ctx => isBoosted = true;
    //    controls.Gameplay.Boost.performed += ctx => isBoosted = true;
    //    controls.Gameplay.Boost.canceled += ctx => isBoosted = false;

    //    controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
    //    controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

    //    controls.Gameplay.Jump.performed += ctx => Jump();
    //}

    //private void OnDisable()
    //{
    //    controls.Gameplay.Disable();
    //}

    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;

        //meshRenderer = model.GetComponent<SkinnedMeshRenderer>();
        normalColor = catMesh.material.color;

        foreach (GameObject w in weapons)
        {
            if (w.activeSelf)
            {
                w.SetActive(false);
            }
        }
    }

    void Update()
    {
        foreach (GameObject w in weapons)
        {
            if (w.GetComponent<Weapon>().weaponType == equippedWeapon)
            {
                w.GetComponent<Weapon>().HideTrail(playerInput);
            }
        }


        IsIdle();

        //Debug.Log(playerInput);
        rabo.SetRadius(radius);

        if (playerInput != null)
        {
            movement = playerInput.actions["Move"].ReadValue<Vector2>();
        }
        else
        {
            Debug.LogWarning("playerInput null");
        }

        //Debug.Log(movement);

        StaminaControl();

        ChecaBoosted();

        //SAIR DO ESCONDERIJO QUANDO ACABAR A STAMINA
        if (hidden && stamina == 0)
        {
            Hide();
        }

        //Debug.Log("hidden: " + hidden);
        //Debug.Log("stamina: " + stamina);


        EquipWeapon();


        ////INPUT ANTIGO
        //if (Input.GetKeyDown("h") && stamina > 1 && this.tag == "Player1") // REMOVER ULTIMA CONDI��O
        //{
        //    Hide();
        //}

        //if (Input.GetKeyUp("left shift"))
        //{
        //    isBoosted = false;
        //}
    }

    private void IsIdle()
    {
        if (IsGrounded() && movement == Vector2.zero)
        {
            idleTime += Time.deltaTime;

            catAnimator.ResetTrigger("Jump");
            catAnimator.ResetTrigger("Walk");
            catAnimator.ResetTrigger("Run");


            if (idleTime < 10f)
            {
                catAnimator.SetTrigger("Idle");
            }
            else
            {
                catAnimator.SetTrigger("IdleLong");
            }
        }
        else
        {
            idleTime = 0;
        }
    }

    private void ChecaBoosted()
    {
        if (playerInput != null)
        {
            isBoosted = playerInput.actions["Boost"].IsPressed();
        }
        else
        {
            Debug.LogWarning("playerInput null");
        }
    }

    void FixedUpdate()
    {
        SetupMove();

        //if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        //{
        //    if (Input.GetKey("left shift") && !cat.hidden && cat.stamina > 10)
        //    {
        //        isBoosted = true;
        //        MoveBoosted();
        //    }
        //    else
        //    {
        //        Move();
        //    }

        //} else
        //{
        //    if (isGrounded)
        //    {
        //        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        //    }
        //}
    }

    public void SetupMove()
    {
        //controls.Gameplay.Move.enabled && 
        if (canMove)
        {
            if (!isOnFire)
            {
                ////INVERTI PORQUE NAO ESTAVA DESLIGANDO O BOOST QUANDO ACABAVA A STAMINA
                if (isBoosted && stamina > 0 && !hidden)
                {
                    //Move();
                    MoveBoosted();
                    boostedVFX.SetActive(true);
                }
                else
                {
                    //MoveBoosted();
                    Move();
                    boostedVFX.SetActive(false);
                }
            }
            else
            {
                MoveOnFire();
            }

            LookAt();
        }

    }

    private void EquipWeapon()
    {
        foreach (GameObject w in weapons)
        {
            w.SetActive(false);
        }

        foreach (GameObject w in weapons)
        {
            if (w.GetComponent<Weapon>().weaponType == equippedWeapon)
            {
                w.SetActive(true);
                rabo.setWeapon(w.GetComponent<Weapon>());
            }
            else if (equippedWeapon == "none")
            {
                w.SetActive(false);
                rabo.setWeapon(null);
            }
        }
    }


    private void StaminaControl()
    {
        ////GARANTIR QUE A STAMINA N�O PASSE DE SEU LIMITE MAXIMO
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        ////GARANTIR QUE A STAMINA N�O PASSE DE SEU LIMITE MINIMO
        if (stamina < 0)
        {
            stamina = 0;
        }

        ////CONSUMIR STAMINA ENQUANTO ESTIVER ESCONDIDO E POSSUIR ALGUMA STAMINA
        if (hidden && stamina > 0)
        {
            stamina -= 0.1f;
        } ////CONSUMIR STAMINA ENQUANTO ESTIVER COM BOOST E POSSUIR ALGUMA STAMINA
        else if (isBoosted && stamina > 0)
        {
            stamina -= 0.08f;
        } ////CASO SAIA DO ESCONDERIJO OU STAMINA ACABE ENTAO PARE DE CONSUMIR STAMINA
        else if (!isBoosted && !hidden && stamina < maxStamina)
        {
            stamina += 0.06f;
        }

        ////PEQUENO REGEN DE STAMINA MAXIMA
        if (maxStamina < 100 && !isOnFire)
        {
            if (movement == Vector2.zero)
            {
                maxStamina += 0.02f;
            }
            else
            {
                maxStamina += 0.01f;
            }
        }
    }

    public void TomarDano(int damage)
    {
        if (isInvincible) return;

        audioCatHigh.clip = audioClipsCat[UnityEngine.Random.Range(0, 2)];
        //audioCatHigh.pitch = 1.25f;
        audioCatHigh.Play();

        //Debug.Log(this.name + " tomou dano!");
        if (maxStamina > 0)
        {
            if (maxStamina <= damage)
            {
                maxStamina = 0;
            }
            else
            {
                maxStamina -= damage;
            }
        }

        StartCoroutine(TemporaryInvincibility());
    }

    public IEnumerator TemporaryInvincibility()
    {
        isInvincible = true;

        /////////Um unico flash longo, e um unico frame de invencibilidade.
        //meshRenderer.material.color = damageColor;
        //yield return new WaitForSeconds(invincibilityTime);
        //meshRenderer.material.color = normalColor;
        //isInvincible = false;
        for (float i = 0; i < invincibilityTime; i += invincibilityDeltaTime)
        {
            if (catMesh.material.color == normalColor)
            {
                catMesh.material.color = damageColor;
            }
            else
            {
                catMesh.material.color = normalColor;
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        catMesh.material.color = normalColor;
        isInvincible = false;
    }


    public void Hide()
    {
        if (!isOnFire)
        {
            //cat.SetActive(!cat.activeSelf);
            catMesh.enabled = !catMesh.enabled;
            catFaceMesh.enabled = !catFaceMesh.enabled;
            weaponsObj.SetActive(!weaponsObj.activeSelf);
            box.SetActive(!box.activeSelf);
            //catAnimator.enabled = !catAnimator.enabled;
            hidden = !hidden;
        }
    }

    private void Move()
    {
        if (IsGrounded())
        {
            catAnimator.ResetTrigger("Run");
            catAnimator.SetTrigger("Walk");
        }

        float velY = rb.velocity.y;

        Vector3 direction;

        if (hidden)
        {
            direction = 30f * movespeed * Time.deltaTime * new Vector3(movement.x, rb.velocity.y, movement.y);

        }
        else
        {
            direction = 70f * movespeed * Time.deltaTime * new Vector3(movement.x, rb.velocity.y, movement.y);
        }



        //Debug.Log(rb.velocity);

        if (direction != Vector3.zero)
        {

            //transform.forward = direction;
            //transform.forward = Vector3.Lerp(transform.forward, direction, 0.6f);
            //rb.AddForce(Vector3.Lerp(transform.forward, direction, 0.6f));
            //rb.AddForce(100f*direction);
            //rb.MovePosition(transform.position+direction);
            direction.y = velY;
            rb.velocity = direction;
        }



        ////CORRIGIR!!!//////////////////////////////////////////////////////////////////////////////////////////////////////////
        //transform.Translate(direction, Space.World);

        //rb.MovePosition(direction);
    }

    private void MoveBoosted()
    {
        if (IsGrounded())
        {
            catAnimator.ResetTrigger("Walk");
            catAnimator.SetTrigger("Run");
        }

        float velY = rb.velocity.y;
        Vector3 direction = 200f * movespeed * Time.deltaTime * new Vector3(movement.x, rb.velocity.y, movement.y);
        if (direction != Vector3.zero)
        {
            //transform.forward = direction;
            //transform.forward = Vector3.Lerp(transform.forward, direction, 0.7f);
            //rb.AddForce(Vector3.Lerp(transform.forward, direction, 0.6f));
            //rb.AddForce(direction);
            direction.y = velY;
            rb.velocity = direction;
        }

        //transform.Translate(direction, Space.World);
    }

    private void MoveOnFire()
    {
        if (IsGrounded())
        {
            catAnimator.ResetTrigger("Walk");
            catAnimator.SetTrigger("Run");
        }

        float velY = rb.velocity.y;

        Vector3 direction = 280f * movespeed * Time.deltaTime * this.transform.forward;

        if (direction != Vector3.zero)
        {

        }
        direction.y = velY;
        rb.velocity = direction;

    }

    private void LookAt()
    {
        if (!isOnFire)
        {
            Vector3 direction = new Vector3(movement.x, 0, movement.y);

            if (direction != Vector3.zero)
            {
                this.rb.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            if(movement == Vector2.zero)
            {
                Vector3 direction = this.transform.forward;
            }
            else
            {
                Vector3 direction = new Vector3(movement.x, 0, movement.y);

                if (direction != Vector3.zero)
                {
                    this.rb.rotation = Quaternion.LookRotation(direction);
                }
            }

        }
    }

    public void Jump()
    {
        if (IsGrounded() && canMove && !isOnFire)
        {
            catAnimator.ResetTrigger("Walk");
            catAnimator.ResetTrigger("Run");
            catAnimator.ResetTrigger("Idle");
            catAnimator.ResetTrigger("IdleLong");
            catAnimator.SetTrigger("Jump");

            //audioCatHigh.clip = audioClipsCat[4];
            //audioCatHigh.pitch = 1f;
            audioCatLow.Play();

            rb.velocity = Vector3.up * jumpForce;
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Terrain"))
    //    {
    //        isGrounded = false;
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Terrain"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    bool IsGrounded()
    {
        //catAnimator.ResetTrigger("Jump");
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    public void VeryAngryMeow()
    {
        audioCatHigh.clip = audioClipsCat[3];
        audioCatHigh.pitch = 1.25f;
        audioCatHigh.Play();
    }

}
