using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSetter : MonoBehaviour
{
    public int Quest;
    public int QuestStep;

    private void Awake()
    {
        PlayerPrefs.SetInt("Quest", Quest);
        PlayerPrefs.SetInt("QuestStep", QuestStep);
    }
}
