using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Celina Tala

/**
This class is to deal with the ice melting animation
*/
public class IceCollider : MonoBehaviour
{
    public GameObject PlayerManager;
    private GameObject MiniMapCam;
    private bool start = false;
    private float timer = 0f;

    bool animationPlay = false;
    void Start(){
        MiniMapCam = GameObject.Find("MiniMapCam");
    }
    public void MeltIce()
    {
        GameObject.Find("IceMeltingAnimation").GetComponent<Animator>().Play("Ice_Melting_Animation");
    }

    void Update(){
        timer += (Time.deltaTime)*1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (timer > 5f) {
            start = false;
            PlayerManager.GetComponent<showProtagonist>().Switch();
            EventManager.RaiseOnProtagonistChange();
            gameObject.SetActive(false);

            GameObject protagonist = GameObject.Find("CameraPlayer/PlayerManager/Protagonist");

            MiniMap miniScript = (MiniMap)MiniMapCam.GetComponent("MiniMap");
            miniScript.player = protagonist;
        }
       

    }
}
