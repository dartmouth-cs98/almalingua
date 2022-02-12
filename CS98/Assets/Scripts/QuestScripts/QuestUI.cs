using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public static Dictionary<string, string[]> questNPC = new Dictionary<string, string[]>(); //our dictionary mapping questIDS to the NPC associated with it
    string questTask;
    string currentQuest;
    HashSet<int> Children = new HashSet<int>();
    GameObject Witch;

    // Start is called before the first frame update
    private void OnEnable()
    {
        int questLength = 3;
        PlayerPrefs.SetInt("QuestLength", questLength);
        if (questNPC.Count == 0)
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
            questDetails = new string[] { "Slime_Green", "Fight the Green Slime", "Farm" };
            questNPC.Add("21", questDetails);
            questDetails = new string[] { "Farmer", "Talk to Farmer", "Talk to Farmer" };
            questNPC.Add("22", questDetails);
            questDetails = new string[] { "Slime_Orange", "Fight the Orange Slime", "Farm" };
            questNPC.Add("23", questDetails);
            questDetails = new string[] { "Farmer", "Talk to Farmer", "Talk to Farmer" };
            questNPC.Add("24", questDetails);
            questDetails = new string[] { "Slime_Black", "Fight the Black Slime", "Farm" };
            questNPC.Add("25", questDetails);
            questDetails = new string[] { "Farmer", "Talk to Farmer", "Talk to Farmer" };
            questNPC.Add("26", questDetails);
            questDetails = new string[] { "Witch", "", "Talk to Witch Again for Next Steps" };
            questNPC.Add("27", questDetails);
            questDetails = new string[] { "Teacher", "Talk to Teacher", "Walk Up to go back to Village\n-Walk right to the Forest\n-Talk to the teacher" };
            questNPC.Add("30", questDetails);
            questDetails = new string[] { "Cesar", "Talk to Cesar", "Go back to the village\n-Talk to Cesar about children" };
            questNPC.Add("31", questDetails);
            questDetails = new string[] { "Child", "Find children", "Uno está por el río\n-el otro al lado del bosque" };
            questNPC.Add("32", questDetails);
            questDetails = new string[] { "Teacher", "Talk to Teacher", "" };
            questNPC.Add("34", questDetails);
            questDetails = new string[] { "Witch", "", "Talk to Witch Again for Next Steps" };
            questNPC.Add("35", questDetails);
            questDetails = new string[] { "Chef", "Talk to Chef", "Help the Chef" };
            questNPC.Add("40", questDetails);
            questDetails = new string[] { "Farmer", "Ask for Ingredients", "Talk to Farmer\n-pedirle arroz, leche, azúcar y canela" };
            questNPC.Add("41", questDetails);
            questDetails = new string[] { "Chef", "Go back to Chef", "Go back to the Chef and give him the ingredients" };
            questNPC.Add("42", questDetails);
            questDetails = new string[] { "Archeologist", "Go Talk to Archeologist", "Go talk to archeologist for next quest" };
            questNPC.Add("50", questDetails);
            questDetails = new string[] { "Archeologist", "List Objects", "List the objects that you found in the quest" };
            questNPC.Add("52", questDetails);
            questDetails = new string[] { "Mayor", "Talk to Mayor", "Talk to the mayor about the next quest" };
            questNPC.Add("60", questDetails);

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

    public void ChildPoints(int id)
    {
        Children.Add(id);
        if (Children.Count == 2)
        {
            SetQuestStep(3);
        }
    }

    public void AddSpell(string newSpell)
    {
        PlayerPrefs.SetString(newSpell, "true");
    }
}
