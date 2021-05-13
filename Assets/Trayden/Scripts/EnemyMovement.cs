using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    void Update()
    {
        if(gameObject.GetComponent<CharacterInfo>().health < 100)
        {
            Destroy(gameObject);
        }

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
