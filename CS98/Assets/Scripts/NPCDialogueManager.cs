using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class NPCDialogueManager : MonoBehaviour
{

    public Text Name;
    public Text Text;
    public GameObject Panel;
    public GameObject RespondButton;

    private NPCDialogue Dialogues;
    private Queue<string> sentences;
    private string JSONFilePath = "./Assets/Dialogues/doctorScript.json";
    private int SentenceNumber = 0;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        using (StreamReader stream = new StreamReader(JSONFilePath))
        {
            string json = stream.ReadToEnd();
            Dialogues = JsonUtility.FromJson<NPCDialogue>(json);
        }

        for (int i = 0; i < Dialogues.dialogues.Length; i++)
        {
            sentences.Enqueue(Dialogues.dialogues[i]);
        }
    }


    public void DisplayNextSentence()
    {
        RespondButton.GetComponent<HideShowObjects>().Show();
        Panel.transform.Find("UserInput").gameObject.SetActive(false);
        Panel.transform.Find("Text").gameObject.SetActive(true);
        Name.text = Dialogues.name;
        RespondButton.GetComponentInChildren<Text>().text = "Respond";
        if (sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
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


    public void UserResponse(string UserInput)
    {
        if (Name.text == "Yo")
        {
            DisplayNextSentence();
        }
        else
        {
            Name.text = "Yo";
            Panel.transform.Find("UserInput").gameObject.SetActive(true);
            Panel.transform.Find("Text").gameObject.SetActive(false);
            RespondButton.GetComponentInChildren<Text>().text = "Done";
            Debug.Log(UserInput);
        }

    }

}

