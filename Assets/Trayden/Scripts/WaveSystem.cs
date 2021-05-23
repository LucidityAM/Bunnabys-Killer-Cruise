﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public AbilitySelect abilitySelect;

    //0 = pre, 1 = round, 2 = post
    public int roundState;

    private bool opened;
    // Start is called before the first frame update
    void Start()
    {
        roundState = 0;
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(roundState == 0)
        {
            PreRound();
        } else if (roundState == 1)
        {
            Round();
        } else
        {
            PostRound();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            roundState = 1;
        }
    }

    public void PreRound()
    {
        if (opened == false) { abilitySelect.OpenSelect(); opened = true; }
    }

    void Round()
    {

    }

    void PostRound()
    {

    }


    public void AdvanceRoundState()
    {
        if (roundState >= 2)
        {
            roundState = 0;
            opened = false;
        }
        else
        {
            roundState += 1;
        }
    }
}