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
            questDetails = new string[] { "Witch", "Witch Talk", "Talk to the witch" };
            questNPC.Add("10", questDetails);
            questDetails = new string[] { "Cesar", "Reindeer", "Go Talk to Reindeer" };
            questNPC.Add("11", questDetails);
            questDetails = new string[] { "Witch", "Find Wand", "Yo te digo dónde está\n-Ve a la izquierda\n-después hacia arriba\n-y finalmente a la izquierda otra vez.  Está al lado del hospital." };
            questNPC.Add("12", questDetails);
        }

    }

    private void Update()
    {
        string newQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        string[] questDetails = new string[2];
        if (newQuest != currentQuest && QuestUI.questNPC.TryGetValue(newQuest, out questDetails))
        {
            print(newQuest + currentQuest);
            currentQuest = newQuest;
            UpdateText(questDetails[1], questDetails[2]);
        }
    }

    public void SetQuest(int quest)
    {
        PlayerPrefs.SetInt("Quest", quest);
    }

    public void SetQuestStep(int step)
    {
        PlayerPrefs.SetInt("QuestStep", step);
    }
    void UpdateText(string title, string descrip)
    {
        gameObject.transform.Find("ScrollArea").Find("Content").Find("QuestTitle").GetComponent<TMPro.TextMeshProUGUI>().text = title;
        gameObject.transform.Find("ScrollArea").Find("Content").Find("QuestDetails").GetComponent<TMPro.TextMeshProUGUI>().text = "-" + descrip;
    }

}
