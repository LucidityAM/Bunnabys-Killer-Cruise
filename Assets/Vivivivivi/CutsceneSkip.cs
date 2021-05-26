using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneSkip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("EndCutscene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }

    public IEnumerator EndCutscene()
    {
        yield return new WaitForSeconds(33f);
        SceneManager.LoadScene(1);
    }
}
