using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemySpawning : MonoBehaviour
{
    public GameObject crewmate;
    public GameObject greenCrew;
    public GameObject blueCrew;
    public GameObject redCrew;
    public GameObject cocoCrab;
    public GameObject waterCat;
    public WaveSystem waveSystem;
    public GameObject cocoBullet;
    public GameObject acidPuddle;
    public GameObject[] enemies;
    public float x;
    public float z;
    public int waveNumber = 0;
    public int roundNumber = 1;
    public int totalCycles = 0;
    public int specialEnemy = 0;
    public bool isRoundOngoing = false;
    public bool allEnemiesDead = false;

    public Text waveText;

    void Start() 
    {
        waveNumber = 0;
        crewmate = greenCrew;
    }
    void Update()
    {
        if(waveSystem.roundState == 1 && isRoundOngoing == false)
        {
            StartCoroutine(RoundGen(roundNumber));
        }
        if(allEnemiesDead == true)
        {
            roundNumber++;
            isRoundOngoing = false;
            allEnemiesDead = false;
            waveSystem.roundState = 1;
        }
        if(roundNumber > 6)
        {
            waveSystem.roundState = 0;
        }

        waveText.text = waveNumber + " / 3";
    }
    public IEnumerator RoundGen(int roundNumber)
    {
        if(roundNumber == 1 || roundNumber == 2)
        {
            StartCoroutine("RandomiseSpecialEnemy");
            StartCoroutine(SpawnWave1Round(roundNumber, specialEnemy));
        }
        else if(roundNumber == 3 || roundNumber == 4)
        {
            StartCoroutine("RandomiseSpecialEnemy");
            StartCoroutine(SpawnWave1Round(roundNumber + 1, specialEnemy));
        }
        else if(roundNumber == 5 || roundNumber == 6)
        {
            StartCoroutine("RandomiseSpecialEnemy");
            StartCoroutine(SpawnWave1Round(roundNumber + 2, specialEnemy));
        }
        else
        {
            yield return null;
        }
    }
    public IEnumerator RandomiseSpecialEnemy()
    {
        specialEnemy = Random.Range(1, 2);
        yield return null;
    }
    public IEnumerator SpawnWave1Round(int enemyAmount, int specialEnemyIdentity)
    {
        x = Random.Range(-20f, 30f);
        z = Random.Range(-47.5f, 29f);
        Instantiate(crewmate, new Vector3(x, 0.5f, z), Quaternion.Euler(-90f, -90f, 0f));
        if(waveNumber >= 1 && specialEnemyIdentity == 0)
        {
            x = Random.Range(-20f, 30f);
            z = Random.Range(-47.5f, 29f);
            Instantiate(cocoCrab, new Vector3(x, 0.5f, z), Quaternion.Euler(-90f, -90f, 0f));
        }
        else if(waveNumber >= 1 && specialEnemyIdentity == 1)
        {
            x = Random.Range(-20f, 30f);
            z = Random.Range(-47.5f, 29f);
            Instantiate(waterCat, new Vector3(x, 0.5f, z), Quaternion.Euler(-90f, -90f, 0f));
        }
        totalCycles++;
        if(totalCycles < enemyAmount)
        {
            StartCoroutine(SpawnWave1Round(enemyAmount, specialEnemy));
        }
        else
        {
            UpdateEnemies();
            totalCycles = 0;
            isRoundOngoing = true;
            yield return null;
        }
    }

    public void UpdateEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0)
        {
            allEnemiesDead = true;
        }
        else
        {
            return;
        }
    }
}
