using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCollider : MonoBehaviour
{
    public GameObject PlayerManager;
    public bool canMelt = false;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canMelt)
        {
            PlayerManager.GetComponent<showProtagonist>().Switch();
            gameObject.SetActive(false);
        }
    }
}
