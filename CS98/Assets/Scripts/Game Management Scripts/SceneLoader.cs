using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator Transition;      //our scene change animation
    public bool ChangeQuest;        //if we need to change the quest on the sceneloader
    private GameObject protagonist;  //protagonist 


    private void Start()
    {
        int currentQuest = PlayerPrefs.GetInt("Quest");
        protagonist = GameObject.Find("Protagonist");
        // if (currentQuest > 1)
        // {
        //     protagonist = GameObject.Find("Protagonist");
        // }
        // else
        // {
        //     protagonist = GameObject.Find("init_Protagonist");
        // }

    }
    public void LoadScene(string nextScene)
    {
        if (ChangeQuest)
        {
            int currentStep = protagonist.GetComponent<QuestManager>().GetQuestStep();
            protagonist.GetComponent<QuestManager>().SetQuestStep(1);
            // Debug.Log("past step is " + currentStep + " Current Step is " + protagonist.GetComponent<QuestManager>().GetQuestStep());
        }
        StartCoroutine(SceneAnimate(nextScene));
    }

    //adding the cross fade and loading next scene
    IEnumerator SceneAnimate(string nextScene)
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);

    }
}
