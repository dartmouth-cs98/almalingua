using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string triggerName;

    private GameObject trigger;
    private void Start()
    {
        trigger = GameObject.Find(triggerName);
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

            if (triggerName == "NPC" && trigger.GetComponent<PolygonCollider2D>().OverlapPoint((Vector2)touchPosition))
            {
                LoadScene("NPCInteractionTextBubble");
            }
        }

    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
