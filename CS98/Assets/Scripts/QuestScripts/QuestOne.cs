using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOne : MonoBehaviour
{
    public GameObject Protagonist;            //our speechbubble

    // Start is called before the first frame update

    private void Update()
    {

        float deltaX = Mathf.Pow(transform.position.x - Protagonist.transform.position.x, 2);
        float deltaY = Mathf.Pow(transform.position.y - Protagonist.transform.position.y, 2);
        float dist = Mathf.Sqrt(deltaX + deltaY);
        if (dist < 7)
        {
            Protagonist.GetComponent<QuestManager>().SetQuestStep(2);
        }

    }

}
