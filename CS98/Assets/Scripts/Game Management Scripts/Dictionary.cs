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
    public InputField searchBox;
    public Slot[] slots;
    public WordCollection masterList;

    //private WordCollection InputArray;
    private int length;
    private int startIndex = 0;
    private string searchString;
    private int TargetID = -1;
    // private int discoveredWords = 0;

    void Start()
    {  
        string path = "WordFiles/";

        List<WordCollection> lists  = new List<WordCollection>();
        lists.Add(readWordFiles(path + "quest1.json"));
        // lists.Add(readWordFiles(path + "verbs.json"));
        // lists.Add(readWordFiles(path + "nouns.json"));
        // lists.Add(readWordFiles(path + "pronouns.json"));
        // lists.Add(readWordFiles(path + "prepositions.json"));
        // lists.Add(readWordFiles(path + "adverbs.json"));

        foreach (WordCollection l in lists){
            foreach (Word word in l.wlist){
                masterList.Add(word);
            }
        }

        length = masterList.wlist.Count;
        masterList.wlist = masterList.wlist.OrderBy(a => a.w).ToList();
        wordIdMap = new Hashtable();

        //give ID's to all the words and add them to the hashtable 
        for (int i = 0; i < length; i++){
            masterList.wlist[i].ID = i;

            wordIdMap.Add(masterList.wlist[i].w, masterList.wlist[i]);  // key, value = "word", word object
        }
        refresh();

    }

    private WordCollection readWordFiles(string filename){
        WordCollection words;
         /*read into a  List<Word> from the json file provided */
        Queue<string> sentences = new Queue<string>();
        using (StreamReader stream = new StreamReader(filename)){
            string json = stream.ReadToEnd();
            words = JsonUtility.FromJson<WordCollection>(json);
        }

        return words;

    }

    // refresh is called everytime user opens dictionary & also when they turn a page
    //sliding window of seeing 8 slots at a time
    public void refresh(){
        int currIdx = startIndex;


        // //start dictionary from first learned word if we are not searching for a specific word
        // if (discoveredWords > 0 && TargetID == -1 && !masterList.wlist[currIdx].encountered){
        //     while (!masterList.wlist[currIdx].encountered){
        //         currIdx++;
        //     }
        // }


        if (length <1) return; /*edge case check on startup*/
        int i = 0;
        while (i < 8 && currIdx < length){
            Text t = slots[i].txtbox.GetComponent<UnityEngine.UI.Text>();
            Image img = slots[i].imgbox.GetComponent<UnityEngine.UI.Image>();
            Word current  = masterList.wlist[currIdx];

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
        reset();

        
    }

    // public bool validPage(int idx){
    //     for (int i = idx; i < idx + 8; i++){
    //         if (masterList.wlist[idx].encountered){
    //             return true;
    //         }
    //     }
    //     return false;
    // }


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
        searchString = searchBox.text;
        if (wordIdMap.ContainsKey(searchString)){
            Word searched = (Word)wordIdMap[searchString];
            int id = searched.ID;
            startIndex  = id - (id % 8);
            TargetID = id;
            refresh();

        }
    }

    public void reset(){
        startIndex = 0;
        searchString = "";
        TargetID = -1;
        searchBox.text = "";

    }


    
    public void discoveredWord(string newWord){
        if (wordIdMap.ContainsKey(newWord)){
            Word discovered = (Word)wordIdMap[newWord];
            discovered.encountered = true;
        }
    }
    
}