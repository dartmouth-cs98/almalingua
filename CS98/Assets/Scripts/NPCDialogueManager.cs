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
    private bool NPCTalking = true;
    private TouchScreenKeyboard keyboard;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        using (StreamReader stream = new StreamReader(JSONFilePath))
        {
            string json = stream.ReadToEnd();
            Dialogues = JsonUtility.FromJson<NPCDialogue>(json);
        }
        StartDialogue();
    }
    private void Update()
    {
        if (keyboard != null)
        {
            Debug.Log(TouchScreenKeyboard.visible);
        }
    }
    public void StartDialogue()
    {
        Panel.transform.Find("NextButton").gameObject.SetActive(true);
        Name.text = Dialogues.name;
        sentences.Clear();
        for (int i = 0; i < Dialogues.dialogues.Length; i++)
        {
            sentences.Enqueue(Dialogues.dialogues[i]);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
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

    void EndDialogue()
    {
        Panel.transform.Find("NextButton").gameObject.SetActive(false);
        RespondButton.GetComponent<HideShowObjects>().Show();
        NPCTalking = !NPCTalking;
    }

    public void UserResponse(string UserInput)
    {
        Name.text = "Yo";
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, true, false, false, false);
    }

}

