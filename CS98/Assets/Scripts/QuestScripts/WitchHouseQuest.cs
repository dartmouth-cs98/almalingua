using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchHouseQuest : MonoBehaviour
{
    public GameObject Protagonist;
    // Start is called before the first frame update
    void Start()
    {
        Protagonist.GetComponent<QuestManager>().SetQuestStep(0);
    }

    // Update is called once per frame
}
