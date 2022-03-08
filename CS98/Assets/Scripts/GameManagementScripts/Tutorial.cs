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
        QuestUI.SetQuest(0);
        QuestUI.SetQuestStep(0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (QuestUI.GetQuestStep() == 0 && QuestUI.GetQuest() == 0)
        {
            QuestUI.SetQuestStep(1);
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
