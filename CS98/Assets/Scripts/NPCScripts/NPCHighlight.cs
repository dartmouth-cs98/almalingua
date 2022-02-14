using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHighlight : MonoBehaviour
{

    private string currentQuest = "00";        //the current quest we are on
    private GameObject clickedSquarePrefab;           //the prefab square that shows up above a npc
    private GameObject clickedSquare;           //our actual square
    private bool highlight = false;               //if we already highlighted
    private bool firstTime = true;              //the first time we will instantiate our prefab, else we just set active/inactive


    private void OnEnable()
    {
        EventManager.onQuestChange += UpdateQuest;
    }

    private void OnDisable()
    {
        EventManager.onQuestChange -= UpdateQuest;
    }
    /**
    We are getting our current quest and checking if the current NPC is associated with our current quest
    **/
    private void Start()
    {
        UpdateQuest();
    }
    void UpdateQuest()
    {
        currentQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        string[] questDetails = new string[PlayerPrefs.GetInt("QuestLength")];
        if (QuestUI.questNPC.TryGetValue(currentQuest, out questDetails) && questDetails[0] == gameObject.name)  //if everything matches then we will add the box
        {
            HighlightNPC();
        }
        else
        {

            RemoveHighlightNPC();
        }
    }

    public void HighlightNPC()
    {
        if (!highlight)         //if the NPC was not previously highlighted (this is to make sure that this isn't called constantly)
        {
            if (firstTime)      //our firsttime, we will instantiate our prefab
            {
                clickedSquarePrefab = Resources.Load("Prefabs/NPCs/ClickedSquare") as GameObject;
                clickedSquare = GameObject.Instantiate(clickedSquarePrefab, gameObject.transform);
                clickedSquare.transform.position = new Vector3(gameObject.transform.position.x,
                                                            gameObject.transform.position.y + (gameObject.GetComponent<BoxCollider2D>().bounds.size.y) / 2 + clickedSquare.GetComponent<SpriteRenderer>().bounds.size.y
                                                            , 0);
                firstTime = false;
            }
            else
                clickedSquare.SetActive(true);
            highlight = !highlight;
        }

    }

    public void RemoveHighlightNPC()
    {
        if (highlight && !firstTime)    //opposite logic as above, removing our "box"
        {
            clickedSquare.SetActive(false);
            highlight = !highlight;
        }

    }
}
