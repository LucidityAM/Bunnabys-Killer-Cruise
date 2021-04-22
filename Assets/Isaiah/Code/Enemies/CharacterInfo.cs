using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    
    //All basic publically accessible stats
    public float health;
    public float attackSpeed;
    public float range;
    public float armor;
    public float damage;
    public float critChance;

    //Private float critMultipler
    float critMultiplier = 1.5f;

    //A random readonly varaiable
    private static readonly System.Random getrandom = new System.Random();

    // Update is called once per frame
    void Update()
    {
        DeathChecker();
    }

    public void TakeDamage(float damageDealt, float opposingCritChance)
    {
        System.Random rnd = new System.Random();
        if(rnd.Next(0, 100) < opposingCritChance)
        {
            health -= (damageDealt - armor) * critMultiplier;
        }
        else
        {
            health -= (damageDealt - armor);
        }
    }


    public void DeathChecker()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
