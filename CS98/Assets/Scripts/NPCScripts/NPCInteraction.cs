using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{

    public GameObject Panel;
    public int MyQuest;
    public GameObject Player;

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

    private void OnMouseDown()
    {
        Debug.Log("inside");
        Panel.GetComponent<HideShowObjects>().Show();
        Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence();
        StartQuest();
    }

    private void StartQuest()
    {
        PlayerSave.UpdateQuest(MyQuest);
        Player.GetComponent<QuestManager>().UpdateQuest(MyQuest);
    }
}

