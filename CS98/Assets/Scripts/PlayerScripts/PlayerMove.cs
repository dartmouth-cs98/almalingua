using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{

  private Touch touch;
  public float thrust = 10f;
  protected Joystick joystick; 

  public Rigidbody2D rb;
  private Vector2 playerDirection;
  public Animator animator;
  public float movementSpeed;

  // Use this for initialization
  void Start () {
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  void Update () {

    if (Input.GetKey(KeyCode.A)) {
      playerDirection = Vector2.left;
    } else if (Input.GetKey(KeyCode.D)) {
      playerDirection = Vector2.right;
    } else if (Input.GetKey(KeyCode.W)) {
      playerDirection = Vector2.up;
    } else if (Input.GetKey(KeyCode.S)) {
      playerDirection = Vector2.down;
    } else {
      playerDirection = Vector2.zero;
    }

  }

  // Update is called once per frame
  void FixedUpdate () {
    moveCharacter();

  }

  void moveCharacter () {
      rb.AddForce(playerDirection * thrust);
      animate(playerDirection);
  }

  // TY Code
  void animate(Vector2 playerDirection){
    animator.SetFloat("Horizontal", playerDirection.x);
    animator.SetFloat("Vertical", playerDirection.y);
    animator.SetFloat("Speed", movementSpeed);
  }

}

