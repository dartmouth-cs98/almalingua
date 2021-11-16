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
        Panel.GetComponent<HideShowObjects>().Show();
        Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence();
        if (gameObject.name == "Cesar")
        {
            foreach (Word w in Dictionary.playerDictionary.masterList.wlist)
            {
                w.encountered = true;
            }
        }
        if (gameObject.name == "Witch" && PlayerPrefs.GetInt("QuestStep") == 2)
        {
            GameObject Ice = GameObject.Find("Ice_Melting_Done-Sheet_0");
            Ice.GetComponent<IceCollider>().canMelt = true;

        }
        //StartQuest();
    }

    private void StartQuest()
    {
        Player.GetComponent<QuestManager>().SetQuest(MyQuest);
    }
}

