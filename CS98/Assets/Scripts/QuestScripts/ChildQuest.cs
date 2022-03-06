using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildQuest : MonoBehaviour
{
    public GameObject Panel;
    public GameObject QuestUI;
    public GameObject Parent;

    bool ConvoOn = false;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        string currentQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        if (currentQuest == "32") {
            QuestUI.GetComponent<QuestUI>().ChildPoints(gameObject.GetInstanceID());

            Panel.GetComponent<HideShowObjects>().Show();
            Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence();
            ConvoOn = true;
            }
       
    }
    
    private void Update() {
        if (ConvoOn == true && Panel.activeSelf == false) {
            Parent.GetComponent<ChildParent>().Switch();
            ConvoOn = false;
        }
    }
}
