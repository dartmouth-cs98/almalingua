using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class showProtagonist : MonoBehaviour{

    public GameObject innitPlayer;
    public GameObject defaultPlayer;
    public GameObject camObj;



    // Start is called before the first frame update
    void Start()
    {
        innitPlayer.SetActive(true);
        defaultPlayer.SetActive(false);
        CinemachineVirtualCamera virtualCamera = camObj.GetComponent<CinemachineVirtualCamera>();
        Transform toFollow = innitPlayer.transform.GetChild(0);
        virtualCamera.m_Follow = toFollow;
        
    }


    public void Switch(){ 
        Joystick j = innitPlayer.GetComponent<Joystick>();
        j = null;
        innitPlayer.SetActive(false);
        defaultPlayer.SetActive(true);

        // change camera
        CinemachineVirtualCamera virtualCamera = camObj.GetComponent<CinemachineVirtualCamera>();
        Transform toFollow = defaultPlayer.transform.GetChild(0);
        virtualCamera.m_Follow = toFollow;


        //match the transforms
        float x = innitPlayer.transform.position.x;
        float y = innitPlayer.transform.position.y;
        float z = innitPlayer.transform.position.z;
 
        defaultPlayer.transform.position = new Vector3(x, y, z);


    }

}
