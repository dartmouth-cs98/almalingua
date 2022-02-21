using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{
    public float thrust = 10f;
    public float movementSpeed;

    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    float fasterSpeed;
    float slowerSpeed;


    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        fasterSpeed = (float)(thrust*3);
        slowerSpeed = thrust;
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

