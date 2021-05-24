using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject waveSystem;
    public float speed;
    public Collider[] hitColliders;
    public bool hasBeenHit = false;
    public bool iFramesRunning = false;
    public bool hasShot = false;
    public bool hasSpawnedPuddle = false;
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waveSystem = GameObject.FindGameObjectWithTag("WaveSystem");
    }
    void Update()
    {
        if(gameObject.name != "Coconut Crab(Clone)")
        {        
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
            if(gameObject.transform.position.y < 3)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z);
            }
        }
        else if (gameObject.name == "Coconut Crab(Clone)" && hasShot == false)
        {
            Instantiate(waveSystem.GetComponent<EnemySpawning>().cocoBullet, 
            new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z), Quaternion.identity);
            waveSystem.GetComponent<EnemySpawning>().cocoBullet.GetComponent<CocoProjectile>().targetPos = player.transform.position;
            hasShot = true;
            StartCoroutine("IFrameCooldown");
        }
        else if(gameObject.name == "Watermelon Cat(Clone)" && hasSpawnedPuddle == false)
        {
            Instantiate(waveSystem.GetComponent<EnemySpawning>().acidPuddle, 
            new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z), Quaternion.identity);
            hasSpawnedPuddle = true;
            StartCoroutine("IFrameCooldown");
        }

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
        hasShot = false;
        hasBeenHit = false;
        hasSpawnedPuddle = false;
        iFramesRunning = false;
    }
}