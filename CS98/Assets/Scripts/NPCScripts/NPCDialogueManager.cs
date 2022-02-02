using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using CandyCoded.env;
using Newtonsoft.Json.Linq;

public class NPCDialogueManager : MonoBehaviour
{
    public NPCConversation MyNPCConversation;
    public Dictionary<string, string> Entities;
    public string CurrentText;
    public bool IsLoading;


    private System.Random rnd;

    private Conversation conversation;
    private ConversationNode currNode;

    private string currIntent;

    private const string QUEST = "quest";
    private const string QUEST_STEP = "questStep";
    private const string DEFAULT_INTENT = "hello";


    private const double MIN_ACCEPTABLE_SCORE = 0.2;
    private const string ERR_INTENT = "none";
    private GameObject Player;

    void Awake()
    {
        rnd = new System.Random();
        Entities = new Dictionary<string, string>();
        IsLoading = false;
        Player = GameObject.Find("PlayerManager/init_Protagonist");
    }

    private void setCurrentText(string currentText)
    {
        string currentTextWithIcons = "";
        foreach (string word in currentText.Split(' '))
        {
            string cleanWord = Regex.Replace(word, "[^0-9a-zA-Z ]+", "").ToLower();
            string icon = "";
            if (Dictionary.wordIdMap.ContainsKey(cleanWord))
            {
                icon = ((Word)Dictionary.wordIdMap[cleanWord]).icon;
                if (icon != null)
                {
                    print("icon! for word:" + word);
                }
            }
            currentTextWithIcons += icon + word + ' ';
        }
        CurrentText = currentTextWithIcons.Substring(0, currentTextWithIcons.Length - 1);
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

        EventManager.RaiseOnConversationStart();
        conversation = MyNPCConversation.Deserialize();
        currNode = conversation.Root;
        currIntent = DEFAULT_INTENT;
        if (checkQuest())
        {
            currIntent = QUEST;
        }
        GetNextMessage();
    }

    private const string LUIS_ENDPOINT = "https://westus.api.cognitive.microsoft.com/luis/prediction/v3.0/apps";

    /******************   UpdateIntent  ************************/
    /*
     * Used to update the intent. If Luis enabled, turns the 'input'
     * string into an intent by calling Luis API. Else, set intent 
     * to 'input'.
     *
     * [LUIS API NOT YET IMPLEMENTED]
     *
     */
    public void UpdateIntent(string input, System.Action callback, bool sendToLuis = false)
    {
        if (sendToLuis == false)
        {
            currIntent = input;
            callback();
        }
        else
        {
            string my_LUIS_APP;
            string my_LUIS_SUB_KEY;
            env.TryParseEnvironmentVariable("LUIS_APP", out my_LUIS_APP);
            env.TryParseEnvironmentVariable("LUIS_SUB_KEY", out my_LUIS_SUB_KEY);

            string uri = $"{LUIS_ENDPOINT}/{my_LUIS_APP}/slots/production/predict?subscription-key={my_LUIS_SUB_KEY}&verbose=true&show-all-intents=true&log=true&query={input}";
            IsLoading = true;
            StartCoroutine(GetRequest(uri, callback));
        }
    }

    // COPIED/MODIFIED FROM HERE:
    // https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Get.html
    IEnumerator GetRequest(string uri, System.Action callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("Connection error.");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Data Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Received: " + webRequest.downloadHandler.text);
                    dynamic luisResponse = JObject.Parse(webRequest.downloadHandler.text);
                    currIntent = luisResponse.prediction.topIntent;
                    float iScore = luisResponse.prediction.intents[currIntent].score;
                    if (iScore < MIN_ACCEPTABLE_SCORE)
                    {
                        Debug.Log("Score is too low; replacing with error intent");
                        currIntent = ERR_INTENT;
                    }
                    Debug.Log(luisResponse);
                    callback();
                    break;
            }
        }
    }

    /**************  NextMessageRequiresInput() *****************/
    /*
     * If the current node's children are of type "OptionNode", return true. Otherwise, 
     * return false. If no children, returns false.
     */
    public bool NextMessageRequiresInput()
    {
        return (currNode.Connections[0].ConnectionType == Connection.eConnectionType.Option);
    }

    /***************** OnLastMessage**************/
    /*
    Returns true if curr node is our last message in this sequence
    */
    public bool OnLastMessage()
    {
        if (currNode.Connections.Count == 0)
            EventManager.RaiseOnConversationEnd();
        return (currNode.Connections.Count == 0);
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
     *
     * [We're actually not using connections right now; leaving in case we do later].
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
     * Each option text will have format "intent entityKey=entityVal entityKey=entityVal..."
     * This function will test whether the entities specified in text of option
     * match entities in dictionary. If a given entityKey not specified in dicctionary, is false.
     * 
     */
    private bool OptionMatchesIntent(string optionText, string intent)
    {
        optionText = optionText.Trim();
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

            string dictVal = "";
            if (!Entities.TryGetValue(entityKey, out dictVal) || dictVal != entityVal)
            {
                return false;
            }
        }
        return true;
    }

    /*************** checkQuest ********************/
    /*
     * This function returns true/false if there
     * is an OptionNode child of the root representing the appropriate quest/step combo.
     */
    public bool checkQuest()
    {

        Entities[QUEST] = PlayerPrefs.GetInt("Quest").ToString();
        Entities[QUEST_STEP] = PlayerPrefs.GetInt("QuestStep").ToString();

        foreach (Connection connection in conversation.Root.Connections)
        {
            if (connection.ConnectionType == Connection.eConnectionType.Option)
            {
                OptionNode optionNode = ((OptionConnection)connection).OptionNode;
                if (OptionMatchesIntent(optionNode.Text, "quest"))
                {
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
     * Returns null. Use CurrentText to access current text. 
     *
     * Assumes only 1 error intent.
     * 
     */
    public void GetNextMessage()
    {
        List<ConversationNode> matches = new List<ConversationNode>();
        OptionNode fallbackNode = null;

        print("Current node is " + currNode.Text + "with connections " + currNode.Connections.Count);

        // Iterate over each connection, add all valid to list of matches.
        foreach (Connection connection in currNode.Connections)
        {
            // ConnectionConditionsValid always true currently, as we do not use
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
                    else if (option.Text == ERR_INTENT)
                    {
                        fallbackNode = option;
                    }
                }
                else
                {
                    matches.Add(((SpeechConnection)connection).SpeechNode);
                }
            }
        }
        if (matches.Count == 0 && currNode.Connections.Count > 0 && fallbackNode != null)
        {
            matches.Add(fallbackNode); // If no matching intents, but the convo should continue, set FallbackNode as match.
        }
        if (matches.Count > 0)
        {
            // In case of multiple matches, return a random match.. 
            int matchIndex = rnd.Next(matches.Count);
            print(matchIndex);
            currNode = matches[matchIndex];
            // If we arrived at option node, advance 1x more in order so currNode points to speechNode.
            if (currNode.NodeType == ConversationNode.eNodeType.Option)
            {
                OptionNode x = (OptionNode)currNode;
                x.Event.Invoke();
                GetNextMessage();
                return;
            }
            else
            {
                print(currNode.Text);
                // We will return the text at current node.
                setCurrentText(currNode.Text);
                //invoking the event associating with the node
                SpeechNode x = (SpeechNode)currNode;
                x.Event.Invoke();

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
            }
        }
        print("Match count is " + matches.Count + " and new node is " + currNode.Text);
    }
}
