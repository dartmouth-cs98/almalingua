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

    private NPCDialogue dialogues;
    private Queue<string> sentences;
    private string JSONFilePath = "./Assets/Dialogues/doctorScript.json";
    private bool userTalking = false;


    // Use this for initialization
    void Start()
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
        Debug.Log("Shown");
        sentences.Clear();
        for (int i = 0; i < dialogues.dialogues.Length; i++)
        {
            sentences.Enqueue(dialogues.dialogues[i]);
        }

    }

    public void DisplayNextSentence()
    {
        RespondButton.GetComponent<HideShowObjects>().Show();
        RespondButton.GetComponentInChildren<Text>().text = "Respond";
        Panel.transform.Find("UserInput").gameObject.SetActive(false);
        Panel.transform.Find("Text").gameObject.SetActive(true);
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
            Debug.Log("User talking");
            Name.text = "Yo";
            Panel.transform.Find("UserInput").gameObject.SetActive(true);
            Panel.transform.Find("UserInput").GetComponent<InputField>().text = "";
            Panel.transform.Find("Text").gameObject.SetActive(false);
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
        Debug.Log(UserInput);
    }

    public void CloseButton()
    {
        Panel.GetComponent<HideShowObjects>().Hide();
        RespondButton.GetComponent<HideShowObjects>().Hide();
    }
}

