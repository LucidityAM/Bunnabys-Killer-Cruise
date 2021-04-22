using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public float shootingTime;

    GameObject EnemyClicked;

    bool isClicked;

    CharacterInfo characterInfo;
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
                    }//Cancels an auto attack if you click somewhere else for a second time
                }
            }//Casts a raycast at the position of the mouse and looks for the collider that it hits

        } //Checks if the enemy has been clicked and starts the auto attack accordingly

    }
    public IEnumerator StartAuto(GameObject EnemyClicked)
    {
        while (isClicked)
        {
            CharacterInfo enemyInfo = EnemyClicked.GetComponent<CharacterInfo>();

            enemyInfo.TakeDamage(characterInfo.damage, characterInfo.critChance);

            Debug.Log(EnemyClicked);

            yield return new WaitForSeconds(shootingTime);
        }
    }
}
