using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialWeapon3 : MonoBehaviour
{
    public Animator pAnim;

    //In Script
    Animator anim;
    RaycastHit hit;

    [Header("SpecialWeapon3")]
    public Image slingshotImage; public Image borderImage;
    public float cooldown;
    bool isCooldown = false;
    bool canSkillshot = true;
    public KeyCode ability;
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
        slingshotImage.fillAmount = 1;
        borderImage.fillAmount = 1;

        skillshot.GetComponent<Image>().enabled = false;

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
        if (Input.GetKeyDown(ability) && isCooldown == false)
        {
            StaticVars.isUsingAbility = true;

            skillshot.GetComponent<Image>().enabled = true;
        }

        if (Input.GetKeyUp(ability))
        {
            StaticVars.isUsingAbility = false;
            skillshot.GetComponent<Image>().enabled = false;
        }


        if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            StaticVars.isUsingAbility = false;

            if (canSkillshot)
            {

                //Call the Animation
                StartCoroutine("corSlingshotQ");
            }

        }

        if (isCooldown)
        {
            slingshotImage.fillAmount += 1 / cooldown * Time.deltaTime;
            skillshot.GetComponent<Image>().enabled = false;

            if (slingshotImage.fillAmount >= 1)
            {
                slingshotImage.fillAmount = 1;
                borderImage.fillAmount = 1;
                isCooldown = false;
            }
        }
    }

    IEnumerator corSlingshotQ()
    {
        canSkillshot = false;
        slingshotImage.fillAmount = 0;
        borderImage.fillAmount = 0;
        isCooldown = true;
        //anim stuff

        pAnim.SetBool("isShooting", true);

        yield return new WaitForSeconds(.3f);
        SpawnProjectile();

        //anim stuff
    }

    public void SpawnProjectile()
    {
        pAnim.SetBool("isShooting", false);
        canSkillshot = true;
        Instantiate(projPrefab, projSpawnPoint.transform.position, projSpawnPoint.transform.rotation);
    }
}
