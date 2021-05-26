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
    public int waveNumber;
    public int roundNumber;
    public int totalCycles;
    public int specialEnemy;
    public bool isRoundOngoing;
    public bool allEnemiesDead;
    public bool specialEnemySpawned;

    public Text waveText;

    void Awake() 
    {
        waveNumber = 0;
        roundNumber = 1;
        totalCycles = 0;
        specialEnemy = 0;
        isRoundOngoing = false;
        allEnemiesDead = false;
        specialEnemySpawned = false;
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
            specialEnemySpawned = false;
            waveSystem.roundState = 1;
        }
        if(roundNumber > 3)
        {
            waveSystem.roundState = 0;
            if(waveNumber + 1 == 4)
            {
                SceneManager.LoadScene("Credits");
            }
            else
            {
                waveSystem.roundState = 0;
            }
        }
        if(waveNumber == 0)
        {
            waveText.text = "1 / 3";
        }
        else if(waveNumber == 4)
        {
            waveText.text = "3 / 3";
        }
        else
        {
            waveText.text = waveNumber + " / 3";
        }
    }
    public IEnumerator RoundGen(int roundNumber)
    {
        if(roundNumber == 1)
        {
            StartCoroutine("RandomiseSpecialEnemy");
            StartCoroutine(SpawnRound(roundNumber, specialEnemy));
        }
        else if(roundNumber == 2)
        {
            StartCoroutine("RandomiseSpecialEnemy");
            StartCoroutine(SpawnRound(roundNumber + 1, specialEnemy));
        }
        else if(roundNumber == 3)
        {
            StartCoroutine("RandomiseSpecialEnemy");
            StartCoroutine(SpawnRound(roundNumber + 2, specialEnemy));
        }
        else
        {
            yield return null;
        }
    }
    public IEnumerator RandomiseSpecialEnemy()
    {
        specialEnemy = Random.Range(0, 2);
        yield return null;
    }
    public IEnumerator SpawnRound(int enemyAmount, int specialEnemyIdentity)
    {
        x = Random.Range(-20f, 30f);
        z = Random.Range(-47.5f, 29f);
        Instantiate(crewmate, new Vector3(x, .5f, z), Quaternion.Euler(-90f, -90f, 0f));
        if(waveNumber >= 2 && specialEnemyIdentity == 0 && specialEnemySpawned == false)
        {
            x = Random.Range(-20f, 30f);
            z = Random.Range(-47.5f, 29f);
            Instantiate(cocoCrab, new Vector3(x, .5f, z), Quaternion.Euler(-90f, -90f, 0f));
            specialEnemySpawned = true;
        }
        else if(waveNumber >= 2 && specialEnemyIdentity == 1 && specialEnemySpawned == false)
        {
            x = Random.Range(-20f, 30f);
            z = Random.Range(-47.5f, 29f);
            Instantiate(waterCat, new Vector3(x, .5f, z), Quaternion.Euler(-90f, -90f, 0f));
            specialEnemySpawned = true;
        }
        totalCycles++;
        if(totalCycles < enemyAmount)
        {
            StartCoroutine(SpawnRound(enemyAmount, specialEnemy));
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
            waveSystem.roundState = 2;
        }
        else
        {
            return;
        }
    }
}
