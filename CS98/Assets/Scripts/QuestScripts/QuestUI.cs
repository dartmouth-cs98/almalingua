using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public static Dictionary<string, string[]> questNPC = new Dictionary<string, string[]>(); //our dictionary mapping questIDS to the NPC associated with it
    string questTask;
    string currentQuest;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (questNPC.Count < 4)
        {
            string[] questDetails = new string[] { "Witch", "Witch Talk", "Talk to the witch" };
            questNPC.Add("00", questDetails);
            questDetails = new string[] { "Witch", "Reindeer", "Talk to Cesar" };
            questNPC.Add("10", questDetails);
            questDetails = new string[] { "Cesar", "Wand", "Go find the wand" };
            questNPC.Add("11", questDetails);
            questDetails = new string[] { "Witch", "Burn", "Burn Ice Cube" };
            questNPC.Add("12", questDetails);
        }

    }

    private void Update()
    {
        string newQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        string[] questDetails = new string[2];
        if (newQuest != currentQuest && QuestUI.questNPC.TryGetValue(newQuest, out questDetails))
        {
            currentQuest = newQuest;
            UpdateText(questDetails[1], questDetails[2]);
        }
    }

    void UpdateText(string title, string descrip)
    {
        gameObject.transform.Find("QuestTitle").GetComponent<TMPro.TextMeshProUGUI>().text = title;
        gameObject.transform.Find("QuestDetails").GetComponent<TMPro.TextMeshProUGUI>().text += descrip;
    }

}
