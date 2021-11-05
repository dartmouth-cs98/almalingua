using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{

    public GameObject Panel;
    public int MyQuest;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

            if (gameObject.GetComponent<PolygonCollider2D>().OverlapPoint((Vector2)touchPosition) && Input.touches[i].phase == TouchPhase.Began)
            {
                Panel.GetComponent<HideShowObjects>().Show();
                Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartQuest();
        }
    }

    private void StartQuest()
    {
        QuestManager.QuestMan.UpdateQuest(MyQuest);
    }
}

