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
        }

        Panel.GetComponent<NPCDialogueUI>().NPCInteract(gameObject.name);
    }

    //called by QuestUI
    public void StartDialogue()
    {
        OnMouseDown();
    }
}

