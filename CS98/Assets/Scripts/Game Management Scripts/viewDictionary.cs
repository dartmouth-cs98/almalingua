using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Author: Brandon Guzman*/

public class viewDictionary : MonoBehaviour
{
    public GameObject DictUI;
    public GameObject PopupButton;
    void Start()
    {
        /*dictionary starts as not shown*/
        if (DictUI)
        {
            DictUI.SetActive(false);
        }
    }
    public void Display()
    {
        if (DictUI)
        {
            DictUI.SetActive(!DictUI.activeSelf);

            if (DictUI.activeSelf && Dictionary.playerDictionary)
            { /* if dictionary is being shown , then call refresh*/
                Dictionary.playerDictionary.refresh();
                PopupButton.GetComponentInChildren<Text>().text = "Close";
            }
            else
            {
                PopupButton.GetComponentInChildren<Text>().text = "Open";

            }

        }

    }
}
