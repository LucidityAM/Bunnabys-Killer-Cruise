using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class SpecialWeapon2 : MonoBehaviour
{
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


    void Start()
    {
        isClearing = false;
        canDeathField = true;
        cooldown = 15;
        player = this.gameObject;
        weaponTelegraphing.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        DeathAbility();

        if (usingDeathField)
        {
            hitColliders = Physics.OverlapSphere(this.transform.position, 11);

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
        if (Input.GetKey(KeyCode.R))
        {
            weaponTelegraphing.GetComponent<Image>().enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            weaponTelegraphing.GetComponent<Image>().enabled = false;
        }

        if (weaponTelegraphing.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0) && usingDeathField == false)
        {
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
        weaponTelegraphing.GetComponent<Image>().enabled = false;
        usingDeathField = true;
        
        GameObject DeathAreaPrefab = Instantiate(DeathArea, this.gameObject.transform.localPosition, this.gameObject.transform.rotation);

        yield return new WaitForSeconds(5f);

        Destroy(DeathAreaPrefab);
        hasBeenHealed = false;

        cooldownImage.fillAmount = 0;
        cooldownBorder.fillAmount = 0;
        canDeathField = false;
        isCooldown = true;
        usingDeathField = false;
        isClearing = true;
    }

}
