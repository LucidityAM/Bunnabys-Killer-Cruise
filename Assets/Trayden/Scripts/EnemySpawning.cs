using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject greenCrew;
    public GameObject blueCrew;
    public GameObject redCrew;
    public GameObject cocoCrab;
    public GameObject waterCat;
    public float x;
    public float z;
    public int enemyAmount = 1;
    private int cycleAmount = 0;
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }
    public IEnumerator SpawnEnemy()
    {
        x = Random.Range(-20f, 30f);
        z = Random.Range(-47.5f, 29f);
        Instantiate(greenCrew, new Vector3(x, 0.5f, z), Quaternion.Euler(-90f, -90f, 0f));
        cycleAmount++;
        if(cycleAmount < enemyAmount)
        {
            StartCoroutine("SpawnEnemy");
        }
        else
        {

        }
        yield return null;
    }
}
