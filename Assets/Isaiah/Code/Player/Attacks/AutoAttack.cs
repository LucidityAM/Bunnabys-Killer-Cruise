using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public float shootingTime;

    GameObject EnemyClicked;

    bool isClicked;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) && !isClicked)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
                
            if(Physics.Raycast(ray.origin, ray.direction * 10, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.collider);
            }

            

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Enemy") && !isClicked)
                {
                    isClicked = true;
                    StartAuto(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.tag != "Enemy" && isClicked)
                {
                    isClicked = false;
                }//Cancels an auto attack if you click on an enemy for a second time
            }
        } //Checks if the enemy has been clicked and starts the auto attack accordingly

    }
    public void StartAuto(GameObject EnemyClicked)
    {
        Debug.Log(EnemyClicked);

        if (isClicked)
        {
            if (Time.time > shootingTime)
            {
                shootingTime = Time.time * Time.deltaTime;
                Debug.Log(EnemyClicked);
            }
        }
    }
}
