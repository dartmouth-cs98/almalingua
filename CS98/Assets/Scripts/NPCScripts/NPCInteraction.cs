using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{

    public GameObject Panel;

    private void OnMouseDown()
    {
        if (!Panel.activeSelf)
        {
            Panel.GetComponent<HideShowObjects>().Show();
            print("here");
        }

        Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence();
    }

    //called by QuestUI
    public void StartDialogue()
    {
        OnMouseDown();
    }
}

