using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D trigger;

    private Vector2 direction;
    public float thrust = 3.0f;

    private const int RAND_RANGE = 30;

    public float ATTACK_SIGHT_RANGE = 8.0f;
    public float REG_SIGHT_RANGE = 5.0f;

    private const string OBS_LAYER = "Obstacle";
    private LayerMask obstacleMask;

    private const string PLAYER_TAG = "Player";
    public bool SeesPlayer = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        trigger = gameObject.GetComponent<CircleCollider2D>();
        obstacleMask = LayerMask.GetMask(OBS_LAYER);
    }


    /********* OnTriggerStay2D(Collider2D other)***********/
    /*
     * If the collider currently in trigger (this function called every FixedUpdate I believe, for each matching collider)
     * is the player, check if there are any objects on the LAYER "OBSTACLE" between them.
     * 
     * IF YES, be in WANDER MODE.
     * ELSE, be in ATTACK MODE.
     *
     * ASSUMES enemy not on OBSTACLE layer, and all OBSTACLES assigned to layer.
     * ASSUMES player assigned PLAYER tag.
     */
    void OnTriggerStay2D(Collider2D other)
    {
      if (other.tag == PLAYER_TAG) {
        Vector2 rayDir = (other.transform.position - transform.position);
        rayDir.Normalize();
        RaycastHit2D hit = Physics2D.Linecast(transform.position, other.transform.position, obstacleMask);

        if (hit.collider != null) 
        {
          SeesPlayer = false;
          trigger.radius = REG_SIGHT_RANGE;
        } 
        else 
        {
          SeesPlayer = true;
          direction = other.transform.position - transform.position;
          trigger.radius = ATTACK_SIGHT_RANGE;
        }
      }
    }


    /********* OnTriggerExit2D(Collider2D other) ***********/
    /*
     * When a collider exits the enemy's trigger radius, check if is the player (could be another collider).
     * If yes, end chase mode.
     */
    void OnTriggerExit2D(Collider2D other)
    {
      if (other.tag == PLAYER_TAG) {
        SeesPlayer = false;
        trigger.radius = REG_SIGHT_RANGE;
      }
    }

    void FixedUpdate()
    {
      if (!SeesPlayer) {
        moveRandomly();
      }
      rb.AddForce(direction * thrust);
    }

    void moveRandomly()
    {
      int rand = Random.Range(1, RAND_RANGE);
      if (rand == 1) {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
      }
    }

}
