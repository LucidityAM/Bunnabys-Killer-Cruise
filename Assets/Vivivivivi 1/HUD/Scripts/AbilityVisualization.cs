using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityVisualization : MonoBehaviour
{


    [Header("Q")]
    public Image imageQ;
    private float cooldownQ;
    private bool onCooldownQ;
    [Header("E")]
    public Image imageE;
    private float cooldownE;
    private bool onCooldownE;
    [Header("C")]
    public Image imageC;
    private float cooldownC;
    private bool onCooldownC;
    [Header("R")]
    public Image imageR;
    private float cooldownR;
    private bool onCooldownR;


    // Start is called before the first frame update
    void Start()
    {
        imageQ.fillAmount = 1;
        imageE.fillAmount = 1;
        imageC.fillAmount = 1;
        imageR.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CooldownQAbility()
    {
        if (onCooldownQ == false)
        {
            imageQ.fillAmount = 1;
        }

        if (onCooldownQ)
        {
            imageQ.fillAmount -= 1 / cooldownQ * Time.deltaTime;

            if(imageQ.fillAmount <= 0)
            {
                imageQ.fillAmount = 0;
                onCooldownQ = false;
            }
        }
    }

    void CooldownEAbility()
    {
        if (onCooldownE == false)
        {
            imageE.fillAmount = 1;
        }

        if (onCooldownE)
        {
            imageE.fillAmount -= 1 / cooldownE * Time.deltaTime;

            if (imageE.fillAmount <= 0)
            {
                imageE.fillAmount = 0;
                onCooldownE = false;
            }
        }
    }

    void CooldownCAbility()
    {
        if (onCooldownC == false)
        {
            imageC.fillAmount = 1;
        }

        if (onCooldownC)
        {
            imageC.fillAmount -= 1 / cooldownC * Time.deltaTime;

            if (imageC.fillAmount <= 0)
            {
                imageC.fillAmount = 0;
                onCooldownC = false;
            }
        }
    }

    void CooldownRAbility()
    {
        if (onCooldownR == false)
        {
            imageR.fillAmount = 1;
        }

        if (onCooldownR)
        {
            imageR.fillAmount -= 1 / cooldownR * Time.deltaTime;

            if (imageR.fillAmount <= 0)
            {
                imageR.fillAmount = 0;
                onCooldownR = false;
            }
        }
    }

}
