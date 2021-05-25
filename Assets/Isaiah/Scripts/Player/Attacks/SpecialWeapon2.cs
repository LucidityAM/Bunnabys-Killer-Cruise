using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class SpecialWeapon2 : MonoBehaviour
{
    public Animator pAnim;

    [Header("Player Movement")]
    GameObject player;
    bool isDeathFieldButtonDown;


    [Header("Telegraphing")]
    Vector3 position;
    public Image weaponTelegraphing;
    public GameObject DeathArea;

    [Header("Cooldown")]
    public bool canDeathField;
    public float cooldown;
    public Image cooldownImage;
    public Image cooldownBorder;
    public bool isCooldown;

    public bool usingDeathField;
    bool hasBeenHealed;
    public bool isClearing;

    public Collider[] hitColliders;
    public GameObject DeathAreaPrefab;
    public AudioSource audioSrc;

    void Start()
    {
        isClearing = false;
        canDeathField = true;
        player = this.gameObject;
        usingDeathField = false;
        weaponTelegraphing.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        DeathAbility();

        if (usingDeathField && DeathAreaPrefab != null)
        {
            hitColliders = Physics.OverlapSphere(DeathAreaPrefab.transform.position, 11);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if(hitColliders[i] != null)
                {
                    if (hitColliders[i].gameObject.CompareTag("Enemy"))
                    {
                        if (hitColliders[i].isTrigger)
                        {
                            if (hitColliders[i].GetComponent<CharacterInfo>().isDebuffed == false)
                            {
                                hitColliders[i].GetComponent<EnemyMovement>().speed = (hitColliders[i].GetComponent<EnemyMovement>().speed / 2);
                                hitColliders[i].GetComponent<CharacterInfo>().damage = (hitColliders[i].GetComponent<CharacterInfo>().damage / 2);
                                hitColliders[i].GetComponent<CharacterInfo>().isDebuffed = true;
                            }

                        }
                    }

                    if (hitColliders[i].gameObject.CompareTag("Player"))
                    {
                        if (!hasBeenHealed)
                        {
                            player.GetComponent<CharacterInfo>().health += player.GetComponent<CharacterInfo>().maxHealth / 3;
                            hasBeenHealed = true;
                        }
                    }
                } 
            }
        }

        if (isClearing)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {

                if (hitColliders[i] != null)
                {
                    if (hitColliders[i].gameObject.CompareTag("Enemy"))
                    {
                        if (hitColliders[i].isTrigger && hitColliders[i].GetComponent<CharacterInfo>().isDebuffed == true)
                        {
                            hitColliders[i].GetComponent<EnemyMovement>().speed = (hitColliders[i].GetComponent<EnemyMovement>().speed * 2);
                            hitColliders[i].GetComponent<CharacterInfo>().damage = (hitColliders[i].GetComponent<CharacterInfo>().damage * 2);
                            hitColliders[i].GetComponent<CharacterInfo>().isDebuffed = false;
                        }
                    }
                }

                if (i == hitColliders.Length - 1)
                {
                    isClearing = false;
                }
            }
        }
    }

    public void DeathAbility()
    {
        if (Input.GetKey(KeyCode.R) && canDeathField && usingDeathField == false)
        {
            StaticVars.isUsingAbility = true;
            weaponTelegraphing.GetComponent<Image>().enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            StaticVars.isUsingAbility = false;
            weaponTelegraphing.GetComponent<Image>().enabled = false;
        }

        if (weaponTelegraphing.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0) && usingDeathField == false)
        {
            StaticVars.isUsingAbility = false;
            weaponTelegraphing.GetComponent<Image>().enabled = false;
            StartCoroutine(DeathField());
        }

        if (isCooldown)
        {
            cooldownImage.fillAmount += 1 / cooldown * Time.deltaTime;
            usingDeathField = true;

            if (cooldownImage.fillAmount >= 1)
            {
                cooldownImage.fillAmount = 1;
                cooldownBorder.fillAmount = 1;
                isCooldown = false;
                canDeathField = true;
                usingDeathField = false;
            }
        }


    }

    public IEnumerator DeathField()
    {
        audioSrc.Play();

        weaponTelegraphing.GetComponent<Image>().enabled = false;
        usingDeathField = true;
        
        DeathAreaPrefab = Instantiate(DeathArea, this.gameObject.transform.localPosition, this.gameObject.transform.rotation);
        isCooldown = true;
        cooldownImage.fillAmount = 0;
        cooldownBorder.fillAmount = 0;

        yield return new WaitForSeconds(5f);

        Destroy(DeathAreaPrefab);
        hasBeenHealed = false;

        canDeathField = false;
        usingDeathField = false;
        isClearing = true;
    }

}
