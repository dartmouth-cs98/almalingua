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
  public Vector2 movementDirection;
  public Animator animator;
  public float movementSpeed;

  // Use this for initialization
  void Start () {
    joystick = FindObjectOfType<Joystick>();
  }

  // Update is called once per frame
  void FixedUpdate () {
    moveCharacter(joystick.Direction);
  }

  void moveCharacter (Vector2 playerDirection) {
      gameObject.GetComponent<Rigidbody2D>().AddForce(playerDirection * thrust);
      animate(playerDirection);
  }

  // TY Code
  void animate(Vector2 playerDirection){
    animator.SetFloat("Horizontal", playerDirection.x);
    animator.SetFloat("Vertical", playerDirection.y);
    animator.SetFloat("Speed", movementSpeed);
  }

}


/*
    /*
    if (Input.touchCount > 0) {
      touch = Input.GetTouch(0);

      Vector2 touchPosition = new Vector2(touch.position.x, touch.position.y);
      Vector2 relativePosition = touchPosition - BALL_CENTER;

      Debug.Log(touchPosition);

      // Get angle of joystick. 
      float relAngle = Vector2.Angle(Vector2.right, relativePosition);
      Vector2 playerDirection;

      // Get the nearest cardinal direction of the joystick
      if (relAngle > 135) {
        playerDirection = Vector2.left;
      } else if (relAngle < 45) {
        playerDirection = Vector2.right;
      } else {
        if (relativePosition.y > 0) {
          playerDirection = Vector2.up;
        } else {
          playerDirection = Vector2.down;
        }
      }
      // TY Code
      movementDirection = playerDirection;

      ball.GetComponent<RectTransform>().position = BALL_CENTER + (relativePosition.normalized * BALL_RANGE);
      moveCharacter(playerDirection);

      // TY Code
      animate();
    }
    */
