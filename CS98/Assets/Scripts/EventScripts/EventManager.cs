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

    public delegate void ConversationEnd();     //This is to signal the end of a conversation 
    public static event ConversationEnd onConversationEnd;

    //when conversation starts 
    public static void RaiseOnConversationStart()
    {
        if (onConversationStart != null)
        {
            onConversationStart();
        }
    }

    public static void RaiseOnConversationEnd()
    {
        if (onConversationEnd != null)
        {
            onConversationEnd();
        }
    }
}
