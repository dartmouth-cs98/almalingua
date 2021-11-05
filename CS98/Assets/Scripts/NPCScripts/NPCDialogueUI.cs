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
    public GameObject DialogueManager;  //DialogueManager empty game object

    
    private bool userTalking = false;
    private bool rootTalking = true;    //if it is the root speechnode (first time it is opened)


    void OnEnable()
    {
        RespondButton.GetComponent<HideShowObjects>().Show();
        rootTalking = true;

    }

    public void DisplayNextSentence()
    {
        RespondButton.GetComponentInChildren<Text>().text = "Respond";
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        NameText.text = NPCName;
        string dialogueText;
        if (rootTalking) { 
            dialogueText = DialogueManager.GetComponent<NPCDialogueManagerRay>().GetCurrentMessage();
            rootTalking = false;
        } else { 
            dialogueText = DialogueManager.GetComponent<NPCDialogueManagerRay>().GetNextMessage();
        }
        if (dialogueText != null) {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogueText));
        } 

    }

    IEnumerator TypeSentence(string sentence)
    {
        Text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            Text.text += letter;
            yield return null;
        }
    }


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
            DisplayNextSentence();

        }
        userTalking = !userTalking;

    }

    public void UserResponse(string UserInput)
    {
        DialogueManager.GetComponent<NPCDialogueManagerRay>().UpdateIntent(UserInput, true);
    }

    public void CloseButton()
    {
        gameObject.GetComponent<HideShowObjects>().Hide();
        RespondButton.GetComponent<HideShowObjects>().Hide();
    }
}

