using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public static Dictionary<string, string[]> questNPC = new Dictionary<string, string[]>(); //our dictionary mapping questIDS to the NPC associated with it
    string questTask;
    string currentQuest;
    private static HashSet<int> Children = new HashSet<int>();
    GameObject Witch;
    public GameObject SceneLoader;

    // Start is called before the first frame update
    private void OnEnable()
    {
        int questLength = 4;
        PlayerPrefs.SetInt("QuestLength", questLength);
        if (questNPC.Count == 0)
        {
            string[] questDetails = new string[] { "Witch", "Witch Talk", "Talk to the witch" , null};
            questNPC.Add("00", questDetails);
            questDetails = new string[] { "Witch", "Witch Talk", "Talk to the witch",null };
            questNPC.Add("10", questDetails);
            questDetails = new string[] { "Cesar", "Reindeer", "Go Talk to Reindeer",null };
            questNPC.Add("11", questDetails);
            questDetails = new string[] { "Witch", "Find Wand", "Yo te digo dónde está\n-Ve a la izquierda\n-después hacia arriba\n-y finalmente a la izquierda otra vez.  Está al lado del hospital.",null };
            questNPC.Add("12", questDetails);
            questDetails = new string[] { "Farmer", "Talk to Farmer", "Talk to Farmer\n-Keep walking straight down and enter the Farm\n-He will be to your right",null };
            questNPC.Add("20", questDetails);
            questDetails = new string[] { "Slime_Green", "Fight the Green Slime", "The slime is next to potatos and carrots", "Farm" };
            questNPC.Add("21", questDetails);
            questDetails = new string[] { "Farmer", "Talk to Farmer", "Talk to Farmer about next quest" , null};
            questNPC.Add("22", questDetails);
            questDetails = new string[] { "Slime_Orange", "Fight the Orange Slime", "Slime is next to corn and wheat" ,"Farm" };
            questNPC.Add("23", questDetails);
            questDetails = new string[] { "Farmer", "Talk to Farmer", "Talk to Farmer about next quest", null};
            questNPC.Add("24", questDetails);
            questDetails = new string[] { "Slime_Black", "Fight the Black Slime","Slime is next to avocados and oranges", "Farm" };
            questNPC.Add("25", questDetails);
            questDetails = new string[] { "Farmer", "Talk to Farmer", "Talk to Farmer",null };
            questNPC.Add("26", questDetails);
            questDetails = new string[] { "Witch", "", "Talk to Witch Again for Next Steps",null };
            questNPC.Add("27", questDetails);
            questDetails = new string[] { "Teacher", "Talk to Teacher", "Walk Up to go back to Village\n-Walk right to the Forest\n-Talk to the teacher" ,null};
            questNPC.Add("30", questDetails);
            questDetails = new string[] { "Cesar", "Talk to Cesar", "Go back to the village\n-Talk to Cesar about children" ,null};
            questNPC.Add("31", questDetails);
            questDetails = new string[] { "Child", "Find children", "Uno está por el río\n-el otro al lado del bosque",null };
            questNPC.Add("32", questDetails);
            questDetails = new string[] { "Devil", "Fight the Devil", "Walk around the forest and find the devil" ,"Forest"};
            questNPC.Add("33", questDetails);
            questDetails = new string[] { "Teacher", "Talk to Teacher", "Talk to the Teacher again" ,null};
            questNPC.Add("34", questDetails);
            questDetails = new string[] { "Witch", "", "Talk to Witch Again for Next Steps" ,null};
            questNPC.Add("35", questDetails);
            questDetails = new string[] { "Chef", "Talk to Chef", "Help the Chef\n-Go Right back to the village",null };
            questNPC.Add("40", questDetails);
            questDetails = new string[] { "Farmer", "Ask for Ingredients", "Talk to Farmer\n-pedirle arroz, leche, azúcar y canela" ,null};
            questNPC.Add("41", questDetails);
            questDetails = new string[] { "Chef", "Go back to Chef", "Go back to the Chef and give him the ingredients" ,null};
            questNPC.Add("42", questDetails);
            questDetails = new string[] { "Archeologist", "Go Talk to Archeologist", "Go talk to archeologist for next quest" ,null};
            questNPC.Add("50", questDetails);
            questDetails = new string[] { "", "Find the Pan", "Walk around and find the pan\n-It is near the top right" ,null};
            questNPC.Add("51", questDetails);
            questDetails = new string[] { "Devil", "Fight the devil", "Fight the devil" ,"Ruins"};
            questNPC.Add("52", questDetails);

            questDetails = new string[] { "", "Find the Bowl", "Walk around and find the bowl\n-It is near the middle left" ,null};
            questNPC.Add("53", questDetails);
            questDetails = new string[] { "Devil", "Fight the devil", "Fight the devil" ,"Ruins"};
            questNPC.Add("54", questDetails);

            questDetails = new string[] { "", "Find the Scissors", "Walk around and find the scissors\n-It is near the middle right" ,null};
            questNPC.Add("55", questDetails);
            questDetails = new string[] { "Devil", "Fight the devil", "Fight the devil" ,"Ruins"};
            questNPC.Add("56", questDetails);

            questDetails = new string[] { "Archeologist", "List Objects", "Go back to the archeologist in the village\n-List the objects that you found in the quest",null };
            questNPC.Add("57", questDetails);

            questDetails = new string[] { "Mayor", "Talk to Mayor", "Talk to the mayor about the next quest",null };
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
        Debug.Log("Quest " + GetQuest() + " Step " + GetQuestStep());
    }

    public static void SetQuestStep(int step)
    {
        PlayerPrefs.SetInt("QuestStep", step);
        EventManager.RaiseOnQuestChange();
        Debug.Log("Quest " + GetQuest() + " Step " + GetQuestStep());
    }

    public static int GetQuest()
    {
        return PlayerPrefs.GetInt("Quest");
    }

    public static int GetQuestStep()
    {
        return PlayerPrefs.GetInt("QuestStep");
    }


    void UpdateText(string title, string descrip)
    {
        gameObject.transform.Find("ScrollArea").Find("Content").Find("QuestTitle").GetComponent<TMPro.TextMeshProUGUI>().text = title;
        gameObject.transform.Find("ScrollArea").Find("Content").Find("QuestDetails").GetComponent<TMPro.TextMeshProUGUI>().text = "-" + descrip;
    }
    public void WitchSpeak()
    {
        SetQuestStep(4);
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
        
    }

    public void SpawnCombatSystem() {
        if (Children.Count == 2) {
            SetQuestStep(3);
            SceneLoader.GetComponent<SceneLoader>().LoadScene("combatScene");
        }
    }

    public void AddSpell(string newSpell)
    {
        if (!PlayerPrefs.HasKey(newSpell)) 
            PlayerPrefs.SetString(newSpell, "true");
    }
}
