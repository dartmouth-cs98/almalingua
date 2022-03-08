using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalQuest : MonoBehaviour
{
    public GameObject Panel;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other) {
        if (!Panel.activeSelf)
        {
            Panel.GetComponent<HideShowObjects>().Show();
        }
         Panel.GetComponent<NPCDialogueUI>().NPCInteract(gameObject.name);
    }
}
