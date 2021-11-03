using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**Celina Tala 
Purpose: To keep gameobjects within the bounds of the screen
 */

// code taken from https://pressstart.vip/tutorials/2018/06/28/41/keep-object-in-bounds.html
public class BoundaryManager : MonoBehaviour
{
    public GameObject Collider; //the boundaries of our screen
    private Vector2 size;       //size of our screen
    private Vector3 worldPos;   //center of our screen in world position 
    private float objectWidth;  //width of current gameobject
    private float objectHeight; //height of current game object

    // Use this for initialization
    void Start()
    {
        size = Collider.GetComponent<BoxCollider2D>().size;
        worldPos = transform.TransformPoint(Collider.GetComponent<BoxCollider2D>().bounds.center);
        objectWidth = gameObject.GetComponent<BoxCollider2D>().bounds.size.x; //width of box collider divided by 2
        objectHeight = gameObject.GetComponent<BoxCollider2D>().bounds.size.y; ; //height of box collider divided by 2
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = transform.position; //our current gameobject's position
        viewPos.x = Mathf.Clamp(viewPos.x, worldPos.x - (size.x / 2f), worldPos.x + (size.x / 2f) - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, worldPos.y - (size.y / 2f) + objectHeight + 0.5f, worldPos.y + (size.y / 2f));
        transform.position = viewPos;
    }

}
