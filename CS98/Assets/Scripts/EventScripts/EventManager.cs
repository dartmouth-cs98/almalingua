using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Celina Tala

/**
THis is to organize the different events we have in our system
**/
public class EventManager : MonoBehaviour
{
    public delegate void ConversationStart();   //This can be used for different conversations
    public static event ConversationStart onConversationStart;


    //when conversation starts 
    public static void RaiseOnConversationStart()
    {
        if (onConversationStart != null)
        {
            onConversationStart();
        }
    }
}
