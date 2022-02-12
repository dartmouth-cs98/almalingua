using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Celina Tala

/**
This class is to deal with our picture panel
*/
// public class PicturePanelManager : MonoBehaviour
// {
//     private static GameObject picturePanel;

//     private void OnEnable()
//     {
//         EventManager.onConversationStart += ConversationStart;
//     }

//     private void onDisable()
//     {
//         EventManager.onConversationStart -= ConversationStart;
//     }

//     //when our converesation starts, reset the picture child to 0
//     public void ConversationStart()
//     {
//         PlayerPrefs.SetInt("pictureChild", 0);

//     }

//     //the event that the dialogue editor will call
//     //it shows the corresponding image
//     public void ShowPicturePanel()
//     {
//         int pictureChild = PlayerPrefs.GetInt("pictureChild");
//         if (pictureChild == 0)
//         {
//             picturePanel = GameObject.Find("PicturePanelParent").transform.GetChild(0).gameObject;
//             picturePanel.SetActive(true);
//         }
//         //this section is checking if we want to show the pictures of the orange/staff 
//         if (pictureChild > 0)
//         {
//             picturePanel.transform.GetChild(pictureChild - 1).gameObject.SetActive(false);
//         }
//         picturePanel.transform.GetChild(pictureChild).gameObject.SetActive(true);
//         PlayerPrefs.SetInt("pictureChild", pictureChild + 1);
//     }

//     public static void HidePicturePanel()
//     {
//         if (picturePanel != null)
//             picturePanel.SetActive(false);
//     }
// }