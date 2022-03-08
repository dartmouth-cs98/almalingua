using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator Transition;      //our scene change animation

    string currScene;
    private void Awake() {
        currScene = SceneManager.GetActiveScene().name;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape))
        {
            LoadScene("TitleScreen");
             PlayerPrefs.DeleteAll();
        }
    }
    public void LoadScene(string nextScene)
    {
        int q =  PlayerPrefs.GetInt("Quest");
        int step = PlayerPrefs.GetInt("QuestStep");
        PlayerPrefs.SetString("PrevScene", currScene);
        if (q < 7 && step < 1){
            StartCoroutine(SceneAnimate(nextScene));
        }
        else{
            SceneManager.LoadScene(nextScene);
        } 
    }

    //adding the cross fade and loading next scene
    IEnumerator SceneAnimate(string nextScene)
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);

    }
}
