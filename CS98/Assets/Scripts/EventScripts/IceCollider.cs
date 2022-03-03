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

    void Start(){
        MiniMapCam = GameObject.Find("MiniMapCam");
    }
    public void MeltIce()
    {
        GameObject.Find("IceMeltingAnimation").GetComponent<Animator>().Play("Ice_Melting_Animation");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
            PlayerManager.GetComponent<showProtagonist>().Switch();
            EventManager.RaiseOnProtagonistChange();
            gameObject.SetActive(false);

            GameObject protagonist = GameObject.Find("CameraPlayer/PlayerManager/Protagonist");

            MiniMap miniScript = (MiniMap)MiniMapCam.GetComponent("MiniMap");
            miniScript.player = protagonist;

    }
}
