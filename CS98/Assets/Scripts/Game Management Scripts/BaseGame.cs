using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Create/BaseGame", order = 1)]
[System.Serializable]

public class BaseGame : ScriptableObject
{

    [System.Serializable]
    public class Quest
    {
        public string questname;
        public int questnum;
        public int prereq;
        public string description;
        public string[] steps;
    }

    [System.Serializable]
    public class QuestHolder
    {
        public Quest[] quests;
    }

    public QuestHolder qh;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
