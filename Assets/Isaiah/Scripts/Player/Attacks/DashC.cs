using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashC : MonoBehaviour
{
    //In Script
    Animator anim;
    RaycastHit hit;
    PlayerController moveScript;
    EnemyMovement enemyHit;

    [Header("Grand Entrance (C)")]
    public Image dashImage; public Image dashBorderImage;
    public float cooldown;
    bool isCooldown = false;
    public KeyCode ability;
    bool canDash = true;


    //Ability Input Variables
    [Header("Ability Inputs")]
    Vector3 position;
    public Image targetCircle;
    public Image indicatorRangeCircle;
    public Canvas dashCanvas;
    private Vector3 posUp;
    public float maxDashDistance;

    // Start is called before the first frame update
    void Start()
    {
        dashImage.fillAmount = 1;
        dashBorderImage.fillAmount = 1;


        targetCircle.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;

        moveScript = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Dash();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ability 1 Input
        if(Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            if(hit.collider.gameObject != this.gameObject)
            {
                posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                position = hit.point;
            }
        }

        //Ability 1 Canvas Inputs
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxDashDistance);

        var newHitPos = transform.position + hitPosDir * distance;
        dashCanvas.transform.position = (newHitPos);

    }

    public void Dash()
    {
        //Enable the Skillshot Indicator
        if (Input.GetKeyDown(ability) && isCooldown == false)
        {
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;

        }

        if (Input.GetKeyUp(ability))
        {
            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
        }


        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {

            if (canDash)
            {
                //Call the Animation
                StartCoroutine("corDashC");
            }

        }

        if (isCooldown)
        {
            dashImage.fillAmount += 1 / cooldown * Time.deltaTime;
            targetCircle.GetComponent<Image>().enabled = false;

            if (dashImage.fillAmount >= 1)
            {
                dashImage.fillAmount = 1;
                dashBorderImage.fillAmount = 1;
                isCooldown = false;
                canDash = true;
            }
        }
    }

    public IEnumerator corDashC()
    {
        float currentSpeed = moveScript.speed;
        moveScript.speed = 0;
        canDash = false;
        dashImage.fillAmount = 0;
        dashBorderImage.fillAmount = 0;
        isCooldown = true;
        //anim stuff

        yield return new WaitForSeconds(.2f);
        MovementDash();
        yield return new WaitForSeconds(0.5f);
        KnockUp();

        //anim stuff
        moveScript.speed = currentSpeed;
    }

    public void MovementDash()
    {
        float dashAmount = 150f;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        Vector3 moveDirection = (transform.position - position);
        moveDirection.y = 0;
        moveDirection.Normalize();

        moveScript.r.AddForce(-moveDirection * dashAmount, ForceMode.Impulse);
    }

    void KnockUp()
    {

    }
}

