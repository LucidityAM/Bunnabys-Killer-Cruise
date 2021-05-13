using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject enemySpawner;
    public float speed;
    public void Awake() 
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(gameObject.GetComponent<CharacterInfo>().health < 100)
        {
            enemySpawner.GetComponent<EnemySpawning>().StartCoroutine("SpawnEnemy");
            Destroy(gameObject);
        }

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
