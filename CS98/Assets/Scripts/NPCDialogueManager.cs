using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class NPCDialogueManager : MonoBehaviour
{
    private NPCDialogue Dialogues;
    public Text NPCName;
    public Text NPCText;
    private Queue<string> sentences;
    string JSONFilePath = "./Assets/Dialogues/doctorScript.json";
    private bool NextButtonShown = true;
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
        if (sentences.Count == 0 && NextButtonShown)
        {
            EndDialogue();
        }
    }
    public void StartDialogue()
    {
        NPCName.text = Dialogues.name;
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
    }

    IEnumerator TypeSentence(string sentence)
    {
        NPCText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            NPCText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        GameObject.Find("/SpeechBubble/Next").SetActive(false);
        NextButtonShown = false;
    }

}

