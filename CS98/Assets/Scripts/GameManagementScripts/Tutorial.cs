using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/**
 * A script controlling the first tutorial scene UI and progression.
 *
 * Sada Nichols-Worley
 */

public class Tutorial : MonoBehaviour
{
    public Text InputField;
    public GameObject Obstacle;
    public GameObject PlayerObj;

    private string username = "";
    private int questStep = 0;


    // Start is called before the first frame update
    void Start()
    {
        QuestUI.SetQuest(9);
        QuestUI.SetQuestStep(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerPrefs.GetInt("QuestStep") == 0)
        {
            NextQuest();
        }
    }

    private void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("QuestStep") == 1)
        {
            NextQuest();
        }
    }


    // Updating which message is displayed in the text box
    public void NextQuest()
    {
        questStep += 1;
        QuestUI.SetQuestStep(questStep);

        if (PlayerPrefs.GetInt("QuestStep") == 2)
        {
            Obstacle.SetActive(false);
        }
    }

    // Saving entered username in case of later use for the player / online play
    public void EnteredUsername()
    {
        if (!InputField) return;
        username = InputField.text;
        PlayerSave.SetUsername(username);
    }

    
}
