using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public WaveSystem waveSystem;
    public AudioSource audio;
    public AudioClip preRound;
    public AudioClip battle;

    private bool playedPR;
    private bool playedB;
    // Start is called before the first frame update
    void Start()
    {
        playedPR = false;
        playedB = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(waveSystem.roundState == 0)
        {
            playedB = false;
            audio.clip = preRound;
            if (!playedPR) { audio.Play(); playedPR = true; }
        } else
        {
            playedPR = false;
            audio.clip = battle;
            if (playedB) { audio.Play(); playedB = true; }
        }
    }
}
