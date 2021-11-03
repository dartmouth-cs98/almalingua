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


    void OnEnable()
    {
        // sentences = new Queue<string>();
        // for (int i = 0; i < dialogues.dialogues.Length; i++)
        // {
        //     sentences.Enqueue(dialogues.dialogues[i]);
        // }
        // DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        RespondButton.GetComponent<HideShowObjects>().Show();
        RespondButton.GetComponentInChildren<Text>().text = "Respond";
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        NameText.text = NPCName;
        DialogueManager.GetComponent<NPCDialogueManagerRay>().GetNextMessage();
        string dialogueText =  DialogueManager.GetComponent<NPCDialogueManagerRay>().GetCurrentMessage();
        if (dialogueText == null) {
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

