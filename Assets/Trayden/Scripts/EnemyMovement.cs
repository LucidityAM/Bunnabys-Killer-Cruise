using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Collider[] hitColliders;
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);

        hitColliders = Physics.OverlapSphere(this.transform.position, 4.5f);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i] != null)
            {
                if (hitColliders[i].gameObject.CompareTag("Player"))
                {
                    speed = 0;
                }
                else
                {
                    speed = 13.5f;
                }
            }
        }
    }
}