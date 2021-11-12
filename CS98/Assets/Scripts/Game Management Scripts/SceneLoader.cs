using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator Transition;      //our scene change animation


    public string NextScene;  //name of the scene to load next

    public void LoadScene()
    {
        StartCoroutine(SceneAnimate());
    }

    //adding the cross fade and loading next scene
    IEnumerator SceneAnimate()
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(NextScene);

    }
}
