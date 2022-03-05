using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Author: Brandon Guzman*/

public class viewDictionary : MonoBehaviour{
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
    public void Display() {
        Dictionary d = Dictionary.playerDictionary;
        dictUI.SetActive(!dictUI.activeSelf);
        if  (dictUI.activeSelf){ 
            /* if dictionary is being shown , then call refresh*/

            d.RevealWords();
        }
        else{
            d.reset();
        }


    }

    public void ShowButton(){
        showDict = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }


}