using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{
    public GameObject[] droppedWeapons;
    public bool isRandom = true;

    string selectedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon(isRandom);
    }


    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "TailHand")
        {
            //col.GetComponent<Character>().equippedWeapon = selectedWeapon;
            EquipAndVanish(col.gameObject);
        }
    }

    void SelectWeapon(bool isRandom)
    {
        if (!isRandom)
        {
            //PENSAR MELHOR
        }
        else
        {
            int random = droppedWeapons.Length;

            GameObject selected = droppedWeapons[Random.Range(0, random)];
            selectedWeapon = selected.tag;
            selected.SetActive(true);
        }
    }

    public string SelectedWeapon()
    {
        if(selectedWeapon != null)
        {
            return selectedWeapon;
        }
        else
        {
            return "none";
        }
    }

    void EquipAndVanish(GameObject obj)
    {
        obj.GetComponentInParent<Character>().equippedWeapon = selectedWeapon;
        Destroy(this.gameObject);
    }
}
