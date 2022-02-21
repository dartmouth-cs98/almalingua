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
    public bool canMelt = false;


    public void MeltIce()
    {
        GameObject.Find("IceMeltingAnimation").GetComponent<Animator>().Play("Ice_Melting_Animation");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
<<<<<<< HEAD
        if (canMelt )
        {
=======
>>>>>>> 96130c9ca206f116bd58dffe1ef9b53d5020aa8c
            PlayerManager.GetComponent<showProtagonist>().Switch();
            EventManager.RaiseOnProtagonistChange();
            gameObject.SetActive(false);
    }
}
