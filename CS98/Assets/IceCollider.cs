using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCollider : MonoBehaviour
{
    public GameObject PlayerManager;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerManager.GetComponent<showProtagonist>().Switch();
        gameObject.SetActive(false);
    }
}
