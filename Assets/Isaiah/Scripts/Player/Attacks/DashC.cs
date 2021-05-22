using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashC : MonoBehaviour
{
    public Animator pAnim;

    //In Script
    Animator anim;
    RaycastHit hit;
    PlayerController moveScript;
    EnemyMovement enemyHit;
    public GameObject dashLines;

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

    public float damage;

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
            StaticVars.isUsingAbility = true;
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;

        }

        if (Input.GetKeyUp(ability))
        {
            StaticVars.isUsingAbility = false;
            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
        }


        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            StaticVars.isUsingAbility = false;
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
        StartCoroutine(MovementDash());

        pAnim.SetBool("isSlicing", true);
        yield return new WaitForSeconds(0.5f);
        KnockUp();

        //anim stuff
        moveScript.speed = currentSpeed;
    }

    public IEnumerator MovementDash()
    {
        float dashAmount = 200f;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        dashLines.SetActive(true);

        Quaternion transRot = Quaternion.LookRotation(this.transform.position - position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);

        dashLines.transform.rotation = Quaternion.Lerp(transRot, dashLines.transform.rotation, 0f);

        Vector3 moveDirection = (transform.position - position);
        moveDirection.y = 0;
        moveDirection.Normalize();

        moveScript.r.AddForce(-moveDirection * dashAmount, ForceMode.VelocityChange);

        yield return new WaitForSeconds(.2f);

        dashLines.SetActive(false);
    }

    void KnockUp()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 7);

        pAnim.SetBool("isSlicing", false);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.CompareTag("Enemy"))
            {
                if (hitColliders[i].isTrigger)
                {
                    hitColliders[i].gameObject.GetComponent<CharacterInfo>().TakeDamage(damage, 0);
                    StartCoroutine(CC(hitColliders[i].gameObject));
                }
            }
        }
    }

    public IEnumerator CC(GameObject enemy)
    {
        if(enemy != null)
        {
            enemy.GetComponent<EnemyMovement>().enabled = false;
        }
        
        yield return new WaitForSeconds(.5f);

        if (enemy != null)
        {
            enemy.GetComponent<EnemyMovement>().enabled = true;
        }
    }
}

