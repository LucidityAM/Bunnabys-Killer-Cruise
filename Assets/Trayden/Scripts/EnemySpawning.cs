using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemy;
    public float x;
    public float z;
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    public IEnumerator SpawnEnemy()
    {
        x = Random.Range(-20f, 30f);
        z = Random.Range(-47.5f, 29f);
        Instantiate(enemy, new Vector3(x, 0.5f, z), Quaternion.Euler(-90f, -90f, 0f));
        yield return null;
    }
}
