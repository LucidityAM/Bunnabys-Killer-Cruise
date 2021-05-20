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
    public WaveSystem waveSystem;
    public GameObject[] enemies;
    public float x;
    public float z;
    public int roundNumber = 1;
    private int totalCycles = 0;
    private bool isRoundOngoing = false;
    void Update()
    {
        if(waveSystem.roundState == 1 && isRoundOngoing == false)
        {
            StartCoroutine(RoundGen(roundNumber));
        }
        if(enemies == null)
        {
            waveSystem.roundState = 2;
            roundNumber++;
            isRoundOngoing = false;
        }
    }
    public IEnumerator RoundGen(int roundNumber)
    {
        if(roundNumber == 1 || roundNumber == 2)
        {
            StartCoroutine(SpawnRound(roundNumber));
        }
        else if(roundNumber == 3 || roundNumber == 4)
        {
            StartCoroutine(SpawnRound(roundNumber + 1));
        }
        else if(roundNumber == 5 || roundNumber == 6)
        {
            StartCoroutine(SpawnRound(roundNumber + 2));
        }
        else
        {
            yield return null;
        }
    }
    public IEnumerator SpawnRound(int enemyAmount)
    {
        x = Random.Range(-20f, 30f);
        z = Random.Range(-47.5f, 29f);
        Instantiate(greenCrew, new Vector3(x, 0.5f, z), Quaternion.Euler(-90f, -90f, 0f));
        totalCycles++;
        if(totalCycles < enemyAmount)
        {
            StartCoroutine(SpawnRound(enemyAmount));
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            totalCycles = 0;
            isRoundOngoing = true;
            yield return null;
        }
    }
}
