using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpot : MonoBehaviour
{
    public float damage;
    
    void Update()
    {
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void OnTriggerStay(Collider collision) 
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterInfo>().TakeDamage(.5f, 0);
        }
    }
}
