using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class MiniMap : MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject playerIcon;
    public GameObject player;
    // Start is called before the first frame update


  
    void Start(){
        PlayerCamera = GameObject.Find("Main Camera");

        int q =  PlayerPrefs.GetInt("Quest");
        int step = PlayerPrefs.GetInt("QuestStep");
        if ( q < 3){
             player = GameObject.Find("CameraPlayer/PlayerManager/init_Protagonist/init_Character");

        }
        else{
            player = GameObject.Find("CameraPlayer/PlayerManager/Protagonist/Character");
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = PlayerCamera.transform.position;
        float x = player.transform.position.x;
        float y = player.transform.position.y;

        playerIcon.transform.position = new Vector3(x, y-1, player.transform.position.z);
        
    }
}
