using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelect : MonoBehaviour
{
    public GameObject abilityCanvas;
    public GameObject dashButton;
    public GameObject fieldButton;
    public GameObject laserButton;

    public Animator selectTextAnim;
    private Animator dashButtonAnim;
    private Animator fieldButtonAnim;
    private Animator laserButtonAnim;

    private bool menuOpen;
    private bool closing;

    //0 = dash, 1 = field, 2 = laser
    private int ability;
    // Start is called before the first frame update
    void Start()
    {
        dashButtonAnim = dashButton.GetComponent<Animator>();
        fieldButtonAnim = fieldButton.GetComponent<Animator>();
        laserButtonAnim = laserButton.GetComponent<Animator>();

        abilityCanvas.SetActive(false);
        menuOpen = false;
        closing = false;
        dashButtonAnim.SetBool("isOpen", false);
        fieldButtonAnim.SetBool("isOpen", false);
        laserButtonAnim.SetBool("isOpen", false);
        selectTextAnim.SetBool("isOpen", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OpenSelect();
        }
    }

    void OpenSelect()
    {
        menuOpen = true;
        abilityCanvas.SetActive(true);
        StartCoroutine("DisplayBannersCor");
    }

    void CloseSelect()
    {
        menuOpen = false;
        closing = true;
        StartCoroutine("DisplayBannersCor");
    }

    public IEnumerator DisplayBannersCor()
    {
        selectTextAnim.SetBool("isOpen", menuOpen);
        yield return new WaitForSeconds(0.4f);
        dashButtonAnim.SetBool("isOpen", menuOpen);
        yield return new WaitForSeconds(0.3f);
        fieldButtonAnim.SetBool("isOpen", menuOpen);
        yield return new WaitForSeconds(0.3f);
        laserButtonAnim.SetBool("isOpen", menuOpen);
        yield return new WaitForSeconds(0.3f);
        if (closing == true) { abilityCanvas.SetActive(false); closing = false; }
    }

    //0 = dash, 1 = field, 2 = laser
    public void ChooseAbility(int abilityInput)
    {
        ability = abilityInput;
        CloseSelect();
    }
}
