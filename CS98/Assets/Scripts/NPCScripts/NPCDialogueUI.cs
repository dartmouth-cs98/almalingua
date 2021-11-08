using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

/** 
Celina Tala 
*/

public class NPCDialogueUI : MonoBehaviour
{
    public Text NameText;               //the textbox for the npc name/"yo"
    public Text Text;                   //the textbook for the actual dialogue
    public string NPCName;              //name of our NPC
    public GameObject RespondButton;    //a button
    public GameObject npc;

    private bool userTalking;
    private bool rootTalking;    //if it is the root speechnode (first time it is opened)
    private string userResponse;        //the user response
    private string dialogueText;        //the dialogueText of the NPC


    /***************** OnEnable***********/
    /* 
    OnEnable of the speechbuble, we show our respond button
    */
    void OnEnable()
    {
        RespondButton.GetComponent<HideShowObjects>().Show();
        rootTalking = true;
        userTalking = false;
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
        RespondButton.GetComponentInChildren<Text>().text = "Respond";
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        NameText.text = NPCName;
        if (rootTalking)
        {
            npc.GetComponent<NPCDialogueManager>().StartConversation();
            rootTalking = !rootTalking;
        }
        else
        {
            npc.GetComponent<NPCDialogueManager>().GetNextMessage();

        }
        dialogueText = npc.GetComponent<NPCDialogueManager>().CurrentText;
        if (dialogueText != null)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogueText));
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
        if (!userTalking)
        {
            NameText.text = "Yo";
            gameObject.transform.GetChild(3).GetComponent<HideShowObjects>().Show();
            gameObject.transform.GetChild(3).GetComponent<InputField>().text = "";
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            RespondButton.GetComponentInChildren<Text>().text = "Done";
        }
        else
        {
            npc.GetComponent<NPCDialogueManager>().UpdateIntent(userResponse);
            DisplayNextSentence();

        }
        userTalking = !userTalking;

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
    }
}

