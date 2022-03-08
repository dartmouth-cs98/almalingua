using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressXDialogueManager : MonoBehaviour
{

    bool inTriggerSpace = false;
    bool isTalking = false;
    public GameObject InfoUI;

    private const string PLAYER_TAG = "Player";

    void Update()
    {
        // Hide the InfoUI and disable X button during conversation.
        if (gameObject.GetComponent<NPCInteraction>().Panel.activeSelf)
        {
            if (!isTalking) // if not already hidden UI
            {
                InfoUI.GetComponent<HideShowObjects>().Hide();
                isTalking = true;
            }
        } else
        {
          isTalking = false;
        }

        // Trigger dialogue if needed.
        if (Input.GetKeyDown(KeyCode.X) && inTriggerSpace && !isTalking)
        {
            gameObject.GetComponent<NPCInteraction>().StartDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isTalking && col.tag == PLAYER_TAG)
        {
            InfoUI.GetComponent<HideShowObjects>().Show();
        }
        inTriggerSpace = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == PLAYER_TAG) {
            InfoUI.GetComponent<HideShowObjects>().Hide();
            inTriggerSpace = false;
        }
    }

}
