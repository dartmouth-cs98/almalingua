using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

/** 
Celina Tala 
*/

public class NPCDialogueManager : MonoBehaviour
{
    public Text Name;
    public Text Text;
    public GameObject RespondButton;

    private NPCDialogue dialogues;
    private Queue<string> sentences;
    private string JSONFilePath = "./Assets/Dialogues/doctorScript.json";
    private bool userTalking = false;


    // Use this for initialization
    void Awake()
    {
        using (StreamReader stream = new StreamReader(JSONFilePath))
        {
            string json = stream.ReadToEnd();
            dialogues = JsonUtility.FromJson<NPCDialogue>(json);
        }
    }

    void OnEnable()
    {
        sentences = new Queue<string>();
        for (int i = 0; i < dialogues.dialogues.Length; i++)
        {
            sentences.Enqueue(dialogues.dialogues[i]);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        RespondButton.GetComponent<HideShowObjects>().Show();
        RespondButton.GetComponentInChildren<Text>().text = "Respond";
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        Name.text = dialogues.name;
        if (sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            Text.text = "Dialogue Completed";
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
            Name.text = "Yo";
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

    }

    public void CloseButton()
    {
        gameObject.GetComponent<HideShowObjects>().Hide();
        RespondButton.GetComponent<HideShowObjects>().Hide();
    }
}

