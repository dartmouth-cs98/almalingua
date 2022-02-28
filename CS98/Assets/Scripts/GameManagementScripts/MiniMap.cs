using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject playerIcon;
    public GameObject player;
    // Start is called before the first frame update


    void Start(){
        PlayerCamera = GameObject.Find("CameraPlayer/ProtagonistCamera");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = PlayerCamera.transform.position;
        float x = player.transform.position.x;
        float y = player.transform.position.y;

        playerIcon.transform.position = new Vector2(x, y-1);


        
    }
}
