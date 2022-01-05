using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHighlight : MonoBehaviour
{
    public static Dictionary<string, string> questNPC = new Dictionary<string, string>(); //our dictionary mapping questIDS to the NPC associated with it
    private string currentQuest = "00";        //the current quest we are on
    private GameObject clickedSquarePrefab;           //the prefab square that shows up above a npc
    private GameObject clickedSquare;           //our actual square
    private bool highlight = false;               //if we already highlighted
    private bool firstTime = true;              //the first time we will instantiate our prefab, else we just set active/inactive

    // Start is called before the first frame update
    void Start()
    {
        if (questNPC.Count < 4)
        {
            questNPC.Add("00", "Witch");
            questNPC.Add("10", "Witch");
            questNPC.Add("11", "Cesar");
            questNPC.Add("12", "Witch");
        }

    }

    /**
    We are getting our current quest and checking if the current NPC is associated with our current quest
    **/
    void Update()
    {
        currentQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        string NPCName;
        if (questNPC.TryGetValue(currentQuest, out NPCName) && NPCName == gameObject.name)  //if everything matches then we will add the box
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
                                                            gameObject.transform.position.y + (gameObject.GetComponent<PolygonCollider2D>().bounds.size.y) / 2 + clickedSquare.GetComponent<SpriteRenderer>().bounds.size.y
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
