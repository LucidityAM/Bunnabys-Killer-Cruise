using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDescription : MonoBehaviour
{
    [Header("Ability Description Objects")]
    public GameObject prefAbilityDesc;
    public Text aName;
    public Text aDescription;

    [Header("Description")]
    public string dName;
    public string description;
    public float offsetX;
    public float offsetY;


    // Start is called before the first frame update
    void Start()
    {
        prefAbilityDesc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDescription()
    {
        aName.text = "";
        aDescription.text = "";
        aName.text = dName;
        aDescription.text = description;
        prefAbilityDesc.SetActive(true);
        prefAbilityDesc.transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, prefAbilityDesc.transform.position.z);

    }

    public void RemoveDescription()
    {
        aName.text = "";
        aDescription.text = "";
        prefAbilityDesc.SetActive(false);

    }

}
