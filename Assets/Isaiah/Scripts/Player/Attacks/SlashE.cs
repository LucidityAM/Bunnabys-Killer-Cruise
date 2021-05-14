using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlashE : MonoBehaviour
{
    //In Script
    Animator anim;
    RaycastHit hit;
    PlayerController moveScript;
    EnemyMovement enemyHit;
    
    [Header("Slash (E)")]
    public Image slashImage; public Image borderImage;
    public float cooldown;
    bool isCooldown = false;
    public KeyCode ability;
    bool canSlash = true;

    //Ability Input Variables
    [Header("Ability Inputs")]
    Vector3 position;
    public Canvas slashCanvas;
    public Image slashRangeIndicator;
    public Transform player;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        slashImage.fillAmount = 1;
        borderImage.fillAmount = 1;

        slashRangeIndicator.GetComponent<Image>().enabled = false;

        moveScript = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SlashAbility();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ability 1 Input
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

    }

    void SlashAbility()
    {
        //Enable the Skillshot Indicator
        if (Input.GetKeyDown(ability) && isCooldown == false)
        {
            slashRangeIndicator.GetComponent<Image>().enabled = true;
        }

        if (Input.GetKeyUp(ability))
        {
            slashRangeIndicator.GetComponent<Image>().enabled = false;
        }

        if (slashRangeIndicator.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {

            if (canSlash)
            {

                //Call the Animation
                StartCoroutine("corSlashE");
            }

        }

        if (isCooldown)
        {
            slashImage.fillAmount += 1 / cooldown * Time.deltaTime;
            slashRangeIndicator.GetComponent<Image>().enabled = false;

            if (slashImage.fillAmount >= 1)
            {
                slashImage.fillAmount = 1;
                borderImage.fillAmount = 1;
                isCooldown = false;
            }
        }
    }

    IEnumerator corSlashE()
    {
        float currentSpeed = moveScript.speed;
        moveScript.speed = 0;
        canSlash = false;
        slashImage.fillAmount = 0;
        borderImage.fillAmount = 0;
        isCooldown = true;
        //anim stuff

        yield return new WaitForSeconds(.5f);
        Slash();

        //anim stuff
        moveScript.speed = currentSpeed;
    }

    void Slash()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 5);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.CompareTag("Enemy"))
            {
                //Debug.Log(hitColliders[i].gameObject);
                if (hitColliders[i].isTrigger)
                {
                    hitColliders[i].gameObject.GetComponent<CharacterInfo>().TakeDamage(damage, 0);
                }
            }
        }
    }


}
