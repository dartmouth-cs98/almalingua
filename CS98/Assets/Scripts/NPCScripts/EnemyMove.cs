using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D trigger;

	public Animator animator;
    private Vector2 direction;


    public GameObject sceneLoader;
    public float thrust = 8f;

    private const int RAND_RANGE = 50;

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
        trigger.radius = REG_SIGHT_RANGE;
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
      } else {
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
        SeesPlayer = false; //TY: changes to true? not triggering this though
        trigger.radius = REG_SIGHT_RANGE;
      }
    }

    void FixedUpdate()
    {
      if (!SeesPlayer) {
        moveRandomly();
      }
      rb.AddForce(direction * thrust);
      animate(direction); // to animate
    }

    // Allows the enemy to move randomly
    void moveRandomly()
    {
      int rand = Random.Range(1, RAND_RANGE);
      int horizontal = 1;
      int vertical = 1;

      if (rand == 1) {
      	//TY: change random into only 2 options
      	if(Random.Range(1,3) == 1){
      		horizontal = 1;
      	} else {
      		horizontal = -1;
      	}
      	if(Random.Range(1,3) == 1){
      		vertical = 1;
      	} else {
      		vertical = -1;
      	} 
      	//

        direction = new Vector2(horizontal, vertical); //TY: added int to cut it into ints
      }
    }

    // TY Code: animate based on direction
    void animate(Vector2 playerDirection)
    {
    	if (direction.x >= 0 && direction.y >= 0){ //1st quadrant
    		direction = new Vector2(1, 1);
    	} else if (direction.x >= 0 && direction.y < 0){
    		direction = new Vector2(1, -1);
    	} else if (direction.x < 0 && direction.y >= 0){
    		direction = new Vector2(-1, 1);
    	} else {
    		direction = new Vector2(-1, -1);
    	}

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        // animator.SetFloat("Speed", movementSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
      if (other.gameObject.tag == PLAYER_TAG){
        sceneLoader.GetComponent<SceneLoader>().LoadScene("combatScene");
      }
    }

}
