using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUIExplainer : MonoBehaviour
{

    public GameObject InfoUI;

    void OnTriggerEnter2D(Collider2D col)
    {
        InfoUI.GetComponent<HideShowObjects>().Show();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        InfoUI.GetComponent<HideShowObjects>().Hide();
    }

}
