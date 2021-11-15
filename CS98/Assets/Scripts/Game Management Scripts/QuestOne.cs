using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOne : MonoBehaviour
{
    public GameObject Protagonist;      //our player
    public GameObject Panel;            //our speechbubble
    public GameObject Witch;           //our witch 
    private void Start()
    {
        // witch = GameObject.Find("Witch");
    }
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D other)
    {
        // int currentStep = Protagonist.GetComponent<QuestManager>().GetQuestStep();
        Protagonist.GetComponent<QuestManager>().SetQuestStep(2);
        Witch.GetComponent<HideShowObjects>().Show();
        Panel.GetComponent<HideShowObjects>().Show();
        Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence();

    }
}
