using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Character;    //character we want to teleport
    public GameObject SceneLoader;  //our sceneloader script
    private void OnCollisionEnter2D(Collision2D other)
    {
        SceneLoader.GetComponent<SceneLoader>().LoadScene();
    }
}
