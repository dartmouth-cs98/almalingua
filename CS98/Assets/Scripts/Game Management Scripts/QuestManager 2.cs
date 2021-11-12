using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public BaseGame baseGame;
    private int CurrentQuest;
    private int QuestStep;
    public GameObject QuestDisplay;
    public GameObject QuestDetails;
    public TextAsset QuestData;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Player pref quest on load: " +PlayerPrefs.GetInt("Quest"));

        LoadJSON.load_JSON(QuestData, baseGame);
        if (! PlayerPrefs.HasKey("Quest"))
        {
            PlayerPrefs.SetInt("Quest", 0);
            QuestStep = 0;
            PlayerSave.UpdateQuestStep(QuestStep);
        }
        CurrentQuest = PlayerPrefs.GetInt("Quest");
        QuestStep = PlayerPrefs.GetInt("QuestStep");
        UpdateQuest(CurrentQuest);
    }

    // update the stored quest in this class using player prefs
    // and update quest display text
    public void UpdateQuest(int curr=0)
    {
        CurrentQuest = PlayerPrefs.GetInt("Quest");
        TMPro.TextMeshProUGUI txt = QuestDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        txt.SetText(baseGame.qh.quests[CurrentQuest].questname);
        QuestDetails.SetActive(true);
        TMPro.TextMeshProUGUI det = QuestDetails.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        det.SetText(baseGame.qh.quests[CurrentQuest].description);
        QuestDetails.SetActive(false);
        Debug.Log("New player pref quest: " + PlayerPrefs.GetInt("Quest"));
        Debug.Log("This class quest: " + CurrentQuest);
    }

    public void UpdateQuestStep(int step=0)
    {
        PlayerSave.UpdateQuestStep(step);
        QuestStep = step;
    }

}
