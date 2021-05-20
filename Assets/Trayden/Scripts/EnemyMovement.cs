using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Collider[] hitColliders;
    public bool hasBeenHit = false;
    public bool iFramesRunning = false;
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 2f, gameObject.transform.position.z);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);

        hitColliders = Physics.OverlapSphere(this.transform.position, 5f);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i] != null)
            {
                if (hitColliders[i].gameObject.CompareTag("Player"))
                {
                    speed = 0;
                    if(hasBeenHit == false)
                    {
                        player.GetComponent<CharacterInfo>().TakeDamage(gameObject.GetComponent<CharacterInfo>().damage, 0);
                        hasBeenHit = true;
                    }
                }
                else
                {
                    speed = 9f;
                }
            }
        }

        if(hasBeenHit == true && iFramesRunning == false)
        {
            StartCoroutine("IFrameCooldown");
        }
    }

    public IEnumerator IFrameCooldown()
    {
        iFramesRunning = true;
        yield return new WaitForSeconds(.75f);
        hasBeenHit = false;
        iFramesRunning = false;
    }
}