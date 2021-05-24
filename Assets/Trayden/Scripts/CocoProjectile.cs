using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoProjectile : MonoBehaviour
{
    public float damage;
    public float speed;
    public Vector3 targetPos;
    void Update()
    {
        StartCoroutine(DestroyObject());

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterInfo>().TakeDamage(damage, 25);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
