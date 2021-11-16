using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
  // UnityEngine.Events.UnityEvent firstEvent;

    void Start()
    {
      // if (firstEvent == null) {
      //  firstEvent = new UnityEngine.Events.UnityEvent();
      //}
          
      //firstEvent.AddListener(Ping);
    }


    void Ping()
    {
      Debug.Log("PING!");
    }

}
