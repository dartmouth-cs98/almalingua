using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  public int movementSpeed;

  private Vector2 pointA;
  private Vector2 pointB;

  private Vector2 BALL_CENTER;

  public GameObject ball;

  // Use this for initialization
  void Start () {
    BALL_CENTER = new Vector2(ball.GetComponent<RectTransform>().position.x, ball.GetComponent<RectTransform>().position.y);
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetMouseButton(0)) {
      Vector2 touchPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
      Vector2 relativePosition = touchPosition - BALL_CENTER;
      Debug.Log(relativePosition.normalized * 30);
      ball.GetComponent<RectTransform>().position = BALL_CENTER + (relativePosition.normalized * 30);
      moveCharacter(relativePosition.normalized);
    }
      
  }

  void moveCharacter (Vector2 direction) {
      gameObject.GetComponent<RectTransform>().Translate(direction * movementSpeed * Time.deltaTime);
  }

}
