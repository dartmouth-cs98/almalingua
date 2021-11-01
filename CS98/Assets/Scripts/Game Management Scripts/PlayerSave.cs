using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (! PlayerPrefs.HasKey("quest"))
        {
            PlayerPrefs.SetInt("quest", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateQuest(int quest)
    {
        PlayerPrefs.SetInt("quest", quest);
    }
}
