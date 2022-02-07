using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start() {
        string currentQuest = PlayerPrefs.GetInt("Quest").ToString() + (PlayerPrefs.GetInt("QuestStep")-1).ToString();
        string []questDetails = new string[PlayerPrefs.GetInt("QuestLength")];
        if (QuestUI.questNPC.TryGetValue(currentQuest, out questDetails))
        {
            string NPCName = questDetails[0];
            print("NPC Name: " + NPCName);
            if (NPCName == gameObject.name) {
                gameObject.SetActive(false);
            }
        }
    }
}