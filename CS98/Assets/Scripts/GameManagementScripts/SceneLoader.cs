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

    public void LoadScene(string nextScene)
    {
        PlayerPrefs.SetString("PrevScene", currScene);
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
