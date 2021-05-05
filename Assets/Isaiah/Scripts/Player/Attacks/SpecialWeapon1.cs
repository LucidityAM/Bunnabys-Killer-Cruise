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
    GameObject player;
    

    [Header("Telegraphing")]
    Vector3 position;
    public Image weaponTelegraphing;

    [Header("Cooldown")]
    bool isCooldown;
    bool canDash = true;
    public float cooldown = 10;
    public Image cooldownImage;

    void Start()
    {
        player = this.gameObject;
        weaponTelegraphing.GetComponent<Image>().enabled = false;

        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        DashAbility();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        //Ability 1 Canvas Inputs
        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);

        weaponTelegraphing.transform.rotation = Quaternion.Lerp(transRot, weaponTelegraphing.transform.rotation, 0f);
    }

    public void DashAbility()
    {
        if(Input.GetKey(KeyCode.R) && isCooldown == false)
        {
            weaponTelegraphing.GetComponent<Image>().enabled = true;
        }

        if (weaponTelegraphing.GetComponent<Image>().enabled == true && Input.GetKeyUp(KeyCode.R))
        {
            if (dashAmount > 0)
            {
                dashAmount -= 1;
                cooldownImage.fillAmount = 1;

                StartCoroutine(Dash());
            }
        }

        if (dashAmount != 3)
        {
            cooldownImage.fillAmount += 1 / cooldown * Time.deltaTime;

            if (weaponTelegraphing.fillAmount >= 1)
            {
                weaponTelegraphing.fillAmount = 1;
                dashAmount++;
            }

            if(dashAmount == 0)
            {
                weaponTelegraphing.GetComponent<Image>().enabled = false;

                if (cooldownImage.fillAmount >= 1)
                {
                    cooldownImage.fillAmount = 1;
                    isCooldown = false;
                }
            }
        }
    }

    IEnumerator Dash()
    {
        yield return null;
    }
}
