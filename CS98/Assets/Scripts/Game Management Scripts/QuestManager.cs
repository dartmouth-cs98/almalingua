using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager QuestMan;
    public BaseGame baseGame;
    public int CurrentQuest = 0;
    public int QuestStep = 0;
    public GameObject QuestDisplay;
    public GameObject QuestDetails;
    public TextAsset QuestData;


    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateQuest(int curr=0)
    {
        PlayerSave.UpdateQuest(curr);
        CurrentQuest = PlayerPrefs.GetInt("quest");
        TMPro.TextMeshProUGUI txt = QuestDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        txt.SetText(baseGame.qh.quests[CurrentQuest].questname);
        QuestDetails.SetActive(true);
        TMPro.TextMeshProUGUI det = QuestDetails.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        det.SetText(baseGame.qh.quests[CurrentQuest].description);
        QuestDetails.SetActive(false);
    }

    public void UpdateQuestStep(int step=0)
    {
        PlayerSave.UpdateQuestStep(step);
        QuestStep = step;
    }

}
