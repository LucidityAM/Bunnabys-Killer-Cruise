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
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    public IEnumerator SpawnEnemy(int enemyAmount, bool spawnGreenCrew, bool spawnBlueCrew, bool spawnRedCrew, bool spawnCocoCrab, bool spawnWaterCat)
    {
        x = Random.Range(-20f, 30f);
        z = Random.Range(-47.5f, 29f);
        Instantiate(greenCrew, new Vector3(x, 0.5f, z), Quaternion.Euler(-90f, -90f, 0f));
        yield return null;
    }
}
