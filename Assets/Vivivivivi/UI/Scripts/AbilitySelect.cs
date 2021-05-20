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
    public GameObject player;

    public Animator selectTextAnim;
    private Animator dashButtonAnim;
    private Animator fieldButtonAnim;
    private Animator laserButtonAnim;

    public Image rAbilityImage, rAbilityImageFill;
    public GameObject dashText;

    private SpecialWeapon1 dash;
    private SpecialWeapon2 field;
    private SpecialWeapon3 laser;

    public Sprite[] rSprites;

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

        dash = player.GetComponent<SpecialWeapon1>();
        field = player.GetComponent<SpecialWeapon2>();
        laser = player.GetComponent<SpecialWeapon3>();

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

    public void OpenSelect()
    {
        menuOpen = true;
        abilityCanvas.SetActive(true);
        Cursor.visible = true;
        StartCoroutine("DisplayBannersCor");
    }

    public void CloseSelect()
    {
        menuOpen = false;
        closing = true;
        StartCoroutine("DisplayBannersCor");
        Cursor.visible = false;
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
        SetAbility();
        CloseSelect();
    }

    public void SetAbility()
    {
        rAbilityImage.sprite = rSprites[ability];
        rAbilityImageFill.sprite = rSprites[ability];

        if(ability == 0)
        {
            dash.enabled = true;
            field.enabled = false;
            laser.enabled = false;
            dashText.SetActive(true);

        } else if(ability == 1)
        {
            dash.enabled = false;
            field.enabled = true;
            laser.enabled = false;
            dashText.SetActive(false);
        }
        else
        {
            dash.enabled = false;
            field.enabled = false;
            laser.enabled = true;
            dashText.SetActive(false);
        }
    }
}
