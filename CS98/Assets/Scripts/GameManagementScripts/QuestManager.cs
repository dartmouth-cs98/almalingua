using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public BaseGame baseGame;
    public GameObject QuestDisplay;
    public GameObject QuestDetails;
    public TextAsset QuestData;

    // Start is called before the first frame update
    void Start()
    {
        // LoadJSON.load_JSON(QuestData, baseGame);
        if (!PlayerPrefs.HasKey("Quest"))
        {
            PlayerPrefs.SetInt("Quest", 1);
            PlayerPrefs.SetInt("QuestStep", 0);
        }
        // UpdateQuestDisplay();
    }

    // getters and setters for quest and quest step
    public void SetQuest(int quest)
    {
        PlayerPrefs.SetInt("Quest", quest);
        // UpdateQuestDisplay();
    }

    public int GetQuest()
    {
        return PlayerPrefs.GetInt("Quest");
    }

    public void SetQuestStep(int step)
    {
        print(GetQuest() + " " + GetQuestStep());
        PlayerPrefs.SetInt("QuestStep", step);
    }

    public int GetQuestStep()
    {
        return PlayerPrefs.GetInt("QuestStep");
    }


}
