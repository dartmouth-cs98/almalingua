using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [System.Serializable]
    public class QuestMatching
    {
        public string questID;
        public string NPC;
    }

    [System.Serializable]
    public class QuestList
    {
        public QuestMatching[] questIDMatching;
    }
    public QuestList questIDMatching = new QuestList();
    public TextAsset JsonFile;
    public GameObject Panel;
    public int MyQuest;
    public GameObject Player;

    // Update is called once per frame

    private void Start()
    {
        questIDMatching = JsonUtility.FromJson<QuestList>(JsonFile.text);
    }
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

            if (gameObject.GetComponent<PolygonCollider2D>().OverlapPoint((Vector2)touchPosition) && Input.touches[i].phase == TouchPhase.Began)
            {
                Panel.GetComponent<HideShowObjects>().Show();
                Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence(gameObject.name);
            }
        }

    }

    private void OnMouseDown()
    {
        Panel.GetComponent<HideShowObjects>().Show();
        Panel.GetComponent<NPCDialogueUI>().DisplayNextSentence(gameObject.name);
        //StartQuest();
    }

    private void StartQuest()
    {
        Player.GetComponent<QuestManager>().SetQuest(MyQuest);
    }
}

