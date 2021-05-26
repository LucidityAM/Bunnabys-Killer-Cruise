using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                StartCoroutine("Pause");
            } else 
            {
                StartCoroutine("Resume");
            }
        }
    }

    public IEnumerator Pause()
    {
        pauseMenu.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isPaused = true;
    }

    public IEnumerator Resume()
    {
        yield return new WaitForSeconds(0.2f);
        pauseMenu.SetActive(false);
        isPaused = false;

    }

    public void StartCor(string corName)
    {
        StartCoroutine(corName);
    }
}
