using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float thrust = 10f;
    public float movementSpeed;

    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    float fasterSpeed;
    float slowerSpeed;
    Vector2[] playerPositions = new Vector2[] {new Vector2((float)0.5, (float)-1), 
    new Vector2((float)-9.3, (float)-20), new Vector2((float)48, (float)-1.4), 
    new Vector2((float)-3.5, (float)16.2), new Vector2((float)-57, (float)-2.97), new Vector2((float) -9.5, (float)17.8),
    new Vector2((float)-5.04, (float)-19)};

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        fasterSpeed = (float)(thrust*3);
        slowerSpeed = thrust;
    }

    private void OnEnable() {
        string prevScene = PlayerPrefs.GetString("PrevScene");
        string currentScene = SceneManager.GetActiveScene().name;

        if (prevScene != null) {

            if (currentScene == "Village") {
                if (prevScene == "Farm") {
                    transform.position = playerPositions[1];
                } else if (prevScene == "WitchHouse") {
                    transform.position = playerPositions[0];
                } else if (prevScene == "Forest") {
                    transform.position = playerPositions[2];
                } else if (prevScene == "Ruins") {
                    transform.position = playerPositions[5];
                }
            } else if (currentScene == "Farm") {
                if (prevScene == "Village") {
                    transform.position = playerPositions[3];
                }
            } else if (currentScene == "Forest") {
                if (prevScene == "Village") {
                    transform.position = playerPositions[4];
                }
            } else if (currentScene == "Ruins") {
                if (prevScene == "Village") {
                    transform.position = playerPositions[6];
                } 
            }
        }
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerDirection = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            playerDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            playerDirection = Vector2.down;
        } 
        else
        {
            playerDirection = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.LeftShift) && playerDirection != Vector2.zero) {
            thrust = fasterSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            thrust = slowerSpeed;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveCharacter();

    }

    void moveCharacter()
    {
        rb.AddForce(playerDirection * thrust);
        animate(playerDirection);
    }

    // TY Code
    void animate(Vector2 playerDirection)
    {
        animator.SetFloat("Horizontal", playerDirection.x);
        animator.SetFloat("Vertical", playerDirection.y);
        animator.SetFloat("Speed", movementSpeed);
    }

}

