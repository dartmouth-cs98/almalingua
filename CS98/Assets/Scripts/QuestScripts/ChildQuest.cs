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
        string currentQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        if (currentQuest == "32") {
             Panel.GetComponent<HideShowObjects>().Show();
            Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence();
            QuestUI.GetComponent<QuestUI>().ChildPoints(gameObject.GetInstanceID());
            }
       
    }
}
