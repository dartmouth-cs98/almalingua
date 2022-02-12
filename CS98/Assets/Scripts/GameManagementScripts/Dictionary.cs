using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;


/* Author: Brandon Guzman*/

public class Dictionary : MonoBehaviour
{

    public static Dictionary playerDictionary;
    public static Hashtable wordMap; //public static hashmap for words
    public InputField searchBox;
    public Slot[] slots;
    public WordCollection masterList;
    public GameObject ViewDict; //will revist

    //private WordCollection InputArray;
    private int length;
    private int startIndex = 0;
    public string searchString;
    private int TargetID = -1;
    // private int discoveredWords = 0;

    void Start()
    {
        string path = Application.streamingAssetsPath;
        playerDictionary = this;

        List<WordCollection> lists = new List<WordCollection>();
        lists.Add(readWordFiles(path + "/quest1.json"));
        lists.Add(readWordFiles(path + "/quest2.json"));
        lists.Add(readWordFiles(path + "/quest3.json"));
        lists.Add(readWordFiles(path + "/quest4.json"));
        lists.Add(readWordFiles(path + "/quest5.json"));
        lists.Add(readWordFiles(path + "/quest6.json"));


        foreach (WordCollection l in lists)
        {
            foreach (Word word in l.wlist)
            {
                masterList.Add(word);
            }
        }

        length = masterList.wlist.Count;
        masterList.wlist = masterList.wlist.OrderBy(a => a.w).ToList();
        wordMap = new Hashtable();

        //give ID's to all the words and add them to the hashtable 
        for (int i = 0; i < length; i++)
        {
            masterList.wlist[i].ID = i;
            // masterList.wlist[i].encountered = true;

            wordMap.Add(masterList.wlist[i].w, masterList.wlist[i]);  // key, value = "word", word object
        }
        refresh();

    }

    private WordCollection readWordFiles(string filename)
    {
        WordCollection words;
        /*read into a  List<Word> from the json file provided */
        Queue<string> sentences = new Queue<string>();
        using (StreamReader stream = new StreamReader(filename))
        {
            string json = stream.ReadToEnd();
            words = JsonUtility.FromJson<WordCollection>(json);
        }

        return words;

    }

    // refresh is called everytime user opens dictionary & also when they turn a page
    //sliding window of seeing 8 slots at a time
    public void refresh()
    {
        int currIdx = startIndex;


        // //start dictionary from first learned word if we are not searching for a specific word
        // if (discoveredWords > 0 && TargetID == -1 && !masterList.wlist[currIdx].encountered){
        //     while (!masterList.wlist[currIdx].encountered){
        //         currIdx++;
        //     }
        // }


        if (length < 1) return; /*edge case check on startup*/
        int i = 0;
        while (i < 8 && currIdx < length)
        {
            Text t = slots[i].txtbox.GetComponent<UnityEngine.UI.Text>();
            Image img = slots[i].imgbox.GetComponent<UnityEngine.UI.Image>();
            Word current = masterList.wlist[currIdx];

            if (current.encountered)
            {
                t.text = current.w + "\n -" + current.definition; //update slot with word definition

                //if user searched for specific word, then highlight word in red
                if (currIdx == TargetID)
                {
                    img.color = Color.red;
                }
                else
                {
                    img.color = Color.white;
                }
            }
            else
            { //unknown words will be in black
                img.color = Color.black;
                t.text = "Undiscovered";
            }
            currIdx++;
            i++;

        }
        //empty slots because we have reached the end of our word list
        if (i < 8 && currIdx >= length)
        {
            for (int j = i + 1; j < 8; j++)
            {
                Image img = slots[j].imgbox.GetComponent<UnityEngine.UI.Image>();
                img.color = Color.black;
                Text t = slots[j].txtbox.GetComponent<UnityEngine.UI.Text>();
                t.text = "";

            }
        }


    }



    public void TurnRight()
    {
        if (startIndex < length)
        {
            startIndex += 8;
        }
        refresh();

    }

    public void TurnLeft()
    {
        if (startIndex > 0)
        {
            startIndex -= 8;
        }
        refresh();
    }
    public void UpdateSearch()
    {
        if (!searchBox) return;
        searchString = searchBox.text;
        if (wordMap.ContainsKey(searchString))
        {
            Word searched = (Word)wordMap[searchString];
            int id = searched.ID;
            startIndex = id - (id % 8);
            TargetID = id;
            refresh();

        }
    }

    public void UpdateSearchForSpeechBubble(string word)
    {
        searchString = word;
        ViewDict.GetComponent<viewDictionary>().Display();
        if (wordMap.ContainsKey(searchString))
        {
            Word searched = (Word)wordMap[searchString];
            int id = searched.ID;
            startIndex = id - (id % 8);
            TargetID = id;
            refresh();
        }
    }

    public void reset()
    {
        searchString = "";
        TargetID = -1;
        searchBox.text = "";

    }



    public void discoveredWord(string newWord)
    {
        if (wordMap.ContainsKey(newWord))
        {
            Word discovered = (Word)wordMap[newWord];
            discovered.encountered = true;
        }
    }

    public void RevealWords()
    {   

        int q =  PlayerPrefs.GetInt("Quest");
        int step = PlayerPrefs.GetInt("QuestStep");
        print("she been called");
        foreach (Word w in masterList.wlist)
        {
            //if we're ahead on quests then yes we have encountered
            if (w.questN < q ){
                w.encountered = true;
            }
            // all previous words in the quest
            if (w.questN == q && w.stepN <= step){
                w.encountered = true;
            }
        }
    }

}