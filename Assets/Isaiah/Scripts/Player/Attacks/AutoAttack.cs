using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public float shootingTime;

    GameObject EnemyClicked;

    bool isClicked;

    CharacterInfo characterInfo;

    public float Range;

    public float meleeMultiplier;

    public CharacterInfo enemyInfo;

    public bool isUsingAbility;

    public Animator pAnim;
    // Start is called before the first frame update
    void Awake()
    {
        characterInfo = GetComponent<CharacterInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) && !isClicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Makes a ray at the mouse position
            RaycastHit hit; //Makes a raycast hit

                
            if(Physics.Raycast(ray.origin, ray.direction * 10, out hit, Mathf.Infinity))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("Enemy") && !isClicked)
                    {
                        isClicked = true;
                        StartCoroutine(StartAuto(hit.collider.gameObject));
                    }
                    else if (hit.collider.gameObject.tag != "Enemy" && isClicked)
                    {
                        isClicked = false;
                        pAnim.SetBool("isShooting", false);
                        pAnim.SetBool("isKnifing", false);
                    }//Cancels an auto attack if you click somewhere else for a second time
                }
            }//Casts a raycast at the position of the mouse and looks for the collider that it hits

        } //Checks if the enemy has been clicked and starts the auto attack accordingly

    }
    public IEnumerator StartAuto(GameObject EnemyClicked)
    {
        if (StaticVars.isUsingAbility != true)
        {
            if (EnemyClicked != null)
            {
                while (isClicked)
                {
                    if (EnemyClicked != null)
                    {
                        Vector3 directionToTarget = EnemyClicked.transform.position - gameObject.transform.position;
                        float distance = directionToTarget.magnitude;

                        if (distance > Range)
                        {
                            pAnim.SetBool("isShooting", true);
                            pAnim.SetBool("isKnifing", false);

                            enemyInfo = EnemyClicked.GetComponent<CharacterInfo>();

                            enemyInfo.TakeDamage(characterInfo.damage, characterInfo.critChance);

                            yield return new WaitForSeconds(shootingTime);
                        }
                        else if (distance < Range)
                        {
                            pAnim.SetBool("isKnifing", true);
                            pAnim.SetBool("isShooting", false);

                            enemyInfo = EnemyClicked.GetComponent<CharacterInfo>();

                            enemyInfo.TakeDamage(characterInfo.damage * meleeMultiplier, characterInfo.critChance);

                            yield return new WaitForSeconds(shootingTime);
                        }
                        else
                        {
                            pAnim.SetBool("isShooting", false);
                            pAnim.SetBool("isKnifing", false);
                        }
                    }
                    else
                    {
                        pAnim.SetBool("isShooting", false);
                        pAnim.SetBool("isKnifing", false);
                        yield break;
                    }
                }
            }
            else
            {
                pAnim.SetBool("isShooting", false);
                pAnim.SetBool("isKnifing", false);
                yield break;
            }
        }
        else
        {
            yield break;
        }
    }
    
}
