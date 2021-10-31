using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public BaseGame baseGame;
    public int currentQuest = 0;
    public GameObject QuestDisplay;
    public GameObject QuestDetails;


    // Start is called before the first frame update
    void Start()
    {
        TMPro.TextMeshProUGUI txt = QuestDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        txt.SetText(baseGame.qh.quests[currentQuest].questname);
        TMPro.TextMeshProUGUI det = QuestDetails.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        det.SetText(baseGame.qh.quests[currentQuest].description);
        QuestDetails.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void updateQuest(int curr=0)
    {
        currentQuest = curr;
    }
}
