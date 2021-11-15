using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
/** 
Celina Tala 
*/

public class NPCDialogueUI : MonoBehaviour
{
    public class QuestMatching
    {
        public string questID { get; set; }
        public string NPC { get; set; }
    }

    public Text NameText;               //the textbox for the npc name/"yo"
    public Text Text;                   //the textbook for the actual dialogue

    public GameObject RespondButton;    //a button
    public GameObject PicturePanel;     //the panel that holds our pictures


    private string NPCName;              //name of our NPC
    private bool userTalking;
    private bool rootTalking;    //if it is the root speechnode (first time it is opened)
    private string userResponse;        //the user response
    private string dialogueText;        //the dialogueText of the NPC
    private int pictureChild;       //which picture to show
    private bool nextMessageRequiresInput;  //does the next message require user input
    private string currentQuest = "00";        //the curernt quest we are on
    private GameObject NPC;             //our NPC connected to the speechbubble
    private GameObject player;          //our player
    private Dictionary<string, string> questNPC = new Dictionary<string, string>(); //our dictionary
    // private string jsonString = @"[
    //     {'questID': '0100', 
    //     'NPC': 'Witch'},
    //     {'questID': '0101',
    //     'NPC': 'Cesar'},
    //     {'questID': '0102',
    //     'NPC': 'Witch'}]";



    /***************** OnEnable***********/
    /* 
    OnEnable of the speechbuble, we show our respond button
    */
    private void Start()
    {


        // questNPC = JsonConvert.DeserializeObject<IEnumerable<QuestMatching>>(jsonString).
        //           Select(p => (Id: p.questID, Record: p.NPC)).
        //           ToDictionary(t => t.Id, t => t.Record);
        // foreach (KeyValuePair<string, string> kvp in questNPC)
        //     Debug.Log("Key = {0}, Value = {1}" + kvp.Key + kvp.Value);
    }
    void OnEnable()
    {
        player = GameObject.Find("Protagonist");
        if (questNPC.Count < 3)
        {
            questNPC.Add("10", "Witch");
            questNPC.Add("11", "Cesar");
            questNPC.Add("12", "Witch");
        }
        RespondButton.GetComponent<HideShowObjects>().Show();
        rootTalking = true;
        userTalking = false;
        pictureChild = 0;
        currentQuest = player.GetComponent<QuestManager>().GetQuest().ToString() + player.GetComponent<QuestManager>().GetQuestStep().ToString();

        if (questNPC.TryGetValue(currentQuest, out NPCName))
        {
            NPC = GameObject.Find(NPCName);
        }

    }
    /***** DisplayNextSentence **********/
    /*
    * This function sets the Respond button text to "respond" and displays the TextBox for text
    * and change the Name Textbox to the name of NPC
    *  It will either get the current message (when it is the root of the dialogue) or next message
    * Also types out the sentenec
    */
    public void DisplayNextSentence()
    {
        gameObject.transform.GetChild(3).gameObject.SetActive(false);       //set userinput textbook to inactive
        gameObject.transform.GetChild(1).gameObject.SetActive(true);        //set textbox to active
        NameText.text = NPCName + ":";
        if (rootTalking)
        {
            Debug.Log("root talking");
            NPC.GetComponent<NPCDialogueManager>().StartConversation();
            rootTalking = !rootTalking;
        }
        else
        {
            NPC.GetComponent<NPCDialogueManager>().GetNextMessage();

        }
        dialogueText = NPC.GetComponent<NPCDialogueManager>().CurrentText;
        if (dialogueText != null)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogueText));
        }
        //this section is checking if we want to show the pictures of the orange/staff 
        if (PicturePanel != null && dialogueText == "Quieres esto?")
        {
            PicturePanel.GetComponent<HideShowObjects>().Show();
            if (pictureChild > 0)
            {
                PicturePanel.transform.GetChild(pictureChild - 1).GetComponent<HideShowObjects>().Hide();
            }
            PicturePanel.transform.GetChild(pictureChild).GetComponent<HideShowObjects>().Show();
            pictureChild += 1;
        }
        //if we are at our last message, hide the button
        if (NPC.GetComponent<NPCDialogueManager>().OnLastMessage())
        {
            Debug.Log("last message");
            RespondButton.GetComponent<HideShowObjects>().Hide();
        }
        else
        {
            //alter the button text depending on whether the next message requires input
            nextMessageRequiresInput = NPC.GetComponent<NPCDialogueManager>().NextMessageRequiresInput();
            if (nextMessageRequiresInput)
            {
                RespondButton.GetComponentInChildren<Text>().text = "Respond";
            }
            else
            {
                RespondButton.GetComponentInChildren<Text>().text = "Next";
            }
        }

    }

    /************ TypeSentence *****************/
    /* 
      Typing out the sentence
    */
    IEnumerator TypeSentence(string sentence)
    {
        Text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            Text.text += letter;
            yield return null;
        }
    }

    /********************* ResponseManager ********************/
    /*
    * Either the user is responding, in which case we set the NameText to "Yo" and sets userInput to active
    */
    public void ResponseManager()
    {

        if (!userTalking && nextMessageRequiresInput)
        {
            NameText.text = "Yo:";
            gameObject.transform.GetChild(3).GetComponent<HideShowObjects>().Show();
            gameObject.transform.GetChild(3).GetComponent<InputField>().text = "";
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            RespondButton.GetComponentInChildren<Text>().text = "Done";
            if (PicturePanel != null)
            {
                PicturePanel.GetComponent<HideShowObjects>().Hide();

            }
        }
        else
        {
            if (userResponse != null)
            {
                NPC.GetComponent<NPCDialogueManager>().UpdateIntent(userResponse, () => DisplayNextSentence(), true);
            }
            else
            {
                DisplayNextSentence();
            }


        }
        if (nextMessageRequiresInput) userTalking = !userTalking;

    }

    /********** UserResponse *********************/
    /* Updating the intent with our UserInput 
    */
    public void UserResponse(string UserInput)
    {
        userResponse = UserInput;
    }

    /** Closing our speechbubblel and button
    */
    public void CloseButton()
    {
        gameObject.GetComponent<HideShowObjects>().Hide();
        RespondButton.GetComponent<HideShowObjects>().Hide();
        PicturePanel.GetComponent<HideShowObjects>().Hide();
    }
}

