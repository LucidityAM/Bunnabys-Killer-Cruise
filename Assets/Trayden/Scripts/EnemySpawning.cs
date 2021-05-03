using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemy;
    public float x;
    public float z;
    public float spawnTime;
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    public IEnumerator SpawnEnemy()
    {
        x = Random.Range(-34.5f, 35);
        z = Random.Range(-50, 34);
        Instantiate(enemy, new Vector3(x, 0.5f, z), Quaternion.Euler(-90f, -90f, 0f));
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine("SpawnEnemy");
    }
}
