using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  public int BALL_RANGE = 25;
  private Vector2 BALL_CENTER;
  private GameObject ball;

  private Vector2 pointA;
  private Vector2 pointB;

  private Touch touch;

  public float thrust = 10f;

  protected Joystick joystick; 

  // Save a reference to the joystick (should only be 1 per screen)
  // and its center pivot point.

  void Start () {
    joystick = FindObjectOfType<Joystick>();
  }

  // Update is called once per frame
  void FixedUpdate () {
    Debug.Log(joystick.Direction);
    moveCharacter(joystick.Direction);
  }

  void moveCharacter (Vector2 playerDirection) {
      gameObject.GetComponent<Rigidbody2D>().AddForce(playerDirection * thrust);
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

      ball.GetComponent<RectTransform>().position = BALL_CENTER + (relativePosition.normalized * BALL_RANGE);
      moveCharacter(playerDirection);
    }
    */