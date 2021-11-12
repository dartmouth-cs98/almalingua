using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSave : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // initialize current quest/quest step to the first ones
        if (! PlayerPrefs.HasKey("Quest"))
        {
            PlayerPrefs.SetInt("Quest", 0);
            PlayerPrefs.SetInt("QuestStep", 0);
        }

        PlayerPrefs.SetString("User", "Player 1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateQuest(int quest)
    {
        PlayerPrefs.SetInt("Quest", quest);
    }

    public static void UpdateQuestStep(int step)
    {
        PlayerPrefs.SetInt("QuestStep", step);
    }

    public static void SetUsername(string user)
    {
        PlayerPrefs.SetString("User", user);
    }
}
