using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    void Update()
    {
        if(gameObject.GetComponent<CharacterInfo>().health < 100)
        {
            Destroy(gameObject);
        }
    }
}
