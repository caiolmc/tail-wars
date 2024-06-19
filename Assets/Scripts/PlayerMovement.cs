using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Script inutilizado")]
    public bool utilidade = false;

    //[SerializeField]
    //float movespeed = 10f;
    //[SerializeField]
    //float jumpForce = 1000f;
    //Vector3 forward, right;
    //bool isGrounded = true;
    //bool isBoosted = false;
    ////CharacterController cc;
    //Character cat;
    //Rigidbody rb;
    //GamepadInput controls;
    //Vector2 move;
    private void Awake()
    {
        //controls = new GamepadInput();
        //controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        //cat = GetComponent <Character>();
        //rb = this.GetComponent<Rigidbody>();
        //forward = Camera.main.transform.forward;
        //forward.y = 0;
        //forward = Vector3.Normalize(forward);
        //right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Update()
    {
        //if (isBoosted && cat.stamina > 0)
        //{
        //    cat.stamina -= 0.2f;
        //}
        //if (Input.GetButtonDown("Jump"))
        //{
        //    Jump();
        //}
        //if (Input.GetKeyUp("left shift"))
        //{
        //    isBoosted = false;
        //}
        //if(!isBoosted && !cat.hidden && cat.stamina < 100)
        //{
        //    cat.stamina += 0.03f;
        //}
    }

    void FixedUpdate()
    {
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

    //void Move()
    //{
    //    //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    Vector3 rightMovement = right * Input.GetAxisRaw("Horizontal");
    //    Vector3 upMovement = forward * Input.GetAxisRaw("Vertical");

    //    Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

    //    if (heading != new Vector3(0, 0, 0))
    //    {
    //        //transform.forward = heading;
    //        transform.forward = Vector3.Lerp(transform.forward, heading, 0.4f);
    //    }

    //    if (GetComponent<Character>().hidden)
    //    {
    //        if (heading.magnitude >= 0.1f)
    //        {
    //            rb.velocity = new Vector3(heading.x * movespeed * 3 * Time.deltaTime, rb.velocity.y, heading.z * movespeed * 3 * Time.deltaTime);
    //        }
    //    }
    //    else
    //    {
    //        if (heading.magnitude >= 0.1f)
    //        {
    //            rb.velocity = new Vector3(heading.x * movespeed * 10 * Time.deltaTime, rb.velocity.y, heading.z * movespeed * 10 * Time.deltaTime);
    //        }
    //    }

    //}

    //void MoveBoosted()
    //{
    //    //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    Vector3 rightMovement = right * Input.GetAxisRaw("Horizontal");
    //    Vector3 upMovement = forward * Input.GetAxisRaw("Vertical");

    //    Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

    //    if (heading != new Vector3(0, 0, 0))
    //    {
    //        //transform.forward = heading;
    //        transform.forward = Vector3.Lerp(transform.forward, heading, 0.4f);
    //    }

    //    if (heading.magnitude >= 0.1f)
    //    {
    //        rb.velocity = new Vector3(heading.x * movespeed * 18 * Time.deltaTime, rb.velocity.y, heading.z * movespeed * 18 * Time.deltaTime);
    //    }

    //}

    //void Jump()
    //{
    //    if (isGrounded)
    //    {
    //        rb.velocity = Vector3.up * jumpForce;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Terrain")
    //    {
    //        isGrounded = false;
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Terrain")
    //    {
    //        isGrounded = true;
    //    }
    //}
}
