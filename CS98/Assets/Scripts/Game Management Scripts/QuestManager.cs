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
        PlayerPrefs.SetInt("QuestStep", step);
    }

    public int GetQuestStep()
    {
        return PlayerPrefs.GetInt("QuestStep");
    }


}
