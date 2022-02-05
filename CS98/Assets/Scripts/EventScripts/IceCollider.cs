using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Celina Tala

/**
This class is to deal with the ice melting animation
*/
public class IceCollider : MonoBehaviour
{
    public GameObject PlayerManager;
    public bool canMelt = false;

    private void OnEnable()
    {
        EventManager.onConversationStart += ConversationStart;
    }

    private void OnDisable()
    {
        EventManager.onConversationStart -= ConversationStart;
    }

    public void ConversationStart()
    {
        canMelt = true;
    }
    public void MeltIce()
    {
        GameObject.Find("IceMeltingAnimation").GetComponent<Animator>().Play("Ice_Melting_Animation");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canMelt)
        {
            PlayerManager.GetComponent<showProtagonist>().Switch();
            EventManager.RaiseOnProtagonistChange();
            gameObject.SetActive(false);
        }
    }
}
