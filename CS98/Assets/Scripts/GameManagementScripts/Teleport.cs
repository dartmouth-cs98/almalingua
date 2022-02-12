using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject SceneLoader;  //our sceneloader script
    public string NextScene;        //the next scene to go to
    private void OnCollisionEnter2D(Collision2D other)
    {

        SceneLoader.GetComponent<SceneLoader>().LoadScene(NextScene);
    }


}
