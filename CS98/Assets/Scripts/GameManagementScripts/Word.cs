using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Brandon Guzman*/
[System.Serializable]
public class Word{
    
    public string w; 
    public int ID;
    public string definition;
    public bool encountered;
    public string icon;
    public int questN;
    public int stepN;

}




[System.Serializable]
public class WordCollection{ 

    public List<Word> wlist;

    public void Add(Word w){

        wlist.Add(w);
    }
}

