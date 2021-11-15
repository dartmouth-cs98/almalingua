using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOne : MonoBehaviour
{
    public GameObject Protagonist;            //our speechbubble

    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D other)
    {
        Protagonist.GetComponent<QuestManager>().SetQuestStep(2);

    }

}
