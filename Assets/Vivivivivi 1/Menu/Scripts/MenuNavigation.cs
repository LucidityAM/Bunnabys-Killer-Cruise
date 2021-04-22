using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    #region variables 
    //The major menu object
    public GameObject menu;

    //Three major menus for the pause menu of the game
    public GameObject inventoryMenu;
    public GameObject mapMenu;
    public GameObject systemMenu;

    //bools 
    public bool isOpen;
    #endregion
    private void Awake()
    {
        menu.SetActive(false);
        inventoryMenu.SetActive(false);
        mapMenu.SetActive(false);
        systemMenu.SetActive(false);

        isOpen = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isOpen)
        {
            StartCoroutine("OpenMenu");
        } else if(Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            StartCoroutine("CloseMenu");
        }
    }


    //this method is for unity to access the IEnumerators because 
    //I am a horrible programmer that does not know of an alternative to this
    public void TriggerMethod(string methodName)
    {
        StartCoroutine(methodName);
    }

    public IEnumerator OpenMenu()
    {
        isOpen = true;
        //will feature an animation for opening the journal
        menu.SetActive(true);
        yield return null;
        inventoryMenu.SetActive(true);
    }

    public IEnumerator CloseMenu()
    {
        isOpen = false;
        inventoryMenu.SetActive(false);
        mapMenu.SetActive(false);
        systemMenu.SetActive(false);
        //will feature an animation for opening the journal
        menu.SetActive(false);
        yield return null;
    }

    //visual animation for the books are in the three lower methods
    //while the aniamtion happens, current menus are cleared 
    //the moment the animation is over the current menu shows up
    public IEnumerator OpenInventory()
    {
        mapMenu.SetActive(false);
        systemMenu.SetActive(false);
        yield return null;
        inventoryMenu.SetActive(true);
    }

    public IEnumerator OpenMap()
    {
        inventoryMenu.SetActive(false);
        systemMenu.SetActive(false);
        yield return null;
        mapMenu.SetActive(true);
    }

    public IEnumerator OpenSystem()
    {
        inventoryMenu.SetActive(false);
        mapMenu.SetActive(false);
        yield return null;
        systemMenu.SetActive(true);
    }
}
