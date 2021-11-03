using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCDialogueManagerRay : MonoBehaviour
{
    public NPCConversation npcConversation;
    public string npcName;

    private System.Random rnd;

    private Conversation conversation;
    private ConversationNode currNode;
    private ConversationNode rootNode;
    
    private string currIntent = "good";
    private Dictionary<string, string> entities;

    void Start()
    {
        rnd = new System.Random();
        conversation = npcConversation.Deserialize();
        currNode = conversation.Root;
        entities = new Dictionary<string, string>();
    }

  /******************     ****************************/
    public void UpdateIntent(string input, bool sendToLuis = false)
    {
      if (sendToLuis == false) {
        currIntent = input;
      }
    }

    /**************  GetCurrentMessage() *****************/
    /*
     * Returns text of current node, without altering current node.
     */
    public string GetCurrentMessage()
    {
      return currNode.Text;
    }

    /**************  SaveEntity() *****************/
    /*
     * Entity which abstracts the role of entities in this.
     */
    public void SaveEntity(string key, string value)
    {
      entities.Add(key, value);
    }

    /*************** ConnectionConditionsMatch ********************/
    /*
     * Dialogue Editor allows us to add conditions to dialogue connections,
     * where the connection is only valid if the condition is met. This
     * function parses the condition object, and returns whether or not it
     * is met.
     * 
     * If a connection has no conditions, returns true.
     *
     */
    private bool ConnectionConditionsMatch(Connection connection) 
    {
      foreach (Condition condition in connection.Conditions) {

        // GetInt/GetBool updates this variable with OK/False. 
        // I'm assuming it's always going to be OK.
        eParamStatus paramStatus;


        // See Dialogue Editor documentation for information about Condition types.
        // 2 Types: IntCondition, BoolCondition.

        // Check each condition.
        if (condition.ConditionType == Condition.eConditionType.IntCondition) {

          IntCondition intCondition = (IntCondition)condition;
          switch (intCondition.CheckType)
          {
            case IntCondition.eCheckType.equal:
              if (!(conversation.GetInt(intCondition.ParameterName, out paramStatus) 
              == intCondition.RequiredValue)) {
                return false;
              }
              break;

            case IntCondition.eCheckType.lessThan:
              if (!(conversation.GetInt(intCondition.ParameterName, out paramStatus) 
              < intCondition.RequiredValue)) {
                return false;
              }
              break;

            default:
              if (!(conversation.GetInt(intCondition.ParameterName, out paramStatus) 
              > intCondition.RequiredValue)) {
                return false;
              }
              break;
          }
        } else {

          BoolCondition boolCondition = (BoolCondition)condition;

          if (boolCondition.CheckType == BoolCondition.eCheckType.equal) {
            if (!(conversation.GetBool(boolCondition.ParameterName, out paramStatus) 
            == boolCondition.RequiredValue)) {
              return false;
            }
          } else {
            if (conversation.GetBool(boolCondition.ParameterName, out paramStatus) 
            == boolCondition.RequiredValue) {
              return false;
            }
          }
        }
      }
    return true;
    }

    /**************  GetNextMessage() *****************/
    /*
     * Find the current speech node's connections. If this node is connected to
     * OptionNode(s), advance to (last) OptionNode whose text matches currIntent, 
     * then advance to that OptionNode's SpeechNode child.
     * 
     * If current node's children are SpeechNode(s), advance to (last) SpeechNode
     * whose condition resolves to true.
     *
     * Returns text of new current node after advancement.
     * If no advancement because of no matches, returns null.
     * 
     */
    public string GetNextMessage()
    {
      bool didUpdate = false;
      List<ConversationNode> matches = new List<ConversationNode>(); 

      foreach (Connection connection in currNode.Connections) {
        if (ConnectionConditionsMatch(connection)) {

          // Each connected node is of type Option or Speech. 
          // All connected nodes must be the same type.
          if (connection.ConnectionType == Connection.eConnectionType.Option) {
            OptionNode option = ((OptionConnection)connection).OptionNode;

            // Option only valid if its text matches currIntent.
            if (option.Text.Equals(currIntent)) {
              matches.Add(option); 
            }
          } else {
            matches.Add(((SpeechConnection)connection).SpeechNode);
          }
        }
      }

      if (matches.Count > 0) {
        // In case of multiple matches, return a random match.. 
        int matchIndex = rnd.Next(matches.Count);
        currNode = matches[matchIndex];

        // If we arrived at option node, advance 1x more in order so currNode points to speechNode.
        if (currNode.NodeType == ConversationNode.eNodeType.Option) {
          GetNextMessage();
        }
        return currNode.Text;
      } else {
        return null;
      }
    }

}
