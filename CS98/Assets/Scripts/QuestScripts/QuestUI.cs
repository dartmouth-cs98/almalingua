using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public static Dictionary<string, string[]> questNPC = new Dictionary<string, string[]>(); //our dictionary mapping questIDS to the NPC associated with it
    string questTask;
    string currentQuest;

    GameObject Witch;

    // Start is called before the first frame update
    private void OnEnable()
    {
        int questLength = 3;
        PlayerPrefs.SetInt("QuestLength", questLength);
        if (questNPC.Count < 11)
        {
            string[] questDetails = new string[] { "Witch", "Witch Talk", "Talk to the witch" };
            questNPC.Add("00", questDetails);
            questDetails = new string[] { "Witch", "Witch Talk", "Talk to the witch" };
            questNPC.Add("10", questDetails);
            questDetails = new string[] { "Cesar", "Reindeer", "Go Talk to Reindeer" };
            questNPC.Add("11", questDetails);
            questDetails = new string[] { "Witch", "Find Wand", "Yo te digo dónde está\n-Ve a la izquierda\n-después hacia arriba\n-y finalmente a la izquierda otra vez.  Está al lado del hospital." };
            questNPC.Add("12", questDetails);
            questDetails = new string[] { "Farmer", "Talk to Farmer", "Talk to Farmer" };
            questNPC.Add("20", questDetails);
            questDetails = new string[] { "Farmer", "Talk to Farmer", "Talk to Farmer" };
            questNPC.Add("26", questDetails);
            questDetails = new string[] { "Witch", "", "Talk to Witch Again for Next Steps" };
            questNPC.Add("27", questDetails);
            questDetails = new string[] { "Teacher", "Talk to Teacher", "Talk to the teacher" };
            questNPC.Add("30", questDetails);
            questDetails = new string[] { "Cesar", "Talk to Cesar", "Talk to Cesar about children" };
            questNPC.Add("31", questDetails);
            questDetails = new string[] { "", "Find children", "Uno está por el río\n-el otro al lado del bosque" };
            questNPC.Add("32", questDetails);
            questDetails = new string[] { "Teacher", "Talk to Teacher", "" };
            questNPC.Add("33", questDetails);
            questDetails = new string[] { "Witch", "", "Talk to Witch Again for Next Steps" };
            questNPC.Add("34", questDetails);

        }
        EventManager.onProtagonistChange += WitchSpeak;
        EventManager.onQuestChange += UpdateQuest;

    }

    private void Start()
    {
        UpdateQuest();
    }

    private void OnDisable()
    {
        EventManager.onProtagonistChange -= WitchSpeak;
        EventManager.onQuestChange -= UpdateQuest;

    }
    private void UpdateQuest()
    {
        currentQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        string[] questDetails = new string[3];
        if (QuestUI.questNPC.TryGetValue(currentQuest, out questDetails))
        {
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
        EventManager.RaiseOnQuestChange();

    }
    void UpdateText(string title, string descrip)
    {
        gameObject.transform.Find("ScrollArea").Find("Content").Find("QuestTitle").GetComponent<TMPro.TextMeshProUGUI>().text = title;
        gameObject.transform.Find("ScrollArea").Find("Content").Find("QuestDetails").GetComponent<TMPro.TextMeshProUGUI>().text = "-" + descrip;
    }

    //auto 
    public void WitchSpeak()
    {
        SetQuestStep(3);
        GameObject.Find("Witch").GetComponent<NPCInteraction>().StartDialogue();
    }

    public void GiveSpell()
    {
        Vector2 PlayerPosition = GameObject.Find("/CameraPlayer/PlayerManager/Protagonist").transform.position;
        Witch = GameObject.Find("Witch");
        Witch.transform.position = new Vector2(PlayerPosition.x - 0.5f, PlayerPosition.y);
    }
}
