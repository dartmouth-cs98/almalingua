using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using TMPro;
/** 
Celina Tala 
*/

public class NPCDialogueUI : MonoBehaviour
{
    public Text NameText;               //the textbox for the npc name/"yo"
    public TextMeshProUGUI DialogueText;                   //the textbook for the actual dialogue
    public GameObject RespondButton;    //a button

    private string NPCName;              //name of our NPC
    private bool userTalking;      //whether our user is talking
    private bool rootTalking;    //if it is the root speechnode (first time it is opened)
    private string userResponse;        //the user response
    private string dialogueText;        //the dialogueText of the NPC
    private bool nextMessageRequiresInput;  //does the next message require user input
    private string currentQuest = "";        //the current quest we are on
    private GameObject NPC;             //our NPC connected to the speechbubble
    private string NPCTouch;          //the NPC the user clicked on;

    /***************** OnEnable***********/
    /* 
    OnEnable of the speechbuble, we show our respond button
    */
    void OnEnable()
    {
        rootTalking = true;
        userTalking = false;
        RespondButton.GetComponent<HideShowObjects>().Show();
        UpdateQuest();
    }


    private void OnDisable()
    {
        RespondButton.GetComponent<HideShowObjects>().Hide();
    }

    private void UpdateQuest()
    {
        currentQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        string[] questDetails = new string[PlayerPrefs.GetInt("QuestLength")];
        if (QuestUI.questNPC.TryGetValue(currentQuest, out questDetails))
        {
            NPCName = questDetails[0];
            NPC = GameObject.Find(NPCName);
        }
    }
    public void NPCInteract(string NPCTouched)
    {
        NPCTouch = NPCTouched;
        if (NPCTouch != NPCName)
        {
            DefaultConversation(NPCTouch);
        }
        else
        {
            DisplayNextSentence();
        }


    }
    void DefaultConversation(string npcname)
    {
        GameObject NPC_x = GameObject.Find(npcname);
        NameText.text = npcname;
        NPC_x.GetComponent<NPCDialogueManager>().StartConversation();
        dialogueText = NPC_x.GetComponent<NPCDialogueManager>().CurrentText;
        if (dialogueText != null)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogueText));
        }
        RespondButton.GetComponent<HideShowObjects>().Hide();
        gameObject.transform.Find("CloseButton").GetComponent<HideShowObjects>().Show();

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
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);       //set userinput textbook to inactive
        gameObject.transform.GetChild(1).gameObject.SetActive(true);        //set textbox to active
        NameText.text = NPCName + ":";
        if (rootTalking)
        {
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

        //if we are at our last message, hide the button
        if (NPC.GetComponent<NPCDialogueManager>().OnLastMessage())
        {
            RespondButton.GetComponent<HideShowObjects>().Hide();
            gameObject.transform.Find("CloseButton").GetComponent<HideShowObjects>().Show();
        }
        else
        {
            //alter the button text depending on whether the next message requires input
            nextMessageRequiresInput = NPC.GetComponent<NPCDialogueManager>().NextMessageRequiresInput();
            if (nextMessageRequiresInput)
            {
                userTalking = true;
                RespondButton.GetComponentInChildren<Text>().text = "Respond";
            }
            else
            {
                userTalking = false;
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
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }

    }

    /********************* ResponseManager ********************/
    /*
     * Either the user is responding, in which case we set the NameText to "Yo" and sets userInput to active
     */
    public void ResponseManager()
    {
        if (userTalking)
        {
            NameText.text = "Yo:";
            gameObject.transform.GetChild(3).GetComponent<HideShowObjects>().Show();
            gameObject.transform.GetChild(3).GetComponent<InputField>().text = "";
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            RespondButton.GetComponentInChildren<Text>().text = "Done";
            userTalking = false;
        }
        else
        {
            if (userResponse != null)
            {
                NPC.GetComponent<NPCDialogueManager>().UpdateIntent(userResponse, () => DisplayNextSentence(), false);
            }
            else if (userResponse != null)
            {
                NPC.GetComponent<NPCDialogueManager>().UpdateIntent(userResponse, () => DisplayNextSentence(), true);
            }
            else
            {
                DisplayNextSentence();
            }
        }

    }

    /********** UserResponse *********************/
    /* 
     * Updating the intent with our UserInput 
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
        PicturePanelManager.HidePicturePanel();
    }
}

