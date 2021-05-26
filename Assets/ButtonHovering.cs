using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHovering : MonoBehaviour
{
    public Sprite hoveredSprite;

    private Sprite pastSprite;
    // Start is called before the first frame update
    void Start()
    {
        pastSprite = gameObject.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplaySprite()
    {
        gameObject.GetComponent<Image>().sprite = hoveredSprite;
    }
    public void RemoveSprite()
    {
        gameObject.GetComponent<Image>().sprite = pastSprite;
    }

}
