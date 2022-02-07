using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Author: Brandon Guzman*/

public class viewDictionary : MonoBehaviour
{
    public GameObject dictUI;
    public GameObject PopupButton;
    void Start()
    {
        /*dictionary starts as not shown*/
        dictUI.SetActive(false);
    }
    public void Display()
    {
        dictUI.SetActive(!dictUI.activeSelf);
        print("she been called and current state is " + dictUI.activeSelf);

        if  (dictUI.activeSelf && Dictionary.playerDictionary){ 
            /* if dictionary is being shown , then call refresh*/

            Dictionary.playerDictionary.RevealWords();
            PopupButton.GetComponentInChildren<Text>().text = "Close";
        }
        else
        {
            PopupButton.GetComponentInChildren<Text>().text = "Open";

        }


    }


}
