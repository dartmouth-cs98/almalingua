using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class showProtagonist : MonoBehaviour
{

    public GameObject innitPlayer;
    public GameObject defaultPlayer;
    public GameObject camObj;



    // Start is called before the first frame update
    void Start()
    {
        Transform toFollow;
        if (PlayerPrefs.GetInt("Quest") > 1)
        {
            innitPlayer.SetActive(false);
            defaultPlayer.SetActive(true);
            toFollow = defaultPlayer.transform.GetChild(0);

        }
        else
        {
            innitPlayer.SetActive(true);
            defaultPlayer.SetActive(false);
            toFollow = innitPlayer.transform.GetChild(0);
        }
        CinemachineVirtualCamera virtualCamera = camObj.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.m_Follow = toFollow;
    }


    public void Switch()
    {
        innitPlayer.SetActive(false);
        defaultPlayer.SetActive(true);

        // change camera
        CinemachineVirtualCamera virtualCamera = camObj.GetComponent<CinemachineVirtualCamera>();
        Transform toFollow = defaultPlayer.transform.GetChild(0);
        virtualCamera.m_Follow = toFollow;


        //match the transforms
        float x = innitPlayer.transform.position.x;
        float y = innitPlayer.transform.position.y;

        defaultPlayer.transform.position = new Vector2(x, y);


    }

}
