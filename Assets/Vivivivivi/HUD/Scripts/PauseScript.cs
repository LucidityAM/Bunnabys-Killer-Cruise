using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    #region things that need to be turned off 
    public List<MonoBehaviour> pausedScripts;
    #endregion

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
        ChangeScripts();
        pauseMenu.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isPaused = true;
    }

    public IEnumerator Resume()
    {
        ChangeScripts();
        yield return new WaitForSeconds(0.5f);
        pauseMenu.SetActive(false);
        isPaused = false;
        Debug.Log("gaming");
    }

    public void StartCor(string corName)
    {
        StartCoroutine(corName);
    }
    public void ChangeScripts()
    {
        foreach(MonoBehaviour mono in pausedScripts)
        {
            mono.enabled = isPaused;
        }
    }
}
