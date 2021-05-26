using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScript : MonoBehaviour
{
    public GameObject credit;

    // Start is called before the first frame update
    void Start()
    {
        credit.SetActive(false);
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCredits(Sprite creditImage)
    {
        credit.GetComponent<Image>().sprite = creditImage;
        credit.SetActive(true);
    }

    public void CloseCredits()
    {
        credit.SetActive(false);
    }
}
