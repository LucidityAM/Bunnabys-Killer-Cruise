using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public AbilitySelect abilitySelect;

    //0 = pre, 1 = round, 2 = post
    public int roundState;
    public int currentWave;

    public Wave wave1, wave2, wave3;
    

    // Start is called before the first frame update
    void Start()
    {
        roundState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(roundState == 0)
        {
            PreRound();
        }
    }

    public void PreRound()
    {
        abilitySelect.OpenSelect();
        Debug.Log("gaming");
    }

    void Round()
    {

    }

    void PostRound()
    {

    }

}
