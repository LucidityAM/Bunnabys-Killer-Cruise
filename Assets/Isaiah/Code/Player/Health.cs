using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Slider playerSlider2D;

    public CharacterInfo characterInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerSlider2D = GetComponent<Slider>();

        playerSlider2D.maxValue = characterInfo.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider2D.value = characterInfo.health;
    }
}
