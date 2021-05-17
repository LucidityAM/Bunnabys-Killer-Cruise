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

    //Private float critMultipler
    float critMultiplier = 1.5f;

    //A random readonly varaiable
    private static readonly System.Random getrandom = new System.Random();
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

        if (health <= 0)
        {
            Destroy(gameObject);  
        }
    }

}
