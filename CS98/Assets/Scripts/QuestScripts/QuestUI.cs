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
            string[] questDetails = new string[] { "Bruja", "Bienvenidos a Almalingua", "Usa las <sprite=\"arrow-keys\" name=\"arrow-keys\"> teclas de flecha para encontrar a la bruja en la playa.", null };
            questNPC.Add("00", questDetails);
            questDetails = new string[] { "Bruja", "Bienvenidos a Almalingua", "Habla con la bruja!", null };
            questNPC.Add("01", questDetails);
            questDetails = new string[] { "Bruja", "Pedir ayuda", "Encontre la bruja en su casa en el pueblo.",null };
            questNPC.Add("10", questDetails);
            questDetails = new string[] { "Cesar", "Pídele direcciones a César", "Cesar es el reno en medio del pueblo.",null };
            questNPC.Add("11", questDetails);
            questDetails = new string[] { "Bruja", "Encuentra tu varita", "Ve a la izquierda, arriba, luego a la izquierda. Está al lado del hospital.",null };
            questNPC.Add("12", questDetails);
            questDetails = new string[] { "Granjero", "Hable con el granjero", "La granja está debajo del pueblo. El granjero está en medio de la granja.",null };
            questNPC.Add("20", questDetails);
            questDetails = new string[] { "Slime_Green", "Luche contra el limo verde", "El limo está al lado de las papas y las zanahorias.", "Farm" };
            questNPC.Add("21", questDetails);
            questDetails = new string[] { "Granjero", "Hable con el granjero", "El granjero te dirá qué hacer a continuación." , null};
            questNPC.Add("22", questDetails);
            questDetails = new string[] { "Slime_Orange", "Lucha contra el limo naranja", "El limo está al lado del maíz y el trigo." ,"Farm" };
            questNPC.Add("23", questDetails);
            questDetails = new string[] { "Granjero", "Hable con el granjero", "El granjero te dirá qué hacer a continuación.", null};
            questNPC.Add("24", questDetails);
            questDetails = new string[] { "Slime_Black", "Lucha contra el limo negro","El limo está al lado de los aguacates y las naranjas.", "Farm" };
            questNPC.Add("25", questDetails);
            questDetails = new string[] { "Granjero", "Hable con el granjero", "El granjero te dirá qué hacer a continuación.",null };
            questNPC.Add("26", questDetails);
            questDetails = new string[] { "Bruja", "Hable con la bruja", "",null };
            questNPC.Add("27", questDetails);
            
            questDetails = new string[] { "Maestra", "Hable con la maestra", "La maestra está en el bosque, a la derecha del pueblo." ,null};
            questNPC.Add("30", questDetails);
            questDetails = new string[] { "Cesar", "Hable con César", "Vuelve al pueblo y pregúntale al reno César dónde están los niños." ,null};
            questNPC.Add("31", questDetails);
            questDetails = new string[] { "Niño", "Encuentra a los niños", "Uno está por el río y el otro está al lado del estanque.",null };
            questNPC.Add("32", questDetails);
            questDetails = new string[] { "Maestra", "Lleva los niños a la maestra", "",null };
            questNPC.Add("33", questDetails);
            questDetails = new string[] { "Devil", "Lucha contra el monstruo", "El monstruo está en el bosque." ,"Forest"};
            questNPC.Add("34", questDetails);
            questDetails = new string[] { "Maestra", "Hable con la maestra", "Dile a la maestra dónde estaban los niños." ,null};
            questNPC.Add("35", questDetails);
            questDetails = new string[] { "Bruja", "Hable con la bruja", "" ,null};
            questNPC.Add("36", questDetails);

            questDetails = new string[] { "Chef", "Hable con la cocinera", "La cocinera es una araña en el pueblo.",null };
            questNPC.Add("40", questDetails);
            questDetails = new string[] { "Granjero", "Pregunta por los ingredientes al granjero.", "Pide leche, azúcar y pimientos al granjero en la granja." ,null};
            questNPC.Add("41", questDetails);
            questDetails = new string[] { "Chef", "Hable con la chef", "Dale a la chef los ingredientes." ,null};
            questNPC.Add("42", questDetails);
            questDetails = new string[] { "Arqueólogo", "Hable con el arqueólogo", "El arqueólogo está en el pueblo." ,null};
            questNPC.Add("50", questDetails);
            questDetails = new string[] { "", "Encuentra el sartén", "El sartén está en la zona superior derecha de las ruinas." ,null};
            questNPC.Add("51", questDetails);
            questDetails = new string[] { "Devil", "Lucha contra el monstruo", "El monstruo está en las ruinas." ,"Ruins"};
            questNPC.Add("52", questDetails);

            questDetails = new string[] { "", "Encuentre el tazón.", "El tazón está en el área central izquierda." ,null};
            questNPC.Add("53", questDetails);
            questDetails = new string[] { "Devil", "Lucha contra el monstruo", "El monstruo está en las ruinas." ,"Ruins"};
            questNPC.Add("54", questDetails);

            questDetails = new string[] { "", "Encuentra las tijeras", "Las tijeras están en el área central derecha de las ruinas." ,null};
            questNPC.Add("55", questDetails);
            questDetails = new string[] { "Devil", "Lucha contra el monstruo", "El monstruo está en las ruinas." ,"Ruins"};
            questNPC.Add("56", questDetails);

            questDetails = new string[] { "Arqueólogo", "Hable con el arqueólogo", "Dile al arqueólogo lo que encontraste.",null };
            questNPC.Add("57", questDetails);

            questDetails = new string[] { "Alcalde", "Hable con el alcalde", "El alcalde está en el pueblo.",null };
            questNPC.Add("60", questDetails);
            questDetails = new string[] { "Doctor", "Hable con el doctor", "Pregunta por la poción.",null };
            questNPC.Add("61", questDetails);

            questDetails = new string[] { "", "Busca flores", "Las flores están en el bosque.",null };
            questNPC.Add("62", questDetails);
            questDetails = new string[] { "Devil", "Lucha contra el monstruo", "El monstruo está en el bosque.","Forest" };
            questNPC.Add("63", questDetails);

            questDetails = new string[] { "Doctor", "Hable con el doctor", "Dale al doctor las flores.",null };
            questNPC.Add("64", questDetails);
            questDetails = new string[] { "Bruja", "Habla con la bruja", "Habla con la bruja y pregunta por un libro.",null };
            questNPC.Add("65", questDetails);

            questDetails = new string[] { "Granjero",  "Hable con el granjero", "Habla con el granjero para conseguir la manzana dorada.",null };
            questNPC.Add("66", questDetails);
            questDetails = new string[] { "Alcalde", "Hable con el alcalde", "Devolver los artículos al alcalde.",null };
            questNPC.Add("67", questDetails);
            questDetails = new string[] { "Arqueólogo", "Hable con el arqueólogo", "",null };
            questNPC.Add("68", questDetails);

            questDetails = new string[] { "Duo_Enemy", "Lucha contra el monstruo", "El monstruo está en las ruinas." ,"Ruins" };
            questNPC.Add("70", questDetails);
            questDetails = new string[] { "Portal", "Di el hechizo y vete a casa.", "Di \"revierte\" " ,null };
            questNPC.Add("71", questDetails);

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
            UpdateText(IconsInText.GetTextWithIcons(questDetails[1]), IconsInText.GetTextWithIcons(questDetails[2]));
        }

    }

    public static void SetQuest(int quest)
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
        gameObject.transform.Find("QuestTitle").GetComponent<TMPro.TextMeshProUGUI>().text = title;
        gameObject.transform.Find("QuestDetails").GetComponent<TMPro.TextMeshProUGUI>().text = descrip;
    }
    public void WitchSpeak()
    {
        GameObject.Find("Bruja").GetComponent<NPCInteraction>().StartDialogue();
    }

    public void GiveSpell()
    {
        Vector2 PlayerPosition = GameObject.Find("/CameraPlayer/PlayerManager/Protagonist").transform.position;
        Witch = GameObject.Find("Bruja");
        Witch.transform.position = new Vector2(PlayerPosition.x - 0.5f, PlayerPosition.y);
    }

    public void ChildPoints(int id)
    {
        Children.Add(id);
        
    }

    public void SpawnCombatSystem() {
        if (Children.Count == 2) {
            SetQuestStep(3);
        }
    }

    public void AddSpell(string newSpell)
    {
        if (!PlayerPrefs.HasKey(newSpell)) 
            PlayerPrefs.SetString(newSpell, "true");
    }
}
