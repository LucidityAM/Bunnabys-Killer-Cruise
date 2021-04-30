using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialWeapon1 : MonoBehaviour
{
    [Header("Player Movement")]
    PlayerController playerController;
    public float dashSpeed;
    public float dashTime;
    public float dashAmount = 3;
    

    [Header("Telegraphing")]
    Vector3 position;
    public Image WeaponTelegraphing;

    [Header("Cooldown")]
    bool isCooldown;
    bool canDash = true;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        
    }

    public void DashAbility()
    {
        if(Input.GetKey(KeyCode.R) && isCooldown == false)
        {
            WeaponTelegraphing.GetComponent<Image>().enabled = true;
        }

        if (WeaponTelegraphing.GetComponent<Image>().enabled == true && Input.GetKeyUp(KeyCode.R))
        {
            if (canDash & dashAmount > 0)
            {
                isCooldown = true;

                //StartCoroutine()
            }

        }

    }
}
