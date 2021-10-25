using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  public int movementSpeed;
  public int BALL_RANGE = 25;

  private Vector2 pointA;
  private Vector2 pointB;

  private GameObject ball;
  private Vector2 BALL_CENTER;

  private double angleSplit = 45;

  public float thrust = 10f;

  // Use this for initialization
  void Start () {
    if (ball == null) {
        ball = GameObject.FindGameObjectsWithTag("JoystickBall")[0];
    }
    BALL_CENTER = new Vector2(ball.GetComponent<RectTransform>().position.x, ball.GetComponent<RectTransform>().position.y);
  }

  // Update is called once per frame
  void FixedUpdate () {
    if (Input.GetMouseButton(0)) {
      Vector2 touchPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
      Vector2 relativePosition = touchPosition - BALL_CENTER;

      float relAngle = Vector2.Angle(Vector2.right, relativePosition);
      Vector2 playerDirection;

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
      
  }

  void moveCharacter (Vector2 playerDirection) {
      gameObject.GetComponent<Rigidbody2D>().AddForce(playerDirection * thrust);
  }

}