using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Author: Brandon Guzman*/

public class viewDictionary : MonoBehaviour{
    public GameObject dictUI;
    public GameObject PopupButton;
    public bool showDict = false;
    public Dictionary d = Dictionary.playerDictionary;
    void Start()
    {
        /*dictionary starts as not shown*/
        dictUI.SetActive(false);
        if (!showDict && PlayerPrefs.GetInt("Quest") < 1){
            PopupButton.SetActive(false);
        }

    }
    public void Display() {
        dictUI.SetActive(!dictUI.activeSelf);
        if  (dictUI.activeSelf){ 
            /* if dictionary is being shown , then call refresh*/
            d.RevealWords();
            d.refresh();
        }
        else{
            d.reset();
        }


    }
    public void UpdateSearchForSpeechBubble(string word){
        d.searchBox.text = word;
        Display();
        d.UpdateSearch();

    }

    public void ShowButton(){
        showDict = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }


}