using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject Panel;
    public GameObject DialogueScript;
    private bool DialogueShown = false;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

            if (gameObject.GetComponent<PolygonCollider2D>().OverlapPoint((Vector2)touchPosition))
            {
                Panel.GetComponent<HideShowObjects>().Show();

                if (!DialogueShown)
                {
                    DialogueScript.GetComponent<NPCDialogueManager>().DisplayNextSentence();
                    DialogueShown = !DialogueShown;
                }
            }
        }

    }
}