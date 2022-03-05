using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressXDialogueManager : MonoBehaviour
{

    bool inTriggerSpace = false;
    public GameObject InfoUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && inTriggerSpace)
        {
            gameObject.GetComponent<NPCInteraction>().StartDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        InfoUI.GetComponent<HideShowObjects>().Show();
        inTriggerSpace = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        InfoUI.GetComponent<HideShowObjects>().Hide();
        inTriggerSpace = false;
    }

}
