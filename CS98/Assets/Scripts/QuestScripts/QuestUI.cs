using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public static Dictionary<string, string> questNPC = new Dictionary<string, string>(); //our dictionary mapping questIDS to the NPC associated with it

    private int Quest;
    private int QuestStep;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Quest = PlayerPrefs.GetInt("Quest");
        QuestStep = PlayerPrefs.GetInt("QuestStep");
    }

    private void Start()
    {
        if (questNPC.Count < 4)
        {
            questNPC.Add("00", "Witch");
            questNPC.Add("10", "Witch");
            questNPC.Add("11", "Cesar");
            questNPC.Add("12", "Witch");
        }

    }
}
