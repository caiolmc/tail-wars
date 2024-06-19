using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    Rigidbody rb;
    //Character cat;
    // ? Qual o tipo da arma: hammer, fish, bottle, knife.
    public string weaponType;

    public float weaponSpeed;

    public float raio = 5f;

    private float force;

    private int damage;

    private int hitCount = 0;

    public TrailRenderer trail;
    //public PlayerInput playerInput;

    // gameobject de target que será instanciado no start
    //private GameObject target;
    // clone instanciado a partir da arma original
    //private GameObject clone;


    // Start is called before the first frame update
    void Start()
    {
        // definindo target
        //target = GameObject.FindWithTag("Target");
        // definindo clone
        //clone = GameObject.FindWithTag("Clone");

        rb = this.GetComponent<Rigidbody>();

        SetDamage();
    }

    void Update()
    {

    }

    public void HideTrail(PlayerInput playerInput)
    {
        if (playerInput.actions["Tail"].ReadValue<Vector2>() == Vector2.zero)
        {
            //trail.emitting = false;
            trail.gameObject.SetActive(false);
        }
        else
        {
            //trail.emitting = true;
            trail.gameObject.SetActive(true);
        }
    }

    //fazendo a arma se mover na direçao oposta a cauda
    //private void FixedUpdate()
    //{
    //    // verificando se o clone existe
    //    if (clone != null)
    //    {
    //        // fazendo a arma se mover em direção ao alvo
    //        clone.transform.position =
    //            Vector3.MoveTowards(clone.transform.position,
    //                target.transform.position, weaponSpeed * Time.deltaTime);
    //        clone.transform.up = target.transform.position - clone.transform.position;
    //    }

    //}

    //public void Aim()
    //{
    //    /* TODO
    //      TODO entrar no modo mira quando pressionar e segurar o bot�o
    //      TODO diminuir velocidade de movimento
    //      TODO direção oposta ao da cauda 
    //    */
    //}

    //public void Throw(Vector2 direcao)
    //{
    //    direcao *= -1;

    //    // TODO CONTINUAR AQUI
    //}

    //Ideia inicial para combate
    ////checar a distancia viajada pelo target no inicio e no fim do update e s� dar dano dependendo do valor desta distancia viajada
    public void KnockBack(string weaponType, Rigidbody rbc)
    {
        switch (weaponType)
        {
            case "hammer":
                force = 5f;
                break;
            case "fish":
                force = 2f;
                break;
            case "bottle":
                force = 4f;
                break;
            //case "broken bottle":
            //    force = 700f;
            //    break;
            case "guitar":
                force = 3f;
                break;
            case "knife":
                force = 1f;
                break;
        }

        //if (weaponType == "hammer")
        //{
        //    //Debug.Log("entrou hammer");
        //    force = 2000f;
        //}
        //else if (weaponType == "fish")
        //{
        //    force = 1000f;
        //}
        //else if (weaponType == "bottle")
        //{
        //    force = 800f;
        //}
        //else if (weaponType == "brokenBottle")
        //{
        //    force = 700f;
        //}
        //else if (weaponType == "knife")
        //{
        //    force = 300f;
        //}
        //else if (weaponType == "guitar")
        //{
        //    force = 1500f;
        //}

        //Collider[] colliders = Physics.OverlapSphere(transform.position, raio);

        //foreach (Collider attackedPlayer in colliders)
        //{
        //    Rigidbody rb = attackedPlayer.GetComponent<Rigidbody>();


        //    if (rb != null && rb != this.GetComponent<Rigidbody>())
        //    {
        //        //rb.AddExplosionForce(force, transform.position, raio);
        //        //Debug.Log("entrou explosao");
        //    }

        //}

        rbc.AddForce(new Vector3(0, force * 3, 0), ForceMode.Impulse);


        // TESTAR DEPOIS
        // * force is how forcefully we will push the player away from the enemy.
        //float force = 3;

        //// If the object we hit is the enemy
        //if (c.gameObject.tag == "enemy")
        //{
        //    // Calculate Angle Between the collision point and the player
        //    Vector3 dir = c.contacts[0].point - transform.position;
        //    // We then get the opposite (-Vector3) and normalize it
        //    dir = -dir.normalized;
        //    // And finally we add force in the direction of dir and multiply it by force.
        //    // This will push back the player
        //    GetComponent<Rigidbody>().AddForce(dir * force);
        //}
    }

    //destruindo clone apos colisao
    //lembra de atualizar o prefeb se n funcionar
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(clone){
            Destroy(clone);
            //Debug.Log("Destruiu ao colidir");
        }
    }*/


    private void OnTriggerEnter(Collider col)
    {
        if (this.CompareTag("Weapon1") && col.gameObject.CompareTag("Player1"))
        {
            if ((FindParentID(this.gameObject) != col.GetComponent<PlayerInput>().user.id))
            {
                if (col.gameObject.GetComponent<Character>().isInvincible == false)
                {
                    //Debug.Log("entrou1");
                    KnockBack(weaponType, col.GetComponent<Rigidbody>());
                    col.gameObject.GetComponent<Character>().TomarDano(damage);
                    hitCount++;
                    BonusEffects();
                }


            }



            //KnockBack(weaponType);

            ////A l�gica de dano costumava ficar aqui na arma, mas foi movida para o script do personagem.

            //if (col.gameObject.GetComponent<Character>().maxStamina > 0)
            //{
            //    if (col.GetComponent<Character>().maxStamina <= 5)
            //    {
            //        col.GetComponent<Character>().maxStamina = 0;
            //    }
            //    else
            //    {
            //        col.GetComponent<Character>().maxStamina -= 5;
            //    }

            //Debug.Log("atacou "+col.gameObject.tag);
            //}

            ////Tentativa de knockback.
            //col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3 (0,200,0));
        }
    }

    void SetDamage()
    {

        damage = weaponType switch
        {
            "hammer" => 15,
            "fish" => 8,
            "bottle" => 20,
            //case "broken bottle":
            //    damage = 10;
            //    break;
            "guitar" => 10,
            "knife" => 5,
            _ => 0,
        };


        //if (weaponType == "hammer")
        //{
        //    damage = 15;
        //}
        //else if (weaponType == "fish")
        //{
        //    damage = 8;
        //}
        //else if (weaponType == "bottle")
        //{
        //    damage = 8;
        //}
        //else if (weaponType == "knife")
        //{
        //    damage = 5;
        //}
        //else if (weaponType == "guitar")
        //{
        //    damage = 10;
        //}
        //else
        //{
        //    damage = 0;
        //}
    }

    void SetDamage(int dmg)
    {
        damage = dmg;
    }

    void SetKnockBackForce(int kBForce)
    {
        force = kBForce;
    }

    void BonusEffects()
    {
        //bottle bonus
        if ((weaponType == "bottle") && hitCount > 0)
        {
            //Debug.Log("test");
            //nao conhecia o parametro do metodo getcomponentinchildren pra encontrar filhos desativados
            //foreach (GarrafaInteira obj in GetComponentsInChildren<GarrafaInteira>())
            //{
            //    if (GetComponentsInChildren<GarrafaInteira>().Length > 0)
            //    {
            //        obj.gameObject.SetActive(false);
            //        Debug.Log("testa desativa");
            //    }
            //}

            //foreach (GarrafaQuebrada obj in GetComponentsInChildren<GarrafaQuebrada>())
            //{
            //    if (GetComponentsInChildren<GarrafaQuebrada>().Length > 0)
            //    {
            //        obj.gameObject.SetActive(true);
            //        Debug.Log("testa ativa");
            //    }
            //}

            GetComponentInChildren<GarrafaInteira>(true).gameObject.SetActive(false);
            //EFEITOS VISUAIS DA GARRAFA QUEBRANDO
            //EFEITO SONORO DE VIDRO QUEBRANDO E GATIN SOFRENDO
            //POSSIVEL VIDRO NO CHAO VIRANDO UM OBSTACULO QUE DA DANO NOS GATINS??
            GetComponentInChildren<GarrafaQuebrada>(true).gameObject.SetActive(true);
            //            weaponType = "broken bottle";
            SetDamage(10);
            SetKnockBackForce(2);
        }
        else if ((weaponType == "hammer"))
        {
            //CASO BATA COM O MARTELO NO CHAO TODO MUNDO DA UM PULIN DO IMPACTO
            //EFEITO VISUAL DE IMPACTO

        }
    }

    public static uint FindParentID(GameObject childObject)
    {
        uint id = 10;

        Transform t = childObject.transform;

        while (t.parent != null)
        {
            if (t.parent.gameObject.GetComponent<PlayerInput>() != null)
            {
                id = t.parent.GetComponent<PlayerInput>().user.id;
                //break;
            }
            t = t.parent;
        }
        return id;
    }


}


