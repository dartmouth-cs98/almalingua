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

        Debug.Log("Player pref quest on load: " + PlayerPrefs.GetInt("Quest"));

        LoadJSON.load_JSON(QuestData, baseGame);
        if (!PlayerPrefs.HasKey("Quest"))
        {
            PlayerPrefs.SetInt("Quest", 0);
            PlayerPrefs.SetInt("QuestStep", 0);
        }
        UpdateQuestDisplay();
    }

<<<<<<< HEAD
    // update the stored quest in this class using player prefs
    // and update quest display text
    public void UpdateQuest(int curr = 0)
=======
    // getters and setters for quest and quest step
    public void SetQuest(int quest)
    {
        PlayerPrefs.SetInt("Quest", quest);
        UpdateQuestDisplay();
    }

    public int GetQuest()
    {
        return PlayerPrefs.GetInt("Quest");
    }

    public void SetQuestStep(int step)
    {
        PlayerPrefs.SetInt("Quest", step);
    }

    public int GetQuestStep()
    {
        return PlayerPrefs.GetInt("QuestStep");
    }

    // update quest display text using public UI objects
    public void UpdateQuestDisplay()
>>>>>>> b247c6198d11ddc927071fef0f09d3ed580e4dd3
    {
        TMPro.TextMeshProUGUI txt = QuestDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        txt.SetText(baseGame.qh.quests[GetQuest()].questname);
        QuestDetails.SetActive(true);
        TMPro.TextMeshProUGUI det = QuestDetails.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        det.SetText(baseGame.qh.quests[GetQuest()].description);
        QuestDetails.SetActive(false);
        Debug.Log("New player pref quest: " + PlayerPrefs.GetInt("Quest"));
<<<<<<< HEAD
        Debug.Log("This class quest: " + CurrentQuest);
    }

    public void UpdateQuestStep(int step = 0)
    {
        PlayerSave.UpdateQuestStep(step);
        QuestStep = step;
=======
        Debug.Log("This class quest: " + GetQuest());
>>>>>>> b247c6198d11ddc927071fef0f09d3ed580e4dd3
    }

}
