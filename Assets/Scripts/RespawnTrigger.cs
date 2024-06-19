using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public GameManager gManager;

    public Transform respawnPoint;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player1"))
        {
            //new Vector3(36, 10, 18);
            col.gameObject.transform.position = respawnPoint.position;

            if(col.GetComponent<Character>().maxStamina <= 25 && col.GetComponent<Character>().maxStamina > 1)
            {
                col.GetComponent<Character>().maxStamina = 1;
            }
            else if (col.GetComponent<Character>().maxStamina > 25)
            {
                col.GetComponent<Character>().TomarDano(25);
                //col.GetComponent<Character>().maxStamina -= 30;
            }
            else
            {
                ////remove jogador se ele cair nos respawns com 1 de vida
                //gManager.RemovePlayer(col.gameObject);
                StartCoroutine(gManager.RemovePlayer(col.gameObject));
            }
        }
    }

}
