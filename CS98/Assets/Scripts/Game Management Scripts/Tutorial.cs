using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * A script controlling the first tutorial scene UI and progression.
 *
 * Sada Nichols-Worley
 */

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI TextDetails;
    public GameObject HighlightArrow;
    public GameObject InputField;

    private static string message1 = "Hello and welcome to the land of Almalingua.";
    private static string message2 = "Must've been a rough crash... " +
        "Try using the joystick to make your way to the red circle.";

    private static string message3 = "Well done mysterious traveler. What is your name?";
    private static string message4 = "I may know someone who can help you. Take this path to the house.";
    private string[] messages = { message1, message2, message3, message4 };
    private int currentMessage = 0;


    // Start is called before the first frame update
    void Start()
    {
        TextDetails.text = message1;
    }

    // Tracking when player uses joystick to find target and displaying next message
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentMessage == 1)
        {
            NextMessage();
        }
    }

    // Updating which message is displayed in the text box
    public void NextMessage()
    {
        currentMessage += 1;
        if (currentMessage == 2)
        {
            InputField.SetActive(true);
        }
        TextDetails.text = messages[currentMessage];
    }

    
}
