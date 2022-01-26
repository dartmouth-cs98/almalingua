using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildQuest : MonoBehaviour
{
    public GameObject Panel;
    public GameObject QuestUI;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        Panel.GetComponent<HideShowObjects>().Show();
        Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence();
        QuestUI.GetComponent<QuestUI>().ChildPoints(gameObject.GetInstanceID());
    }
}
