using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class SpecialWeapon1 : MonoBehaviour
{
    public Animator pAnim;

    [Header("Player Movement")]
    PlayerController playerController;
    public float dashAmount;
    GameObject player;
    bool isDashButtonDown;
    public GameObject dashLines;
    

    [Header("Telegraphing")]
    Vector3 position;
    public Image weaponTelegraphing;
    public Canvas SpecialWeapon1Canvas;

    [Header("Cooldown")]
    public bool canDash;
    public float cooldown;
    public Image cooldownImage;
    public Image cooldownBorder;
    public Text remainingCharges;
    public bool isCooldown;
    int timesDashed;

    void Start()
    {
        canDash = true;
        dashAmount = 3;
        player = this.gameObject;
        weaponTelegraphing.GetComponent<Image>().enabled = false;

        playerController = GetComponent<PlayerController>();

        remainingCharges.text = "";
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

        SpecialWeapon1Canvas.transform.rotation = Quaternion.Lerp(transRot, SpecialWeapon1Canvas.transform.rotation, 0f);

        remainingCharges.text = dashAmount.ToString();
        if (remainingCharges.text.Equals("0"))
        {
            remainingCharges.gameObject.SetActive(false);
        } else
        {
            remainingCharges.gameObject.SetActive(true);
        }
    }

    public void DashAbility()
    {
        if(Input.GetKey(KeyCode.R) && dashAmount != 0)
        {
            weaponTelegraphing.GetComponent<Image>().enabled = true;
            StaticVars.isUsingAbility = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            weaponTelegraphing.GetComponent<Image>().enabled = false;
            StaticVars.isUsingAbility = false;
        }

        if (weaponTelegraphing.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            StaticVars.isUsingAbility = false;
            if (dashAmount > 0 && canDash)
            {
                Dash();
            }
        }

        if (isCooldown || dashAmount <= 3)
        {
            cooldownImage.fillAmount += 1 / cooldown * Time.deltaTime;

            if (cooldownImage.fillAmount >= 1)
            {
                cooldownImage.fillAmount = 1;
                cooldownBorder.fillAmount = 1;
                dashAmount += 1;
                
                if(timesDashed == 0)
                {
                    isCooldown = false;
                }
                else if(timesDashed != 0)
                {
                    timesDashed -= 1;
                    cooldownImage.fillAmount = 0;
                    cooldownBorder.fillAmount = 0;
                }
                canDash = true;
            }
        }

        if(dashAmount >= 1)
        {
            cooldownBorder.fillAmount = 1;
        }
    }

    public void Dash()
    {
        timesDashed += 1;

        dashAmount -= 1;

        weaponTelegraphing.GetComponent<Image>().enabled = false;

        cooldownImage.fillAmount = 0;

        StartCoroutine(MovementDash());

        if (dashAmount <= 0)
        {
            canDash = false;
            cooldownBorder.fillAmount = 0;
        }
        isCooldown = true;
    }

    public IEnumerator MovementDash()
    {
        float dashAmount = 150f;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        dashLines.SetActive(true);

        Quaternion transRot = Quaternion.LookRotation(player.transform.position - position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);

        dashLines.transform.rotation = Quaternion.Lerp(transRot, dashLines.transform.rotation, 0f);

        Vector3 moveDirection = (transform.position - position);
        moveDirection.y = 0;
        moveDirection.Normalize();

        playerController.r.AddForce(-moveDirection * dashAmount, ForceMode.Impulse);

        yield return new WaitForSeconds(.2f);

        dashLines.SetActive(false);
    }
}
