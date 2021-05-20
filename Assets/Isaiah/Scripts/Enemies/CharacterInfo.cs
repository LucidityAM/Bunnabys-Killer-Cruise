using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{

    //All basic publically accessible stats
    public float maxHealth;
    public float health;
    public float attackSpeed;
    public float range;
    public float armor;
    public float damage;
    public float critChance;
    public bool isDebuffed;
    bool shakeEnemy;
    public float shakeTimeRemaining, shakePower = 1, shakeFadeTime, shakeRotation;
    public float roatationMultipler = 15;
    public GameObject waveSystem;

    void Awake()
    {
        shakePower = 1;
        waveSystem = GameObject.FindGameObjectWithTag("WaveSystem");
    }

    //Private float critMultipler
    float critMultiplier = 1.5f;

    //A random readonly varaiable
    private static readonly System.Random getrandom = new System.Random();

    void Update()
    {
        if (shakeEnemy == true)
        {
            float xAmount = Random.Range(-0.1f, 0.1f) * shakePower;
            float yAmount = Random.Range(-0.1f, 0.1f) * shakePower;

            gameObject.transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * roatationMultipler * Time.deltaTime);
        }
    }

    public void TakeDamage(float damageDealt, float opposingCritChance)
    {
        System.Random rnd = new System.Random();
        if (rnd.Next(0, 100) < opposingCritChance)
        {
            health -= (damageDealt - armor) * critMultiplier;
        }
        else
        {
            health -= (damageDealt - armor);
        }

        StartCoroutine(ColorFlash());

        if (health <= 0)
        {
            this.gameObject.tag = "DeadEnemy";
            waveSystem.GetComponent<EnemySpawning>().UpdateEnemies();
            Destroy(gameObject); 
        }
    }

    public IEnumerator ColorFlash()
    {
        shakeEnemy = true;
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(.2f);

        shakeEnemy = false;
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
