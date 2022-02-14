using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestVisible : MonoBehaviour
{

    public int ENABLE_ON_QUEST = 0;
    public int ENABLE_ON_QUEST_STEP = 0;

    void Start()
    {
      CheckVisibility();
    }

    void CheckVisibility()
    {
        Debug.Log("This object is checking quest");
        Debug.Log(gameObject.name);
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }

        if (ENABLE_ON_QUEST == QuestUI.GetQuest()) 
        {
            if (ENABLE_ON_QUEST_STEP == QuestUI.GetQuestStep())
            {
              foreach (Transform child in transform) {
                  child.gameObject.SetActive(true);
              }
              Debug.Log("Matching quest, questStep!");
            } else {
              Debug.Log("Matching quest, not matching questStep!");
            }
        }
        else {
          Debug.Log("No matching quest.");
        }
    }

    void OnEnable()
    {
      EventManager.onQuestChange += CheckVisibility;
    }

    void OnDisable()
    {
      EventManager.onQuestChange -= CheckVisibility;
    }
}