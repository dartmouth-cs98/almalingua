using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using System.Text.RegularExpressions;

public class NPCDialogueManager : MonoBehaviour
{
    public NPCConversation npcConversation;
    public string npcName;
    public Dictionary<string, string> entities;
    public string CurrentText;

    private System.Random rnd;

    private Conversation conversation;
    private ConversationNode currNode;

    private string currIntent;

    private const string QUEST = "quest";
    private const string QUEST_STEP = "questStep";
    private const string DEFAULT_INTENT = "hello";

    void Start()
    {
        rnd = new System.Random();
        entities = new Dictionary<string, string>();
        // Hard coding quest values for testing.
        entities[QUEST] = PlayerPrefs.GetInt("Quest").ToString();
        entities[QUEST_STEP] = PlayerPrefs.GetInt("QuestStep").ToString();
    }

    /******************   StartConversation  ************************/
    /* 
     * This function sets the current node to the first speech node in 
     * either a quest sub-tree or the default sub-tree.
     * 
     * The 1st message can be retrieved via "CurrentText"
     */
    public void StartConversation()
    {
        conversation = npcConversation.Deserialize();
        currNode = conversation.Root;
        Debug.Log("START CONVO!" + currNode.Text);
        currIntent = DEFAULT_INTENT;
        if (checkQuest())
        {
            currIntent = QUEST;
        }
        GetNextMessage();

    }

    /******************   UpdateIntent  ************************/
    /*
     * Used to update the intent. If Luis enabled, turns the 'input'
     * string into an intent by calling Luis API. Else, set intent 
     * to 'input'.
     *
     * [LUIS API NOT YET IMPLEMENTED]
     *
     */
    public void UpdateIntent(string input, bool sendToLuis = false)
    {
        if (sendToLuis == false)
        {
            currIntent = input;
        }
    }

    /**************  NextMessageRequiresInput() *****************/
    /*
     * If the current node's children are of type "OptionNode", return true. Otherwise, 
     * return false. If no children, returns false.
     */
    public bool NextMessageRequiresInput()
    {
        if (currNode.Connections.Count == 0)
        {
            return false;
        }
        else
        {
            return (currNode.Connections[0].ConnectionType == Connection.eConnectionType.Option);
        }
    }

    /*************** ConnectionConditionsValid ********************/
    /*
     * PRIVATE FUNCTION!
     * Dialogue Editor allows us to add conditions to dialogue connections,
     * where the connection is only valid if the condition is met. This
     * function parses the condition object, and returns whether or not it
     * is met.
     * 
     * If a connection has no conditions, returns true.
     */
    private bool ConnectionConditionsValid(Connection connection)
    {
        foreach (Condition condition in connection.Conditions)
        {

            // GetInt/GetBool updates this variable with OK/False. 
            // I'm assuming it's always going to be OK.
            eParamStatus paramStatus;

            // See Dialogue Editor documentation for information about Condition types.
            // 2 Types: IntCondition, BoolCondition.

            // Check each condition.
            if (condition.ConditionType == Condition.eConditionType.IntCondition)
            {

                IntCondition intCondition = (IntCondition)condition;
                switch (intCondition.CheckType)
                {
                    case IntCondition.eCheckType.equal:
                        if (!(conversation.GetInt(intCondition.ParameterName, out paramStatus)
                        == intCondition.RequiredValue))
                        {
                            return false;
                        }
                        break;

                    case IntCondition.eCheckType.lessThan:
                        if (!(conversation.GetInt(intCondition.ParameterName, out paramStatus)
                        < intCondition.RequiredValue))
                        {
                            return false;
                        }
                        break;

                    default:
                        if (!(conversation.GetInt(intCondition.ParameterName, out paramStatus)
                        > intCondition.RequiredValue))
                        {
                            return false;
                        }
                        break;
                }
            }
            else
            {

                BoolCondition boolCondition = (BoolCondition)condition;

                if (boolCondition.CheckType == BoolCondition.eCheckType.equal)
                {
                    if (!(conversation.GetBool(boolCondition.ParameterName, out paramStatus)
                    == boolCondition.RequiredValue))
                    {
                        return false;
                    }
                }
                else
                {
                    if (conversation.GetBool(boolCondition.ParameterName, out paramStatus)
                    == boolCondition.RequiredValue)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    /*************** OptionMatchesIntent ********************/
    /*
     * Each option text will have format "intent entityKey='entityVal' entityKey='entityVal'..."
     * This function will test whether the entities specified in text of option
     * match entities in dictionary. If a given entityKey not specified in dicctionary, is false.
     * 
     * (Currrently, just matches intent with no way to add entity).
     */
    private bool OptionMatchesIntent(string optionText, string intent)
    {
        Debug.Log("OPTIONMATCHESINTENT: " + optionText + " " + intent);
        if (optionText == null || optionText == "")
        {
            return false;
        }
        string optionIntent = optionText.Split(' ')[0];
        if (optionIntent != intent)
        {
            return false;
        }

        Regex regex = new Regex(@"(\w+)=(\w+)");
        MatchCollection matches = regex.Matches(optionText);
        foreach (Match match in matches)
        {
            string entityKey = match.Groups[1].Value;
            string entityVal = match.Groups[2].Value;

            Debug.Log("Searching for (" + entityKey + ", " + entityVal + ")");

            string dictVal = "";
            if (!entities.TryGetValue(entityKey, out dictVal) || dictVal != entityVal)
            {
                return false;
            }

            Debug.Log("dictVal was: " + dictVal);
        }

        Debug.Log("returning true");

        return true;
    }

    /*************** checkQuest ********************/
    /*
     * This function returns true/false if there
     * is an OptionNode child of the root representing the appropriate quest/step combo.
     */
    public bool checkQuest()
    {
        Debug.Log("checkQuestO");
        // TO DO HERE -- load in the quests from playerPrefs:
        entities[QUEST] = "1";
        entities[QUEST_STEP] = "1";

        foreach (Connection connection in conversation.Root.Connections)
        {
            if (connection.ConnectionType == Connection.eConnectionType.Option)
            {
                OptionNode optionNode = ((OptionConnection)connection).OptionNode;
                if (OptionMatchesIntent(optionNode.Text, "quest"))
                {
                    Debug.Log("CheckQuest returning true");
                    return true;
                }
            }
        }
        return false;
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
        // Iterate over each connection, add all valid to list of matches.
        foreach (Connection connection in currNode.Connections)
        {
            if (ConnectionConditionsValid(connection))
            {

                // Each connected node is of type Option or Speech. 
                // All connected nodes must be the same type.
                if (connection.ConnectionType == Connection.eConnectionType.Option)
                {
                    OptionNode option = ((OptionConnection)connection).OptionNode;
                    // Option only valid if its text matches currIntent.
                    if (OptionMatchesIntent(option.Text, currIntent))
                    {
                        matches.Add(option);

                    }
                }
                else
                {
                    matches.Add(((SpeechConnection)connection).SpeechNode);
                }
            }
        }
        if (matches.Count > 0)
        {
            // In case of multiple matches, return a random match.. 
            int matchIndex = rnd.Next(matches.Count);
            currNode = matches[matchIndex];
            // If we arrived at option node, advance 1x more in order so currNode points to speechNode.
            if (currNode.NodeType == ConversationNode.eNodeType.Option)
            {
                GetNextMessage();
                return null;
            }
            // We will return the text at current node.
            CurrentText = currNode.Text;
            // If the next node is a blank speech node, advance 1x more. We call these GROUPER nodes.
            // This node is hidden to the caller. Used for connecting multiple speech nodes to same set of outputs.
            if (currNode.Connections.Count > 0 && currNode.Connections[0].ConnectionType == Connection.eConnectionType.Speech)
            {
                SpeechNode nextNode = ((SpeechConnection)currNode.Connections[0]).SpeechNode;
                if (nextNode.Text == null)
                {
                    currNode = nextNode;
                }

            }
            return CurrentText;
        }
        else
        {
            return null;
        }
    }

}
