using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;


/* Author: Brandon Guzman*/

public class Dictionary : MonoBehaviour{

    public static Dictionary playerDictionary; 
    public static Hashtable wordIdMap; //public static hashmap for words
    public GameObject searchBox;
    public Slot[] slots;
    public WordCollection InputArray;

    private int length;
    private string JSONFilePath = "../verbs.json";
    private int startIndex = 0;
    private string SearchString;
    private int TargetID = -1;


    void Start()
    {   
        /*read in a word[] from the json file provided */
        Queue<string> sentences = new Queue<string>();
        using (StreamReader stream = new StreamReader(JSONFilePath)){
            string json = stream.ReadToEnd();
            InputArray = JsonUtility.FromJson<WordCollection>(json);
        }


        InputArray.wlist = InputArray.wlist.OrderBy(a => a.w).ToList();
        wordIdMap = new Hashtable();
        for (int i = 0; i < InputArray.wlist.Count; i++){
            InputArray.wlist[i].ID = i;
            InputArray.wlist[i].encountered = true;

            wordIdMap.Add(InputArray.wlist[i].w, i);
        }
        length = InputArray.wlist.Count;
        refresh();
        
    }

    // refresh is called everytime user opens dictionary & also when they turn a page
    //sliding window of seeing 8 slots at a time
    public void refresh(){
        int currIdx = startIndex;
        if (length <1) return; /*edge case check on startup*/
        int i = 0;
        while (i < 8 && currIdx < length){
            Text t = slots[i].txtbox.GetComponent<UnityEngine.UI.Text>();
            Image img = slots[i].imgbox.GetComponent<UnityEngine.UI.Image>();
            Word current  = InputArray.wlist[currIdx];

            if (current.encountered){
                t.text = current.w + "\n -" + current.definition; //update slot with word definition

                //if user searched for specific word, then highlight word in red
                if (currIdx == TargetID){
                    img.color = Color.red;    
                }
                else{
                    img.color= Color.white;
                }
            }
            else{ //unknown words will be in black
                img.color= Color.black;
                t.text = "Undiscovered"; 
            }
            currIdx ++;
            i ++;
             
        }
        //empty slots because we have reached the end of our word list
        if (i < 8 && currIdx >= length){
            for (int j = i + 1; j < 8; j++)
            {
                Image img = slots[j].imgbox.GetComponent<UnityEngine.UI.Image>();
                img.color= Color.black;
                Text t = slots[j].txtbox.GetComponent<UnityEngine.UI.Text>();
                t.text = "";
                
            }
        }
        SearchString = "";
        
    }

    public void TurnRight(){
         if (startIndex < length - 8){
            startIndex+=8;
        }
        refresh(); 

    }
    
    public void TurnLeft(){
        if (startIndex > 0){
            startIndex -= 8;
        }
        refresh();  
    }
    public void UpdateSearch(){
        if (!searchBox) return;
        SearchString = searchBox.GetComponent<UnityEngine.UI.InputField>().text;
        if (wordIdMap.ContainsKey(SearchString)){
            int id = (int) wordIdMap[SearchString];
            startIndex  = id - (id % 8);
            TargetID = id;
            refresh();

        }
    }
    
}