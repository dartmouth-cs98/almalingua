using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCDialogueManagerRay : MonoBehaviour
{
    public NPCConversation npcConversation;
    public SpeechNode currNode;

    private  string currIntent = "good";

    void Start()
    {
        // Instantiate(npcConversation, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
        Conversation conversation = npcConversation.Deserialize();
        currNode = conversation.Root;
        Debug.Log(GetNextMessage());
        Debug.Log(currNode.Text);
        Debug.Log(currNode.Connections);
        Debug.Log(GetNextMessage());
        Debug.Log(currNode.Text);
        Debug.Log(currNode.Connections);
        Debug.Log(GetNextMessage());
        Debug.Log(currNode.Text);
        Debug.Log(currNode.Connections);
        Debug.Log(GetNextMessage());
    }

    void UpdateIntent(string input, bool sendToLuis = false)
    {
      if (sendToLuis == false) {
        currIntent = input;
      }
    }

    string GetNextMessage()
    {
      bool didUpdate = false;
      foreach (Connection connection in currNode.Connections) {
        Debug.Log(connection.ConnectionType);
        if (connection.ConnectionType == Connection.eConnectionType.Option) {
          OptionNode option = ((OptionConnection)connection).OptionNode;
          if (option.Text.Equals(currIntent)) {
            currNode = ((SpeechConnection)option.Connections[0]).SpeechNode;
            didUpdate = true;
          }
        } else {
          currNode = ((SpeechConnection)connection).SpeechNode;
          didUpdate = true;
        }
      }
      if (didUpdate) {
        return currNode.Text;
      } else {
        return "";
      }
    }
}
