using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rabo : MonoBehaviour
{
    //private GamepadInput controls;

    private Vector2 tail;

    private PlayerInput playerInput;

    [Header("Raio do movimento")]
    private float radius = 12f;

    [Header("Pontos de referencia")]
    private Transform target;

    private Transform pontoZero;

    private Vector2 circleCenter;

    [Header("Weapon Info")]
    private Weapon weapon;

    // Posicao da mira que sera utilizada para instanciar a arma
    //private Transform aimTransform;
    // variavel que armazena a tag "Clone"
    //private static String tagClone = "Clone";
    // variavel que armazena o objeto parent
    //private GameObject parentWeapon;

    //float mZCoord;
    //Vector3 mOffset;
    //private GameObject pivot;
    //private GameObject test;

    public void SetupRabo(Transform target, Transform pontoZero, PlayerInput playerInput)
    {
        SetPlayerInput(playerInput);
        SetTarget(target);
        SetZero(pontoZero);
    }



    private void Awake()
    {
        //aim = GameObject.FindWithTag("Alvo");
        //controls = new GamepadInput();

        //controls.Gameplay.Tail.performed += ctx =>
        //    tail = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Tail.canceled += ctx => tail = Vector2.zero;

        ////controls.Gameplay.Throw.performed += ctx => AimThrowWeapon(tail);
        ////Adicionando evento de ao soltar o botao, atirar a arma
        //controls.Gameplay.Throw.canceled += ctx => AimThrowWeapon();
    }

    void Start()
    {
        circleCenter =
            new Vector2(pontoZero.localPosition.x, pontoZero.localPosition.z);

        //mZCoord = Camera.main.WorldToScreenPoint(pivot.transform.position).z;
        //mZCoord = Camera.main.WorldToScreenPoint(test.transform.position).z;
        //mOffset = test.transform.position - GetMouseWorldPos();
    }

    //private void OnEnable()
    //{
    //    controls.Gameplay.Enable();
    //}

    //private void OnDisable()
    //{
    //    controls.Gameplay.Disable();
    //}

    private void Update()
    {
        if (playerInput != null)
        {
            tail = playerInput.actions["Tail"].ReadValue<Vector2>();
        }
        else
        {
            Debug.LogWarning("playerInput null");
        }

        ResetTail();

        MoveTail();

        KeepRadius();

        //        transform.position = new Vector3((GetMouseWorldPos() + mOffset).x, pivot.transform.position.y+2, (GetMouseWorldPos() + mOffset).z);
        //transform.position = new Vector3(GetMouseWorldPos().x, pivot.transform.position.y+2, (GetMouseWorldPos().z));
    }

    public void MoveTail()
    {
        float speed;

        if (weapon == null)
        {
            speed = 150f;
        }
        else
        {
            speed = weapon.weaponSpeed;
        }

        if (tail != Vector2.zero)
        {
            target.localPosition = new Vector3(target.localPosition.x,
                    6,
                    target.localPosition.z);
        }
        Vector3 moveTail = speed * Time.deltaTime * new Vector3(tail.x, 0, tail.y);
        target.Translate(moveTail, Space.World);
    }

    public void KeepRadius()
    {
        Vector2 targetXZ =
            new Vector2(target.localPosition.x, target.localPosition.z);

        Vector2 v = targetXZ - circleCenter;
        v = Vector2.ClampMagnitude(v, radius);
        target.localPosition =
            new Vector3((circleCenter.x + v.x),
                (target.localPosition.y),
                (circleCenter.y + v.y));

        //float dist = Vector2.Distance(targetXZ, circleCenter);

        //if (dist > radius)
        //{
        //    Vector2 originToObject = targetXZ - circleCenter;
        //    originToObject *= radius / dist;
        //    target.localPosition = pontoZero.localPosition + originToObject;
        //    target.localPosition = new Vector3(target.localPosition.x, 0, target.localPosition.z);
        //    transform.position = target.localPosition;
        //}
    }

    public void ResetTail()
    {
        pontoZero.localPosition =
            new Vector3(pontoZero.localPosition.x,
                9,
                pontoZero.localPosition.z);
        if (tail == Vector2.zero)
        {
            target.localPosition = pontoZero.localPosition;
        }
    }

    public void SetRadius(float raio)
    {
        this.radius = raio;
    }

    public void setWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetPlayerInput(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    public void SetZero(Transform pontoZero)
    {
        this.pontoZero = pontoZero;
    }

    //mira inicial
    /*
    void AimTestando()
    {
        if (weapon.weaponType != "none")
        {
            //mudei pra pegar o gameobject ao inves do objeto da classe
            //Instanciando a arma que será lançada
            Instantiate(weapon.gameObject, aimTransform.position, aimTransform.rotation);
            //destruindo a arma instanciada originalmente depois de apertar para atirar
            Destroy(weapon);
        }
    }*/

    //void AimThrowWeapon(Vector2 direcao)
    //void AimThrowWeapon()
    //{
    //    /////////////////////////////////////////////////CONTINUAR AQUI
    //    if (weapon.weaponType != "none")
    //    {
    //        //mudei pra pegar o gameobject ao inves do objeto da classe
    //        //Instanciando a arma que será lançada
    //        GameObject novaArma = Instantiate(weapon.gameObject, aimTransform.position, aimTransform.rotation);
    //        novaArma.tag = tagClone;

    //        gameObject.SetActive(!gameObject.activeSelf);
    //    }
    //    /*
    //    if (tail != Vector2.zero)
    //    {
    //        weapon.Throw (direcao);
    //    }
    //    */
    //}

    // funcao recursiva feita para desativar children de weapon (ainda não está 100% pois desativa todas as children e target não pode ser desativada)
    //public static void SetActiveRecursivelyExt(GameObject obj, bool state)
    //{
    //    obj.SetActive(state);
    //    foreach (Transform child in obj.transform)
    //    {
    //        if (child.gameObject.CompareTag("Weapon1"))
    //        {
    //            SetActiveRecursivelyExt(child.gameObject, state);

    //        }
    //    }
    //}

    //private Vector3 GetMouseWorldPos()
    //{
    //    Vector3 mousePoint = Input.mousePosition;
    //    mousePoint.z = mZCoord;

    //    return Camera.main.ScreenToWorldPoint(mousePoint);
    //}
}
