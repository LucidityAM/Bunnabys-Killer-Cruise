using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject spawnItem;
    public void Awake() 
    {
        spawnItem = GameObject.FindGameObjectWithTag("EnemySpawner");
    }

    void Update()
    {
        if(gameObject.GetComponent<CharacterInfo>().health < 100)
        {
            spawnItem.GetComponent<EnemySpawning>().StartCoroutine("SpawnEnemy");
        }
    }
}
