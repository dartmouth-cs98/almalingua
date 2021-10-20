using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject NPC;
    private void Start()
    {
        NPC = GameObject.Find("NPC");
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Vector3 NPCPosition = NPC.transform.position;
            float difference = Vector3.Distance(touchPosition, NPCPosition);
            Debug.Log(difference);
            if (difference < 40.05)
            {
                Debug.Log("hi");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
        }

    }
}
