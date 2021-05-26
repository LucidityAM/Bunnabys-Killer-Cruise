using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSystem : MonoBehaviour
{
    public AbilitySelect abilitySelect;
    public Animator startWaveButton;
    public EnemySpawning enemySpawnScript;

    public GameObject player;


    //0 = pre, 1 = round, 2 = post
    public int roundState;

    private bool opened;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        roundState = 0;
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (roundState == 0)
        {
            PreRound();
        } else if (roundState == 1)
        {
            Round();
        } else
        {
            PostRound();
        }
    }

    public void PreRound()
    {
        if (opened == false) { abilitySelect.OpenSelect(); opened = true; }
        startWaveButton.SetBool("isOpen", true);

        player.GetComponent<CharacterInfo>().health = player.GetComponent<CharacterInfo>().maxHealth;
    }

    void Round()
    {
        startWaveButton.SetBool("isOpen", false);
    }

    void PostRound()
    {
        startWaveButton.SetBool("isOpen", false);
    }


    public void AdvanceRoundState()
    {
        if (roundState >= 2)
        {
            roundState = 0;
            opened = false;
        }
        else
        {
            roundState += 1;
        }

        enemySpawnScript.roundNumber = 1;
    }
    
    public void StartWave()
    {
        enemySpawnScript.waveNumber++;
        if (enemySpawnScript.waveNumber == 1)
        {
            enemySpawnScript.crewmate = enemySpawnScript.greenCrew;
        }
        else if (enemySpawnScript.waveNumber == 2)
        {
            enemySpawnScript.crewmate = enemySpawnScript.blueCrew;
        }
        else if (enemySpawnScript.waveNumber == 3)
        {
            enemySpawnScript.crewmate = enemySpawnScript.redCrew;
        }
        else
        {
            SceneManager.LoadScene("Credits");
        }
        enemySpawnScript.roundNumber = 1;
        roundState = 1;
    }
}