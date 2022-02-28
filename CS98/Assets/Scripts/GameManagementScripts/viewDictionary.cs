using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Author: Brandon Guzman*/

public class viewDictionary : MonoBehaviour
{
    public GameObject dictUI;
    public GameObject PopupButton;
    public bool showDict = false;
    void Start()
    {
        /*dictionary starts as not shown*/
        dictUI.SetActive(false);
        if (!showDict && PlayerPrefs.GetInt("Quest") < 2){
            PopupButton.SetActive(false);
        }

    }
    public void Display()
    {
        dictUI.SetActive(!dictUI.activeSelf);
        if  (dictUI.activeSelf && Dictionary.playerDictionary){ 
            /* if dictionary is being shown , then call refresh*/

            Dictionary.playerDictionary.RevealWords();
        }


    }

    public void ShowButton(){
        print("here");
        showDict = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }


}
