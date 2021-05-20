using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Collider[] hitColliders;
    public GameObject waveSystem;
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hitColliders = Physics.OverlapSphere(this.transform.position, 3.5f);
        waveSystem = GameObject.FindGameObjectWithTag("WaveSystem");
    }
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
        if(gameObject.GetComponent<CharacterInfo>().health <= 0)
        {
            gameObject.tag = "DeadEnemy";
            waveSystem.GetComponent<EnemySpawning>().enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Destroy(gameObject);
        }

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i] != null)
            {
                if (hitColliders[i].gameObject.CompareTag("Player"))
                {
                    speed = 0;
                }
                else
                {
                    speed = 15;
                }
            }
        }
    }
}