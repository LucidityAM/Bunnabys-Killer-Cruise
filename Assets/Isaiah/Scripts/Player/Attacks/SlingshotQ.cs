using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlingshotQ : MonoBehaviour
{
    //In Script
    Animator anim;
    RaycastHit hit;
    PlayerController moveScript;
    EnemyMovement enemyHit;

    [Header("Slingshot (Q)")]
    public Image slingshotImage;
    public float cooldown;
    bool isCooldown = false;
    public KeyCode ability;
    bool canSkillshot = true;
    public GameObject projPrefab;
    public Transform projSpawnPoint;

    //Ability Input Variables
    [Header("Ability Inputs")]
    Vector3 position;
    public Canvas ability1Canvas;
    public Image skillshot;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        skillshot.GetComponent<Image>().enabled = false;

        moveScript = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SkillshotAbility();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ability 1 Input
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        //Ability 1 Canvas Inputs
        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);

        ability1Canvas.transform.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);
    }

    public void SkillshotAbility()
    {
        //Enable the Skillshot Indicator
        if(Input.GetKeyDown(ability) && isCooldown == false)
        {
            skillshot.GetComponent<Image>().enabled = true;
        }

        if(skillshot.GetComponent<Image>().enabled == true && Input.GetKeyUp(ability))
        {
            float currentSpeed = moveScript.speed;
            moveScript.speed = 0;

            if (canSkillshot)
            {
                isCooldown = true;
                slingshotImage.fillAmount = 1;

                //Call the Animation
                //StartCoroutine()
            }

        }

    }

    IEnumerator corSlingshotQ()
    {
        canSkillshot = false;
        //anim stuff

        yield return new WaitForSeconds(1.5f);
        SpawnProjectile();
        //anim stuff
    }

    public void SpawnProjectile()
    {
        canSkillshot = true;
        Instantiate(projPrefab, projSpawnPoint.transform.position, projSpawnPoint.transform.rotation);
    }
}
