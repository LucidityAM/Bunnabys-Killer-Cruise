using UnityEngine;
using System.Collections;

public class DetectSpriteLocation : MonoBehaviour
{
    private bool isInFront;

    public GameObject player;

    private Renderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector3 directionToTarget = transform.position - player.transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        float distance = directionToTarget.magnitude;

        if (Mathf.Abs(angle) < 90 && distance < 10)
        {
            ObjectInFront();
        }
        else if ((Mathf.Abs(angle) > 90 && distance > 5))
        {
            ObjectBehind();
        }
    }

    void ObjectBehind()
    {
        _renderer.sortingLayerName= "Behind";
    }
    void ObjectInFront()
    {
        _renderer.sortingLayerName = "InFront";
    }
}